//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © 2012 Martin Tamme
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NProxy.Core.Interceptors.Language;
using NProxy.Core.Internal.Common;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Represents proxy factory extensions.
    /// </summary>
    public static class ProxyFactoryExtensions
    {
        /// <summary>
        /// Creates a proxy object.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="invocationTarget">The invocation target.</param>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The proxy object.</returns>
        public static T CreateProxy<T>(this IProxyFactory proxyFactory,
                                       IEnumerable<Type> interfaceTypes,
                                       IInvocationTarget invocationTarget,
                                       IInvocationHandler invocationHandler,
                                       params object[] arguments) where T : class
        {
            if (proxyFactory == null)
                throw new ArgumentNullException("proxyFactory");

            var delegator = new Delegator(invocationTarget, invocationHandler);

            return proxyFactory.CreateProxy<T>(interfaceTypes, delegator, arguments);
        }

        /// <summary>
        /// Creates a proxy object.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="interceptor">The interceptor.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The proxy object.</returns>
        public static T CreateProxy<T>(this IProxyFactory proxyFactory,
                                       IEnumerable<Type> interfaceTypes,
                                       IInterceptor interceptor,
                                       params object[] arguments) where T : class
        {
            if (proxyFactory == null)
                throw new ArgumentNullException("proxyFactory");

            var interceptorChain = new InterceptorChain(interceptor);

            return proxyFactory.CreateProxy<T>(interfaceTypes, interceptorChain, arguments);
        }

        /// <summary>
        /// Creates a proxy object.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="invocationTarget">The invocation target.</param>
        /// <param name="interceptor">The interceptor.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The proxy object.</returns>
        public static T CreateProxy<T>(this IProxyFactory proxyFactory,
                                       IEnumerable<Type> interfaceTypes,
                                       IInvocationTarget invocationTarget,
                                       IInterceptor interceptor,
                                       params object[] arguments) where T : class
        {
            if (proxyFactory == null)
                throw new ArgumentNullException("proxyFactory");

            var delegator = new Delegator(invocationTarget, new InterceptorChain(interceptor));

            return proxyFactory.CreateProxy<T>(interfaceTypes, delegator, arguments);
        }

        /// <summary>
        /// Creates a proxy object.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The proxy object.</returns>
        public static T CreateProxy<T>(this IProxyFactory proxyFactory,
                                       IEnumerable<Type> interfaceTypes,
                                       IEnumerable<IInterceptor> interceptors,
                                       params object[] arguments) where T : class
        {
            if (proxyFactory == null)
                throw new ArgumentNullException("proxyFactory");

            var interceptorChain = new InterceptorChain(interceptors);

            return proxyFactory.CreateProxy<T>(interfaceTypes, interceptorChain, arguments);
        }

        /// <summary>
        /// Creates a proxy object.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="invocationTarget">The invocation target.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The proxy object.</returns>
        public static T CreateProxy<T>(this IProxyFactory proxyFactory,
                                       IEnumerable<Type> interfaceTypes,
                                       IInvocationTarget invocationTarget,
                                       IEnumerable<IInterceptor> interceptors,
                                       params object[] arguments) where T : class
        {
            if (proxyFactory == null)
                throw new ArgumentNullException("proxyFactory");

            var delegator = new Delegator(invocationTarget, new InterceptorChain(interceptors));

            return proxyFactory.CreateProxy<T>(interfaceTypes, delegator, arguments);
        }

        /// <summary>
        /// Creates a proxy object.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="invocationTarget">The invocation target.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The proxy object.</returns>
        public static T CreateProxy<T>(this IProxyFactory proxyFactory,
                                       IEnumerable<Type> interfaceTypes,
                                       IInvocationTarget invocationTarget,
                                       params object[] arguments) where T : class
        {
            var dispatcher = new Dispatcher();
            var declaringType = typeof (T);

            // Apply declaring type interception behaviors.
            var typeInterceptors = ApplyInterceptionBehaviors(declaringType, Enumerable.Empty<IInterceptor>());

            // Apply declaring type event interception behaviors.
            var eventVisitor = Visitor.Create<EventInfo>(e =>
                                                             {
                                                                 var eventInterceptors = ApplyInterceptionBehaviors(e, typeInterceptors);

                                                                 foreach (var methodInfo in e.GetAccessorMethods())
                                                                 {
                                                                     var interceptors = ApplyInterceptionBehaviors(methodInfo, eventInterceptors);

                                                                     dispatcher.SetInterceptors(methodInfo, interceptors);
                                                                 }
                                                             });

            declaringType.VisitEvents(eventVisitor);

            // Apply declaring type property interception behaviors.
            var propertyVisitor = Visitor.Create<PropertyInfo>(p =>
                                                                   {
                                                                       var propertyInterceptors = ApplyInterceptionBehaviors(p, typeInterceptors);

                                                                       foreach (var methodInfo in p.GetAccessorMethods())
                                                                       {
                                                                           var interceptors = ApplyInterceptionBehaviors(methodInfo, propertyInterceptors);

                                                                           dispatcher.SetInterceptors(methodInfo, interceptors);
                                                                       }
                                                                   });

            declaringType.VisitProperties(propertyVisitor);

            // Apply declaring type method interception behaviors.
            var methodVisitor = Visitor.Create<MethodInfo>(m =>
                                                               {
                                                                   var interceptors = ApplyInterceptionBehaviors(m, typeInterceptors);

                                                                   dispatcher.SetInterceptors(m, interceptors);
                                                               });

            declaringType.VisitMethods(methodVisitor);

            var delegator = new Delegator(invocationTarget, dispatcher);

            return (T) proxyFactory.CreateProxy(declaringType, interfaceTypes, delegator, arguments);
        }

        /// <summary>
        /// Applies the interception behaviors for the specified member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="defaultInterceptors">The default interceptors.</param>
        /// <returns>The interceptors.</returns>
        private static IList<IInterceptor> ApplyInterceptionBehaviors(MemberInfo memberInfo, IEnumerable<IInterceptor> defaultInterceptors)
        {
            var interceptionBehaviors = memberInfo.GetCustomAttributes<IInterceptionBehavior>();
            var interceptors = new List<IInterceptor>(defaultInterceptors);

            foreach (var interceptionBehavior in interceptionBehaviors)
            {
                interceptionBehavior.Validate(memberInfo);
                interceptionBehavior.Apply(memberInfo, interceptors);
            }

            return interceptors;
        }

        /// <summary>
        /// Returns a fluent interface to configure a new proxy.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>A fluent interface to configure a new proxy.</returns>
        public static INewProxy<T> NewProxy<T>(this IProxyFactory proxyFactory, params object[] arguments) where T : class
        {
            return new NewProxy<T>(proxyFactory, arguments);
        }
    }
}
