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
    /// Represents a static target interceptor.
    /// </summary>
    [Serializable]
    internal sealed class StaticTargetInterceptor : IInterceptor
    {
        /// <summary>
        /// The target object.
        /// </summary>
        private readonly object _target;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticTargetInterceptor"/> class.
        /// </summary>
        /// <param name="target">The target object.</param>
        public StaticTargetInterceptor(object target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            _target = target;
        }

        #region IInterceptor Members

        /// <inheritdoc/>
        public object Intercept(IInvocationContext invocationContext)
        {
            var methodInfo = invocationContext.Method;

            return methodInfo.Invoke(_target, invocationContext.Parameters);
        }

        #endregion
    }
}