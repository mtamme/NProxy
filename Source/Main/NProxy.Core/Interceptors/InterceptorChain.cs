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
using System.Reflection;

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Represents an interceptor chain.
    /// </summary>
    internal sealed class InterceptorChain : IInvocationHandler
    {
        /// <summary>
        /// The interceptors.
        /// </summary>
        private readonly List<IInterceptor> _interceptors;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptorChain"/> class.
        /// </summary>
        /// <param name="interceptor">The interceptor.</param>
        public InterceptorChain(IInterceptor interceptor)
        {
            if (interceptor == null)
                throw new ArgumentNullException("interceptor");

            _interceptors = new List<IInterceptor> {interceptor};
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptorChain"/> class.
        /// </summary>
        /// <param name="interceptors">The interceptors.</param>
        public InterceptorChain(IEnumerable<IInterceptor> interceptors)
        {
            if (interceptors == null)
                throw new ArgumentNullException("interceptors");

            _interceptors = new List<IInterceptor>(interceptors);
        }

        #region IInvocationHandler Members

        /// <inheritdoc/>
        public object Invoke(object target, MethodInfo methodInfo, object[] parameters)
        {
            var invocationContext = new InvocationContext(target, methodInfo, parameters, _interceptors);

            return invocationContext.Proceed();
        }

        #endregion
    }
}
