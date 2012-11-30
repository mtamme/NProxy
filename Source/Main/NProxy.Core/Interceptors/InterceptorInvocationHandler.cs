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
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Internal.Common;

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Represents an interceptor invocation handler.
    /// </summary>
    internal sealed class InterceptorInvocationHandler : IInvocationHandler
    {
        /// <summary>
        /// The interceptors.
        /// </summary>
        private readonly IInterceptor[] _interceptors;

        /// <summary>
        /// The method interceptors.
        /// </summary>
        private readonly Dictionary<long, IInterceptor[]> _methodInterceptors;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dispatcher"/> class.
        /// </summary>
        /// <param name="interceptor">The interceptor.</param>
        public InterceptorInvocationHandler(params IInterceptor[] interceptors)
        {
            if (interceptors == null)
                throw new ArgumentNullException("interceptor");

            _interceptors = interceptors;

            _methodInterceptors = new Dictionary<long, IInterceptor[]>();
        }

        /// <summary>
        /// Applies all interceptors for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="defaultInterceptors">The default interceptors.</param>
        public void ApplyInterceptors(Type type, IEnumerable<IInterceptor> defaultInterceptors)
        {
            // Apply type interception behaviors.
            var typeInterceptors = ApplyInterceptionBehaviors(type, defaultInterceptors);

            // Apply event interceptors.
            var eventVisitor = Visitor.Create<EventInfo>(e => ApplyInterceptors(e, typeInterceptors));

            type.VisitEvents(eventVisitor);

            // Apply property interceptors.
            var propertyVisitor = Visitor.Create<PropertyInfo>(p => ApplyInterceptors(p, typeInterceptors));

            type.VisitProperties(propertyVisitor);

            // Apply method interceptors.
            var methodVisitor = Visitor.Create<MethodInfo>(m => ApplyInterceptors(m, typeInterceptors));

            type.VisitMethods(methodVisitor);
        }

        /// <summary>
        /// Applies all interceptors for the specified event.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <param name="defaultInterceptors">The default interceptors.</param>
        private void ApplyInterceptors(EventInfo eventInfo, IEnumerable<IInterceptor> defaultInterceptors)
        {
            var eventInterceptors = ApplyInterceptionBehaviors(eventInfo, defaultInterceptors);

            foreach (var methodInfo in eventInfo.GetAccessorMethods())
            {
                ApplyInterceptors(methodInfo, eventInterceptors);
            }
        }

        /// <summary>
        /// Applies all interceptors for the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="defaultInterceptors">The default interceptors.</param>
        private void ApplyInterceptors(PropertyInfo propertyInfo, IEnumerable<IInterceptor> defaultInterceptors)
        {
            var propertyInterceptors = ApplyInterceptionBehaviors(propertyInfo, defaultInterceptors);

            foreach (var methodInfo in propertyInfo.GetAccessorMethods())
            {
                ApplyInterceptors(methodInfo, propertyInterceptors);
            }
        }

        /// <summary>
        /// Applies all interceptors for the specified method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="defaultInterceptors">The default interceptors.</param>
        private void ApplyInterceptors(MethodInfo methodInfo, IEnumerable<IInterceptor> defaultInterceptors)
        {
            var interceptors = ApplyInterceptionBehaviors(methodInfo, defaultInterceptors);

            SetInterceptors(methodInfo, interceptors);
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
        /// Sets the interceptors for the specified method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="interceptors">The interceptors.</param>
        private void SetInterceptors(MethodInfo methodInfo, IList<IInterceptor> interceptors)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            if (interceptors == null)
                throw new ArgumentNullException("interceptors");

            if (interceptors.Count == 0)
                return;

            var methodToken = methodInfo.GetToken();

            _methodInterceptors.Add(methodToken, interceptors.Concat(_interceptors).ToArray());
        }

        /// <summary>
        /// Returns all interceptors for the specified member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The interceptors.</returns>
        private IInterceptor[] GetInterceptors(MemberInfo memberInfo)
        {
            var methodToken = memberInfo.GetToken();
            IInterceptor[] interceptors;

            return _methodInterceptors.TryGetValue(methodToken, out interceptors) ? interceptors : _interceptors;
        }

        #region IInvocationHandler Members

        /// <inheritdoc/>
        public object Invoke(object proxy, MethodInfo methodInfo, object[] parameters)
        {
            var interceptors = GetInterceptors(methodInfo);
            var invocationContext = new InvocationContext(proxy, methodInfo, parameters, interceptors);

            return invocationContext.Proceed();
        }

        #endregion
    }
}
