//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © Martin Tamme
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
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