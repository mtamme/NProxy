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
using System.Collections.Generic;
using System.Linq;
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
        /// Creates a proxy for the specified declaring type.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The proxy.</returns>
        private object InternalCreateProxy(Type declaringType,
                                           IEnumerable<Type> interfaceTypes,
                                           IInvocationHandler invocationHandler,
                                           IEnumerable<object> arguments)
        {
            // Create type definition.
            var typeDefinition = CreateTypeDefinition(declaringType);

            // Add interceptable attribute.
            typeDefinition.AddCustomAttribute(new AttributeInfo(typeof (InterceptableAttribute)));

            // Add interface types.
            foreach (var interfaceType in interfaceTypes)
            {
                typeDefinition.AddInterface(interfaceType);
            }

            // Get proxy type.
            var proxyType = _typeProvider.GetType(typeDefinition);

            // Create proxy instance.
            var constructorArguments = new List<object> {invocationHandler};

            if (arguments != null)
                constructorArguments.AddRange(arguments);

            return typeDefinition.CreateInstance(proxyType, constructorArguments.ToArray());
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

            return InternalCreateProxy(declaringType, interfaceTypes, invocationHandler, arguments);
        }

        /// <inheritdoc/>
        public T CreateProxy<T>(IEnumerable<Type> interfaceTypes,
                                IInvocationHandler invocationHandler,
                                params object[] arguments) where T : class
        {
            if (interfaceTypes == null)
                throw new ArgumentNullException("interfaceTypes");

            if (invocationHandler == null)
                throw new ArgumentNullException("invocationHandler");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            return (T) InternalCreateProxy(typeof (T), interfaceTypes, invocationHandler, arguments);
        }

        /// <inheritdoc/>
        public TInterface AdaptProxy<TInterface>(object proxy) where TInterface : class
        {
            if (proxy == null)
                throw new ArgumentNullException("proxy");

            var interfaceType = typeof (TInterface);

            if (!interfaceType.IsInterface)
                throw new ArgumentException(String.Format("Type '{0}' is not an interface type", interfaceType));

            var proxyType = proxy.GetType();

            if (proxyType.HasCustomAttribute<InterceptableAttribute>())
                return proxy as TInterface;

            var delegateProxy = proxy as Delegate;

            if (delegateProxy == null)
                throw new ArgumentException("Object is not a proxy", "proxy");

            return AdaptProxy<TInterface>(delegateProxy.Target);
        }

        #endregion
    }
}
