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
    /// Represents a base target interceptor.
    /// </summary>
    [Serializable]
    internal sealed class BaseTargetInterceptor : IInterceptor
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        public static readonly BaseTargetInterceptor Instance = new BaseTargetInterceptor();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTargetInterceptor"/> class.
        /// </summary>
        private BaseTargetInterceptor()
        {
        }

        #region IInterceptor Members

        /// <inheritdoc/>
        public object Intercept(IInvocationContext invocationContext)
        {
            var methodInfo = invocationContext.Method;

            return methodInfo.Invoke(invocationContext.Target, invocationContext.Parameters);
        }

        #endregion
    }
}