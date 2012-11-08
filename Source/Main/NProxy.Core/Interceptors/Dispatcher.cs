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

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Represents a dispatcher.
    /// </summary>
    internal sealed class Dispatcher : IInvocationHandler
    {
        /// <summary>
        /// An empty interceptor array.
        /// </summary>
        private static readonly IInterceptor[] EmptyInterceptors = new IInterceptor[0];

        /// <summary>
        /// The interceptors.
        /// </summary>
        private readonly Dictionary<long, IInterceptor[]> _interceptors;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dispatcher"/> class.
        /// </summary>
        public Dispatcher()
        {
            _interceptors = new Dictionary<long, IInterceptor[]>();
        }

        /// <summary>
        /// Sets the interceptors for the specified method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="interceptors">The interceptors.</param>
        public void SetInterceptors(MethodInfo methodInfo, IEnumerable<IInterceptor> interceptors)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            if (interceptors == null)
                throw new ArgumentNullException("interceptors");

            var methodToken = methodInfo.GetToken();

            _interceptors[methodToken] = interceptors.ToArray();
        }

        /// <summary>
        /// Returns all interceptors for the specified member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The interceptors.</returns>
        private IEnumerable<IInterceptor> GetInterceptors(MemberInfo memberInfo)
        {
            var methodToken = memberInfo.GetToken();
            IInterceptor[] interceptors;

            return _interceptors.TryGetValue(methodToken, out interceptors) ? interceptors : EmptyInterceptors;
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
