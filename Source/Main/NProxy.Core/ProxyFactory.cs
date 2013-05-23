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
using System.Reflection;
using NProxy.Core.Internal.Builders;
using NProxy.Core.Internal.Caching;
using NProxy.Core.Internal.Descriptors;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core
{
    /// <summary>
    /// Represents the proxy factory.
    /// </summary>
    public sealed class ProxyFactory : IProxyFactory
    {
        /// <summary>
        /// The <see cref="ProxyAttribute"/> constructor information.
        /// </summary>
        private static readonly ConstructorInfo ProxyAttributeConstructorInfo = typeof (ProxyAttribute).GetConstructor(
            BindingFlags.Public | BindingFlags.Instance);

        /// <summary>
        /// The type builder factory.
        /// </summary>
        private readonly ITypeBuilderFactory _typeBuilderFactory;

        /// <summary>
        /// The interception filter.
        /// </summary>
        private readonly IInterceptionFilter _interceptionFilter;

        /// <summary>
        /// The cache.
        /// </summary>
        private readonly ICache<IProxyDescriptor, IProxy> _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        public ProxyFactory()
            : this(new ProxyTypeBuilderFactory(true, false), new DefaultInterceptionFilter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        /// <param name="typeBuilderFactory">The type builder factory.</param>
        /// <param name="interceptionFilter">The interception filter.</param>
        internal ProxyFactory(ITypeBuilderFactory typeBuilderFactory, IInterceptionFilter interceptionFilter)
        {
            if (typeBuilderFactory == null)
                throw new ArgumentNullException("typeBuilderFactory");

            if (interceptionFilter == null)
                throw new ArgumentNullException("interceptionFilter");

            _typeBuilderFactory = typeBuilderFactory;
            _interceptionFilter = interceptionFilter;

            _cache = new InterlockedCache<IProxyDescriptor, IProxy>();
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

        /// <summary>
        /// Generates a proxy.
        /// </summary>
        /// <param name="proxyDescriptor">The proxy descriptor.</param>
        /// <returns>The proxy type.</returns>
        private IProxy GenerateProxy(IProxyDescriptor proxyDescriptor)
        {
            var typeBuilder = _typeBuilderFactory.CreateBuilder(proxyDescriptor.ParentType);

            // Add custom attribute.
            typeBuilder.AddCustomAttribute(ProxyAttributeConstructorInfo);

            // Build type.
            var typeVisitor = new TypeBuilderAdapter(typeBuilder, _interceptionFilter);

            proxyDescriptor.Accept(typeVisitor);

            // Create type.
            var type = typeBuilder.CreateType();

            return new Proxy(proxyDescriptor, type);
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

            // Get or generate proxy.
            return _cache.GetOrAdd(proxyDescriptor, GenerateProxy);
        }

        /// <inheritdoc/>
        public IProxy<T> CreateProxy<T>(IEnumerable<Type> interfaceTypes) where T : class
        {
            var proxy = CreateProxy(typeof (T), interfaceTypes);

            return new Proxy<T>(proxy);
        }

        #endregion
    }
}