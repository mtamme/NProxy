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
using NProxy.Core.Internal.Descriptors;

namespace NProxy.Core
{
    /// <summary>
    /// Represents a proxy.
    /// </summary>
    internal class Proxy : IProxy
    {
        /// <summary>
        /// The proxy descriptor.
        /// </summary>
        private readonly IProxyDescriptor _proxyDescriptor;

        /// <summary>
        /// The type.
        /// </summary>
        private readonly Type _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Proxy"/> class.
        /// </summary>
        /// <param name="proxyDescriptor">The proxy descriptor.</param>
        /// <param name="type">The type.</param>
        public Proxy(IProxyDescriptor proxyDescriptor, Type type)
        {
            if (proxyDescriptor == null)
                throw new ArgumentNullException("proxyDescriptor");

            if (type == null)
                throw new ArgumentNullException("type");

            _proxyDescriptor = proxyDescriptor;
            _type = type;
        }

        #region IProxy Members

        /// <inheritdoc/>
        public Type DeclaringType
        {
            get { return _proxyDescriptor.DeclaringType; }
        }

        /// <inheritdoc/>
        public object CreateInstance(IInvocationHandler invocationHandler, params object[] arguments)
        {
            if (invocationHandler == null)
                throw new ArgumentNullException("invocationHandler");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            var constructorArguments = new List<object> {invocationHandler};

            constructorArguments.AddRange(arguments);

            return _proxyDescriptor.CreateInstance(_type, constructorArguments.ToArray());
        }

        #endregion
    }

    /// <summary>
    /// Represents a proxy.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    internal sealed class Proxy<T> : Proxy, IProxy<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Proxy{T}"/> class.
        /// </summary>
        /// <param name="proxyDescriptor">The proxy descriptor.</param>
        /// <param name="type">The type.</param>
        public Proxy(IProxyDescriptor proxyDescriptor, Type type)
            : base(proxyDescriptor, type)
        {
        }

        #region IProxy<T> Members

        /// <inheritdoc/>
        public new T CreateInstance(IInvocationHandler invocationHandler, params object[] arguments)
        {
            return (T) base.CreateInstance(invocationHandler, arguments);
        }

        #endregion
    }
}