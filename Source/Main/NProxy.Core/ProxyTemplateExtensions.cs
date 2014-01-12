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

namespace NProxy.Core
{
    /// <summary>
    /// Provides <see cref="IProxyTemplate"/> extension methods.
    /// </summary>
    public static class ProxyTemplateExtensions
    {
        /// <summary>
        /// Adapts a proxy to the specified interface type.
        /// </summary>
        /// <typeparam name="TInterface">The interface type.</typeparam>
        /// <param name="proxyTemplate">The proxy template.</param>
        /// <param name="proxy">The proxy object.</param>
        /// <returns>The object, of the specified interface type, to which the proxy object has been adapted.</returns>
        public static TInterface AdaptProxy<TInterface>(this IProxyTemplate proxyTemplate, object proxy) where TInterface : class
        {
            var interfaceType = typeof (TInterface);

            return (TInterface) proxyTemplate.AdaptProxy(interfaceType, proxy);
        }
    }
}