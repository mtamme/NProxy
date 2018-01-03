//
// Copyright © Martin Tamme
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Applies all interceptors for the specified proxy template.
        /// </summary>
        /// <param name="proxyTemplate">The proxy template.</param>
        /// <param name="interceptors">The interceptors.</param>
        public void ApplyInterceptors(IProxyTemplate proxyTemplate, IEnumerable<IInterceptor> interceptors)
        {
            if (proxyTemplate == null)
                throw new ArgumentNullException("proxyTemplate");

            if (interceptors == null)
                throw new ArgumentNullException("interceptors");

            // Apply type interception behaviors.
            var typeInterceptors = ApplyInterceptionBehaviors(proxyTemplate.DeclaringType, interceptors);

            // Apply event interception behaviors.
            foreach (var eventInfo in proxyTemplate.InterceptedEvents)
            {
                ApplyInterceptors(eventInfo, typeInterceptors);
            }

            // Apply property interception behaviors.
            foreach (var propertyInfo in proxyTemplate.InterceptedProperties)
            {
                ApplyInterceptors(propertyInfo, typeInterceptors);
            }

            // Apply method interception behaviors.
            foreach (var methodInfo in proxyTemplate.InterceptedMethods)
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

            foreach (var methodInfo in eventInfo.GetMethods())
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

            foreach (var methodInfo in propertyInfo.GetMethods())
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

            var memberToken = new MemberToken(memberInfo);

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
            var customAttributes = memberInfo.GetCustomAttributes(true);
            var memberInterceptors = new List<IInterceptor>(interceptors);

            foreach (var interceptionBehavior in customAttributes.OfType<IInterceptionBehavior>())
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
            var memberToken = new MemberToken(memberInfo);
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