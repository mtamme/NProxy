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

namespace NProxy.Core
{
    /// <summary>
    /// Defines an invocation handler.
    /// </summary>
    public interface IInvocationHandler
    {
        /// <summary>
        /// Processes an invocation on a target.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="parameters">The parameter values.</param>
        /// <returns>The return value.</returns>
        object Invoke(object target, MethodInfo methodInfo, object[] parameters);
    }

    public interface IProxyObject
    {
        IInvocationHandler _GetInvocationHandler();
    }
}