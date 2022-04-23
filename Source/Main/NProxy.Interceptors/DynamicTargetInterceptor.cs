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

namespace NProxy.Interceptors
{
    /// <summary>
    /// Represents a dynamic target interceptor.
    /// </summary>
    [Serializable]
    internal sealed class DynamicTargetInterceptor : IInterceptor
    {
        /// <summary>
        /// The target factory.
        /// </summary>
        private readonly Func<object, object> _targetFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicTargetInterceptor"/> class.
        /// </summary>
        /// <param name="targetFactory">The target factory.</param>
        public DynamicTargetInterceptor(Func<object, object> targetFactory)
        {
            if (targetFactory == null)
                throw new ArgumentNullException("targetFactory");

            _targetFactory = targetFactory;
        }

        #region IInterceptor Members

        /// <inheritdoc/>
        public object Intercept(IInvocationContext invocationContext)
        {
            var methodInfo = invocationContext.Method;
            var target = _targetFactory(invocationContext.Target);

            return methodInfo.Invoke(target, invocationContext.Parameters);
        }

        #endregion
    }
}