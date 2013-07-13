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
        /// <param name="defaultInterceptors">The default interceptors.</param>
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
        /// <param name="proxy">The proxy.</param>
        /// <param name="interceptors">The interceptors.</param>
        public void ApplyInterceptors(IProxy proxy, IEnumerable<IInterceptor> interceptors)
        {
            if (proxy == null)
                throw new ArgumentNullException("proxy");

            if (interceptors == null)
                throw new ArgumentNullException("interceptors");

            // Apply type interception behaviors.
            var typeInterceptors = ApplyInterceptionBehaviors(proxy.DeclaringType, interceptors);

            // Apply event interception behaviors.
            foreach (var eventInfo in proxy.InterceptedEvents)
            {
                ApplyInterceptors(eventInfo, typeInterceptors);
            }

            // Apply property interception behaviors.
            foreach (var propertyInfo in proxy.InterceptedProperties)
            {
                ApplyInterceptors(propertyInfo, typeInterceptors);
            }

            // Apply method interception behaviors.
            foreach (var methodInfo in proxy.InterceptedMethods)
            {
                ApplyInterceptors(methodInfo, typeInterceptors);
            }
        }

        /// <summary>
        /// Applies all interceptors for the specified event.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <param name="interceptors">The interceptors.</param>
        private void ApplyInterceptors(EventInfo eventInfo, IEnumerable<IInterceptor> interceptors)
        {
            var eventInterceptors = ApplyInterceptionBehaviors(eventInfo, interceptors);

            foreach (var methodInfo in eventInfo.GetAccessorMethods())
            {
                ApplyInterceptors(methodInfo, eventInterceptors);
            }
        }

        /// <summary>
        /// Applies all interceptors for the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="interceptors">The interceptors.</param>
        private void ApplyInterceptors(PropertyInfo propertyInfo, IEnumerable<IInterceptor> interceptors)
        {
            var propertyInterceptors = ApplyInterceptionBehaviors(propertyInfo, interceptors);

            foreach (var methodInfo in propertyInfo.GetAccessorMethods())
            {
                ApplyInterceptors(methodInfo, propertyInterceptors);
            }
        }

        /// <summary>
        /// Applies all interceptors for the specified member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="interceptors">The interceptors.</param>
        private void ApplyInterceptors(MemberInfo memberInfo, IEnumerable<IInterceptor> interceptors)
        {
            var memberInterceptors = ApplyInterceptionBehaviors(memberInfo, interceptors);

            if (memberInterceptors.Count == 0)
                return;

            memberInterceptors.AddRange(_defaultInterceptors);

            var memberToken = memberInfo.GetToken();

            _interceptors.Add(memberToken, memberInterceptors.ToArray());
        }

        /// <summary>
        /// Applies the interception behaviors for the specified member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>The member interceptors.</returns>
        private static List<IInterceptor> ApplyInterceptionBehaviors(MemberInfo memberInfo, IEnumerable<IInterceptor> interceptors)
        {
            var interceptionBehaviors = memberInfo.GetCustomAttributes<IInterceptionBehavior>(true);
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
        public object Invoke(object target, MethodInfo methodInfo, object[] parameters)
        {
            var interceptors = GetInterceptors(methodInfo);
            var invocationContext = new InvocationContext(target, methodInfo, parameters, interceptors);

            return invocationContext.Proceed();
        }

        #endregion
    }
}