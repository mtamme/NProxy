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
using NProxy.Core.Internal.Caching;
using NProxy.Core.Internal.Definitions;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Internal.Reflection.Emit;

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
        /// The proxy template cache.
        /// </summary>
        private readonly ICache<IProxyDefinition, IProxyTemplate> _proxyTemplateCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        public ProxyFactory()
            : this(new NonInterceptedInterceptionFilter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        /// <param name="interceptionFilter">The interception filter.</param>
        /// <param name="options">The options object.</param>
        public ProxyFactory(IInterceptionFilter interceptionFilter)
            : this(new ProxyTypeBuilderFactory(false), interceptionFilter)
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

            _proxyTemplateCache = new LockOnWriteCache<IProxyDefinition, IProxyTemplate>();
        }

        /// <summary>
        /// Creates a proxy definition for the specified declaring type and interface types.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        /// <returns>The proxy definition.</returns>
        private static IProxyDefinition CreateProxyDefinition(Type declaringType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType.IsDelegate())
                return new DelegateProxyDefinition(declaringType, interfaceTypes);

            if (declaringType.IsInterface)
                return new InterfaceProxyDefinition(declaringType, interfaceTypes);

            return new ClassProxyDefinition(declaringType, interfaceTypes);
        }

        /// <summary>
        /// Generates a proxy template.
        /// </summary>
        /// <param name="proxyDefinition">The proxy definition.</param>
        /// <returns>The proxy template.</returns>
        private IProxyTemplate GenerateProxyTemplate(IProxyDefinition proxyDefinition)
        {
            var typeBuilder = _typeBuilderFactory.CreateBuilder(proxyDefinition.ParentType);
            var proxyGenerator = new ProxyGenerator(typeBuilder, _interceptionFilter);

            return proxyGenerator.GenerateProxyTemplate(proxyDefinition);
        }

        #region IProxyFactory Members

        /// <inheritdoc/>
        public IProxyTemplate GetProxyTemplate(Type declaringType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");

            if (interfaceTypes == null)
                throw new ArgumentNullException("interfaceTypes");

            // Create proxy definition.
            var proxyDefinition = CreateProxyDefinition(declaringType, interfaceTypes);

            // Get or generate proxy template.
            return _proxyTemplateCache.GetOrAdd(proxyDefinition, GenerateProxyTemplate);
        }

        public Type GenerateProxyType(Type declaringType,
          IEnumerable<Type> interfaceTypes,
          Type invocationHandlerFactoryType)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");

            if (interfaceTypes == null)
                throw new ArgumentNullException("interfaceTypes");

            if (!typeof(IInvocationHandlerFactory).IsAssignableFrom(invocationHandlerFactoryType))
                throw new ArgumentException("invocationHandlerFactoryType must be of type IInvocationHandlerFactory");
            if (!invocationHandlerFactoryType.IsPublic && !invocationHandlerFactoryType.IsNestedPublic)
                throw new ArgumentException("invocationHandlerFactoryType must be public");

            // Create proxy definition.
            var proxyDefinition = CreateProxyDefinition(declaringType, interfaceTypes);

            var typeBuilder = _typeBuilderFactory.CreateBuilder(proxyDefinition.ParentType);
            var proxyGenerator = new ProxyGenerator(typeBuilder, _interceptionFilter);
            proxyGenerator.invocationHandlerFactoryType = invocationHandlerFactoryType;

            return proxyGenerator.GenerateProxyType(proxyDefinition);
        }

        #endregion
    }
}