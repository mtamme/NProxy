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

namespace NProxy.Core
{
    /// <summary>
    /// Provides <see cref="IProxyType"/> extension methods.
    /// </summary>
    public static class ProxyTypeExtensions
    {
        /// <summary>
        /// Adapts a proxy to the specified interface type.
        /// </summary>
        /// <typeparam name="TInterface">The interface type.</typeparam>
        /// <param name="proxyType">The proxy type.</param>
        /// <param name="proxy">The proxy object.</param>
        /// <returns>The object, of the specified interface type, to which the proxy object has been adapted.</returns>
        public static TInterface AdaptProxy<TInterface>(this IProxyType proxyType, object proxy) where TInterface : class
        {
            var interfaceType = typeof (TInterface);

            return (TInterface) proxyType.AdaptProxy(interfaceType, proxy);
        }
    }
}