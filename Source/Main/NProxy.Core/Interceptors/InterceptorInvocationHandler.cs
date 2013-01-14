//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © Martin Tamme
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
using System.Reflection;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Internal.Common;

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Represents an interceptor invocation handler.
    /// </summary>
    [Serializable]
    internal sealed class InterceptorInvocationHandler : IInvocationHandler
    {
        /// <summary>
        /// The default interceptors.
        /// </summary>
        private readonly IInterceptor[] _defaultInterceptors;

        /// <summary>
        /// The interceptors.
        /// </summary>
        private readonly Dictionary<MemberToken, IInterceptor[]> _interceptors;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptorInvocationHandler"/> class.
        /// </summary>
        /// <param name="defaultInterceptors">The default interceptor.</param>
        public InterceptorInvocationHandler(IInterceptor[] defaultInterceptors)
        {
            if (defaultInterceptors == null)
                throw new ArgumentNullException("defaultInterceptors");

            _defaultInterceptors = defaultInterceptors;

            _interceptors = new Dictionary<MemberToken, IInterceptor[]>();
        }

        /// <summary>
        /// Applies all interceptors for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="inherit">A value indicating whether to search the type's inheritance chain to find interception behaviors.</param>
        /// <param name="interceptors">The interceptors.</param>
        public void ApplyInterceptors(Type type, bool inherit, IEnumerable<IInterceptor> interceptors)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (interceptors == null)
                throw new ArgumentNullException("interceptors");

            // Apply type interception behaviors.
            var typeInterceptors = ApplyInterceptionBehaviors(type, inherit, interceptors);

            // Apply event interception behaviors.
            var eventVisitor = Visitor.Create<EventInfo>(e => ApplyInterceptors(e, inherit, typeInterceptors));

            type.VisitEvents(eventVisitor);

            // Apply property interception behaviors.
            var propertyVisitor = Visitor.Create<PropertyInfo>(p => ApplyInterceptors(p, inherit, typeInterceptors));

            type.VisitProperties(propertyVisitor);

            // Apply method interception behaviors.
            var methodVisitor = Visitor.Create<MethodInfo>(m => ApplyInterceptors(m, inherit, typeInterceptors));

            type.VisitMethods(methodVisitor);
        }

        /// <summary>
        /// Applies all interceptors for the specified event.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <param name="inherit">A value indicating whether to search the event's inheritance chain to find interception behaviors.</param>
        /// <param name="interceptors">The interceptors.</param>
        private void ApplyInterceptors(EventInfo eventInfo, bool inherit, IEnumerable<IInterceptor> interceptors)
        {
            var eventInterceptors = ApplyInterceptionBehaviors(eventInfo, inherit, interceptors);

            foreach (var methodInfo in eventInfo.GetAccessorMethods())
            {
                ApplyInterceptors(methodInfo, inherit, eventInterceptors);
            }
        }

        /// <summary>
        /// Applies all interceptors for the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="inherit">A value indicating whether to search the property's inheritance chain to find interception behaviors.</param>
        /// <param name="interceptors">The interceptors.</param>
        private void ApplyInterceptors(PropertyInfo propertyInfo, bool inherit, IEnumerable<IInterceptor> interceptors)
        {
            var propertyInterceptors = ApplyInterceptionBehaviors(propertyInfo, inherit, interceptors);

            foreach (var methodInfo in propertyInfo.GetAccessorMethods())
            {
                ApplyInterceptors(methodInfo, inherit, propertyInterceptors);
            }
        }

        /// <summary>
        /// Applies all interceptors for the specified method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="inherit">A value indicating whether to search the method's inheritance chain to find interception behaviors.</param>
        /// <param name="interceptors">The interceptors.</param>
        private void ApplyInterceptors(MethodInfo methodInfo, bool inherit, IEnumerable<IInterceptor> interceptors)
        {
            var methodInterceptors = ApplyInterceptionBehaviors(methodInfo, inherit, interceptors);

            if (methodInterceptors.Count == 0)
                return;

            methodInterceptors.AddRange(_defaultInterceptors);

            var memberToken = methodInfo.GetToken();

            _interceptors.Add(memberToken, methodInterceptors.ToArray());
        }

        /// <summary>
        /// Applies the interception behaviors for the specified member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="inherit">A value indicating whether to search the member's inheritance chain to find interception behaviors.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>The member interceptors.</returns>
        private static List<IInterceptor> ApplyInterceptionBehaviors(MemberInfo memberInfo, bool inherit, IEnumerable<IInterceptor> interceptors)
        {
            var interceptionBehaviors = memberInfo.GetCustomAttributes<IInterceptionBehavior>(inherit);
            var memberInterceptors = new List<IInterceptor>(interceptors);

            foreach (var interceptionBehavior in interceptionBehaviors)
            {
                interceptionBehavior.Validate(memberInfo);
                interceptionBehavior.Apply(memberInfo, memberInterceptors);
            }

            return memberInterceptors;
        }

        /// <summary>
        /// Returns all interceptors for the specified member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The interceptors.</returns>
        private IInterceptor[] GetInterceptors(MemberInfo memberInfo)
        {
            var memberToken = memberInfo.GetToken();
            IInterceptor[] interceptors;

            return _interceptors.TryGetValue(memberToken, out interceptors) ? interceptors : _defaultInterceptors;
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