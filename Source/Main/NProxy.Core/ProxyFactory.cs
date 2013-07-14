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
using NProxy.Core.Internal.Builders;
using NProxy.Core.Internal.Caching;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Internal.Templates;

namespace NProxy.Core
{
    /// <summary>
    /// Represents the proxy factory.
    /// </summary>
    public sealed class ProxyFactory : IProxyFactory
    {
        /// <summary>
        /// The type builder factory.
        /// </summary>
        private readonly ITypeBuilderFactory _typeBuilderFactory;

        /// <summary>
        /// The interception filter.
        /// </summary>
        private readonly IInterceptionFilter _interceptionFilter;

        /// <summary>
        /// The proxy cache.
        /// </summary>
        private readonly ICache<IProxyTemplate, IProxy> _proxyCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        public ProxyFactory()
            : this(new ProxyTypeBuilderFactory(true, false), new AttributeInterceptionFilter())
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

            _proxyCache = new InterlockedCache<IProxyTemplate, IProxy>();
        }

        /// <summary>
        /// Returns a proxy template for the specified declaring type and interface types.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        /// <returns>The proxy template.</returns>
        private static IProxyTemplate CreateProxyTemplate(Type declaringType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType.IsDelegate())
                return new DelegateProxyTemplate(declaringType, interfaceTypes);

            if (declaringType.IsInterface)
                return new InterfaceProxyTemplate(declaringType, interfaceTypes);

            return new ClassProxyTemplate(declaringType, interfaceTypes);
        }

        /// <summary>
        /// Generates a proxy.
        /// </summary>
        /// <param name="proxyTemplate">The proxy template.</param>
        /// <returns>The proxy.</returns>
        private IProxy GenerateProxy(IProxyTemplate proxyTemplate)
        {
            var typeBuilder = _typeBuilderFactory.CreateBuilder(proxyTemplate.ParentType);
            var proxyGenerator = new ProxyGenerator(typeBuilder, _interceptionFilter);

            return proxyGenerator.GenerateProxy(proxyTemplate);
        }

        #region IProxyFactory Members

        /// <inheritdoc/>
        public IProxy CreateProxy(Type declaringType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");

            if (interfaceTypes == null)
                throw new ArgumentNullException("interfaceTypes");

            // Create proxy template.
            var proxyTemplate = CreateProxyTemplate(declaringType, interfaceTypes);

            // Get or generate proxy.
            return _proxyCache.GetOrAdd(proxyTemplate, GenerateProxy);
        }

        #endregion
    }
}