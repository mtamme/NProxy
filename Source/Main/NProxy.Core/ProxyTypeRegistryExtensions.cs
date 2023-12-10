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
using System.Collections.Generic;

namespace NProxy.Core
{
    /// <summary>
    /// Provides <see cref="IProxyTypeRegistry"/> extension methods.
    /// </summary>
    public static class ProxyTypeRegistryExtensions
    {
        /// <summary>
        /// Returns a proxy type.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyTypeRegistry">The proxy type registry.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <returns>The proxy type.</returns>
        public static IProxyType<T> GetProxyType<T>(this IProxyTypeRegistry proxyTypeRegistry, IEnumerable<Type> interfaceTypes) where T : class
        {
            var proxyType = proxyTypeRegistry.GetProxyType(typeof (T), interfaceTypes);

            return new ProxyType<T>(proxyType);
        }

        /// <summary>
        /// Creates a new proxy.
        /// </summary>
        /// <param name="proxyTypeRegistry">The proxy type registry.</param>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new proxy object.</returns>
        public static object CreateProxy(this IProxyTypeRegistry proxyTypeRegistry,
            Type declaringType,
            IEnumerable<Type> interfaceTypes,
            IInvocationHandler invocationHandler,
            params object[] arguments)
        {
            if (proxyTypeRegistry == null)
                throw new ArgumentNullException("proxyFactory");

            var proxyType = proxyTypeRegistry.GetProxyType(declaringType, interfaceTypes);

            return proxyType.CreateProxy(invocationHandler, arguments);
        }

        /// <summary>
        /// Creates a new proxy.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyTypeRegistry">The proxy type registry.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new proxy object.</returns>
        public static T CreateProxy<T>(this IProxyTypeRegistry proxyTypeRegistry,
            IEnumerable<Type> interfaceTypes,
            IInvocationHandler invocationHandler,
            params object[] arguments) where T : class
        {
            if (proxyTypeRegistry == null)
                throw new ArgumentNullException("proxyFactory");

            var proxyType = proxyTypeRegistry.GetProxyType<T>(interfaceTypes);

            return proxyType.CreateProxy(invocationHandler, arguments);
        }
    }
}