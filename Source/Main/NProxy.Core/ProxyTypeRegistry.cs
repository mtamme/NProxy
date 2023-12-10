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
using NProxy.Core.Internal;
using NProxy.Core.Internal.Caching;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Internal.Reflection.Emit;

namespace NProxy.Core
{
    /// <summary>
    /// Represents the proxy type registry.
    /// </summary>
    public sealed class ProxyTypeRegistry : IProxyTypeRegistry
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
        /// The proxy type cache.
        /// </summary>
        private readonly ICache<IProxyInfo, IProxyType> _proxyTypeCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyTypeRegistry"/> class.
        /// </summary>
        public ProxyTypeRegistry()
            : this(new NonInterceptedInterceptionFilter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyTypeRegistry"/> class.
        /// </summary>
        /// <param name="interceptionFilter">The interception filter.</param>
        public ProxyTypeRegistry(IInterceptionFilter interceptionFilter)
            : this(new ProxyTypeBuilderFactory(), interceptionFilter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyTypeRegistry"/> class.
        /// </summary>
        /// <param name="typeBuilderFactory">The type builder factory.</param>
        /// <param name="interceptionFilter">The interception filter.</param>
        internal ProxyTypeRegistry(ITypeBuilderFactory typeBuilderFactory, IInterceptionFilter interceptionFilter)
        {
            if (typeBuilderFactory == null)
                throw new ArgumentNullException("typeBuilderFactory");

            if (interceptionFilter == null)
                throw new ArgumentNullException("interceptionFilter");

            _typeBuilderFactory = typeBuilderFactory;
            _interceptionFilter = interceptionFilter;

            _proxyTypeCache = new LockOnWriteCache<IProxyInfo, IProxyType>();
        }

        /// <summary>
        /// Creates a proxy information for the specified declaring type and interface types.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        /// <returns>The proxy information.</returns>
        private static IProxyInfo CreateProxyInfo(Type declaringType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType.IsDelegate())
                return new DelegateProxyInfo(declaringType, interfaceTypes);

            if (declaringType.IsInterface)
                return new InterfaceProxyInfo(declaringType, interfaceTypes);

            return new ClassProxyInfo(declaringType, interfaceTypes);
        }

        /// <summary>
        /// Creates a proxy type.
        /// </summary>
        /// <param name="proxyInfo">The proxy information.</param>
        /// <returns>The proxy type.</returns>
        private IProxyType CreateProxyType(IProxyInfo proxyInfo)
        {
            var typeBuilder = _typeBuilderFactory.CreateBuilder(proxyInfo.ParentType);
            var proxyTypeFactory = new ProxyTypeFactory(typeBuilder, _interceptionFilter);

            return proxyTypeFactory.CreateProxyType(proxyInfo);
        }

        #region IProxyTypeRegistry Members

        /// <inheritdoc/>
        public IProxyType GetProxyType(Type declaringType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");

            if (interfaceTypes == null)
                throw new ArgumentNullException("interfaceTypes");

            // Create proxy information.
            var proxyInfo = CreateProxyInfo(declaringType, interfaceTypes);

            // Get or generate proxy type.
            return _proxyTypeCache.GetOrAdd(proxyInfo, CreateProxyType);
        }

        #endregion
    }
}