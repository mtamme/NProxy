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

using System.Reflection;

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Defines an invocation context.
    /// </summary>
    public interface IInvocationContext
    {
        /// <summary>
        /// Returns the target object. 
        /// </summary>
        object Target { get; }

        /// <summary>
        /// Returns the method of the target class for which the interceptor was invoked.
        /// </summary>
        MethodInfo Method { get; }

        /// <summary>
        /// Returns the parameter values that will be passed to the method of the target class.
        /// </summary>
        object[] Parameters { get; }

        /// <summary>
        /// Gets or sets the data object associated with the invocation.
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// Proceed to the next interceptor in the interceptor chain.
        /// </summary>
        /// <returns>The return value of the next method in the chain.</returns>
        object Proceed();
    }
}