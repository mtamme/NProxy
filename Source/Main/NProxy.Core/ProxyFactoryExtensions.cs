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
    /// Provides <see cref="IProxyFactory"/> extension methods.
    /// </summary>
    public static class ProxyFactoryExtensions
    {
        /// <summary>
        /// Returns a proxy template.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <returns>The proxy template.</returns>
        public static IProxyTemplate<T> GetProxyTemplate<T>(this IProxyFactory proxyFactory, IEnumerable<Type> interfaceTypes) where T : class
        {
            var proxyTemplate = proxyFactory.GetProxyTemplate(typeof (T), interfaceTypes);

            return new ProxyTemplate<T>(proxyTemplate);
        }

        /// <summary>
        /// Creates a new proxy.
        /// </summary>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new proxy object.</returns>
        public static object CreateProxy(this IProxyFactory proxyFactory,
            Type declaringType,
            IEnumerable<Type> interfaceTypes,
            IInvocationHandler invocationHandler,
            params object[] arguments)
        {
            if (proxyFactory == null)
                throw new ArgumentNullException("proxyFactory");

            var proxyTemplate = proxyFactory.GetProxyTemplate(declaringType, interfaceTypes);

            return proxyTemplate.CreateProxy(invocationHandler, arguments);
        }

        /// <summary>
        /// Creates a new proxy.
        /// </summary>
        /// <typeparam name="T">The declaring type.</typeparam>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new proxy object.</returns>
        public static T CreateProxy<T>(this IProxyFactory proxyFactory,
            IEnumerable<Type> interfaceTypes,
            IInvocationHandler invocationHandler,
            params object[] arguments) where T : class
        {
            if (proxyFactory == null)
                throw new ArgumentNullException("proxyFactory");

            var proxyTemplate = proxyFactory.GetProxyTemplate<T>(interfaceTypes);

            return proxyTemplate.CreateProxy(invocationHandler, arguments);
        }
    }
}