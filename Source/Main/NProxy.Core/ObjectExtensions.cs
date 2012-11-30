//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © 2012 Martin Tamme
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
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core
{
    /// <summary>
    /// Represents object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Casts a proxy object to the specified interface type.
        /// </summary>
        /// <typeparam name="TInterface">The interface type.</typeparam>
        /// <param name="proxy">The proxy object.</param>
        /// <returns>The proxy object cast to the specified interface type.</returns>
        public static TInterface Cast<TInterface>(this object proxy) where TInterface : class
        {
            if (proxy == null)
                throw new ArgumentNullException("proxy");

            var interfaceType = typeof (TInterface);

            if (!interfaceType.IsInterface)
                throw new ArgumentException(String.Format("Type '{0}' is not an interface type", interfaceType));

            var proxyType = proxy.GetType();

            if (proxyType.IsDefined<ProxyAttribute>())
                return (TInterface) proxy;

            var delegateProxy = proxy as Delegate;

            if (delegateProxy == null)
                throw new InvalidOperationException("Object is not a proxy");

            return Cast<TInterface>(delegateProxy.Target);
        }
    }
}
