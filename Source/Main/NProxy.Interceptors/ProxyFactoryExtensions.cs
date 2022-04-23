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

using NProxy.Core;

namespace NProxy.Interceptors
{
    /// <summary>
    /// Provides <see cref="IProxyFactory"/> extension methods.
    /// </summary>
    public static class ProxyFactoryExtensions
    {
        /// <summary>
        /// Returns a fluent interface for creating a new proxy.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>A fluent interface for creating a new proxy.</returns>
        public static ICreateProxy<T> CreateProxy<T>(this IProxyFactory proxyFactory, params object[] arguments) where T : class
        {
            return new CreateProxy<T>(proxyFactory, arguments);
        }
    }
}