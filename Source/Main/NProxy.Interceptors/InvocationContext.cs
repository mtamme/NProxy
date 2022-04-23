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
using System.Reflection;
using NProxy.Core.Internal;

namespace NProxy.Interceptors
{
    /// <summary>
    /// Represents an invocation context.
    /// </summary>
    [Serializable]
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
            if (target == null)
                throw new ArgumentNullException("target");

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
        /// Returns the next interceptor in the interceptor chain.
        /// </summary>
        /// <returns>The next interceptor.</returns>
        private IInterceptor GetNextInterceptor()
        {
            if (_nextInterceptorIndex >= _interceptors.Length)
                throw new InvalidOperationException(Resources.NoMoreInterceptorsInTheInterceptorChain);

            return _interceptors[_nextInterceptorIndex++];
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
        public object Data { get; set; }

        /// <inheritdoc/>
        public object Proceed()
        {
            var interceptor = GetNextInterceptor();

            return interceptor.Intercept(this);
        }

        #endregion
    }
}