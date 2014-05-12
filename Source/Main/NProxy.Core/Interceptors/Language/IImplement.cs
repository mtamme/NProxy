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

namespace NProxy.Core.Interceptors.Language
{
    /// <summary>
    /// Defines the <c>Implement</c> verb.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    /// <typeparam name="TInterceptor">The interceptor type.</typeparam>
    public interface IImplement<T, in TInterceptor> : IInterceptBy<T, TInterceptor> where T : class
    {
        /// <summary>
        /// Specifies an interface to implement.
        /// </summary>
        /// <typeparam name="TInterface">The interface type.</typeparam>
        /// <returns>The <c>Implement</c> verb.</returns>
        IImplement<T, TInterceptor> Implement<TInterface>() where TInterface : class;

        /// <summary>
        /// Specifies interfaces to implement.
        /// </summary>
        /// <param name="interfaceTypes">The interface types.</param>
        /// <returns>The <c>Implement</c> verb.</returns>
        IImplement<T, TInterceptor> Implement(params Type[] interfaceTypes);
    }
}