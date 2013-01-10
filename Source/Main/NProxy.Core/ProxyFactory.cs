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
using NProxy.Core.Internal.Definitions;
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
        private readonly ITypeProvider<ITypeDefinition> _typeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        public ProxyFactory()
            : this(new ProxyTypeBuilderFactory(false))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        /// <param name="typeBuilderFactory">The type builder factory.</param>
        internal ProxyFactory(ITypeBuilderFactory typeBuilderFactory)
            : this(new ProxyTypeGenerator(typeBuilderFactory, new DefaultInterceptionFilter()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyFactory"/> class.
        /// </summary>
        /// <param name="typeProvider">The type provider.</param>
        private ProxyFactory(ITypeProvider<ITypeDefinition> typeProvider)
        {
            _typeProvider = new TypeCache<ITypeDefinition>(typeProvider);
        }

        /// <summary>
        /// Returns a type definition for the specified declaring type.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <returns>The type definition.</returns>
        private static TypeDefinitionBase CreateTypeDefinition(Type declaringType)
        {
            if (declaringType.IsDelegate())
                return new DelegateTypeDefinition(declaringType);

            if (declaringType.IsInterface)
                return new InterfaceTypeDefinition(declaringType);

            return new ClassTypeDefinition(declaringType);
        }

        #region IProxyFactory Members

        /// <inheritdoc/>
        public object CreateProxy(Type declaringType,
                                  IEnumerable<Type> interfaceTypes,
                                  IInvocationHandler invocationHandler,
                                  params object[] arguments)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");

            if (interfaceTypes == null)
                throw new ArgumentNullException("interfaceTypes");

            if (invocationHandler == null)
                throw new ArgumentNullException("invocationHandler");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            // Create type definition.
            var typeDefinition = CreateTypeDefinition(declaringType);

            // Add interface types.
            foreach (var interfaceType in interfaceTypes)
            {
                typeDefinition.AddInterface(interfaceType);
            }

            // Get type.
            var type = _typeProvider.GetType(typeDefinition);

            // Create instance.
            var constructorArguments = new List<object> {invocationHandler};

            constructorArguments.AddRange(arguments);

            return typeDefinition.CreateInstance(type, constructorArguments.ToArray());
        }

        #endregion
    }
}
