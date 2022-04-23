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

namespace NProxy.Interceptors.Language
{
    /// <summary>
    /// Defines the <c>Target</c> verb.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    public interface ITarget<T> : IFluent where T : class
    {
        /// <summary>
        /// Specifies a singleton target and returns a new proxy.
        /// </summary>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <returns>The new proxy object.</returns>
        T Target<TTarget>() where TTarget : class, new();

        /// <summary>
        /// Specifies a singleton target and returns a new proxy.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <returns>The new proxy object.</returns>
        T Target(T target);

        /// <summary>
        /// Specifies a singleton target and returns a new proxy.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <returns>The new proxy object.</returns>
        T Target(object target);

        /// <summary>
        /// Specifies a target factory and returns a new proxy.
        /// </summary>
        /// <param name="targetFactory">The target factory.</param>
        /// <returns>The new proxy object.</returns>
        T Target(Func<object, object> targetFactory);

        /// <summary>
        /// Specifies the base class as the target and returns a new proxy.
        /// </summary>
        /// <returns>The new proxy object.</returns>
        T TargetBase();
    }
}