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
using System.Reflection;

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Represents an invocation context.
    /// </summary>
    internal sealed class InvocationContext : IInvocationContext
    {
        /// <summary>
        /// The target object.
        /// </summary>
        private readonly object _target;

        /// <summary>
        /// The method information.
        /// </summary>
        private readonly MethodInfo _methodInfo;

        /// <summary>
        /// The parameter values.
        /// </summary>
        private readonly object[] _parameters;

        /// <summary>
        /// The interceptors.
        /// </summary>
        private readonly IInterceptor[] _interceptors;

        /// <summary>
        /// The index of the next interceptor.
        /// </summary>
        private int _nextInterceptorIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationContext"/> class.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="parameters">The parameter values.</param>
        /// <param name="interceptors">The interceptors.</param>
        public InvocationContext(object target, MethodInfo methodInfo, object[] parameters, IInterceptor[] interceptors)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            if (parameters == null)
                throw new ArgumentNullException("parameters");

            if (interceptors == null)
                throw new ArgumentNullException("interceptors");

            _target = target;
            _methodInfo = methodInfo;
            _parameters = parameters;
            _interceptors = interceptors;

            _nextInterceptorIndex = 0;
        }

        /// <summary>
        /// Returns the next interceptor.
        /// </summary>
        /// <returns>The next interceptor.</returns>
        private IInterceptor GetNextInterceptor()
        {
            if (_nextInterceptorIndex >= _interceptors.Length)
                throw new InvalidOperationException("No more interceptors in the interceptor chain");

            return _interceptors [_nextInterceptorIndex++];
        }

        #region IInvocationContext Members

        /// <inheritdoc/>
        public object Target
        {
            get { return _target; }
        }

        /// <inheritdoc/>
        public MethodInfo Method
        {
            get { return _methodInfo; }
        }

        /// <inheritdoc/>
        public object[] Parameters
        {
            get { return _parameters; }
        }

        /// <inheritdoc/>
        public object Proceed()
        {
            var interceptor = GetNextInterceptor();

            return interceptor.Intercept(this);
        }

        #endregion
    }
}
