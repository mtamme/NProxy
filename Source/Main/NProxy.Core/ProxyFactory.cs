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
using NProxy.Core.Internal.Generators;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core
{
    /// <summary>
    /// Represents a proxy factory.
    /// </summary>
    public sealed class ProxyFactory : IProxyFactory
    {
        /// <summary>
        /// The type provider.
        /// </summary>
        private readonly ITypeProvider<IProxyDescriptor> _typeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        public ProxyFactory()
            : this(new ProxyTypeBuilderFactory(true, false))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        /// <param name="typeBuilderFactory">The type builder factory.</param>
        internal ProxyFactory(ITypeBuilderFactory typeBuilderFactory)
        {
            var typeProvider = new ProxyTypeGenerator(typeBuilderFactory, new DefaultInterceptionFilter());

            _typeProvider = new TypeCache<IProxyDescriptor, IProxyDescriptor>(d => d, typeProvider);
        }

        /// <summary>
        /// Returns a proxy descriptor for the specified declaring type.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        /// <returns>The type definition.</returns>
        private static IProxyDescriptor CreateDescriptor(Type declaringType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType.IsDelegate())
                return new DelegateProxyDescriptor(declaringType, interfaceTypes);

            if (declaringType.IsInterface)
                return new InterfaceProxyDescriptor(declaringType, interfaceTypes);

            return new ClassProxyDescriptor(declaringType, interfaceTypes);
        }

        #region IProxyFactory Members

        /// <inheritdoc/>
        public IProxy CreateProxy(Type declaringType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");

            if (interfaceTypes == null)
                throw new ArgumentNullException("interfaceTypes");

            // Create proxy descriptor.
            var proxyDescriptor = CreateDescriptor(declaringType, interfaceTypes);

            // Get type.
            var type = _typeProvider.GetType(proxyDescriptor);

            return new Proxy(proxyDescriptor, type);
        }

        /// <inheritdoc/>
        public IProxy<T> CreateProxy<T>(IEnumerable<Type> interfaceTypes) where T : class
        {
            if (interfaceTypes == null)
                throw new ArgumentNullException("interfaceTypes");

            // Create proxy descriptor.
            var proxyDescriptor = CreateDescriptor(typeof (T), interfaceTypes);

            // Get type.
            var type = _typeProvider.GetType(proxyDescriptor);

            return new Proxy<T>(proxyDescriptor, type);
        }

        #endregion
    }
}