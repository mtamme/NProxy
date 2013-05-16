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
using NProxy.Core.Internal.Common;
using NProxy.Core.Internal.Descriptors;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core.Internal.Generators
{
    /// <summary>
    /// Represents a proxy type generator.
    /// </summary>
    internal sealed class ProxyTypeGenerator : ITypeProvider<IProxyDescriptor>
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
        /// Initializes a new instance of the <see cref="ProxyTypeGenerator"/> class.
        /// </summary>
        /// <param name="typeBuilderFactory">The type builder factory.</param>
        /// <param name="interceptionFilter">The interception filter.</param>
        public ProxyTypeGenerator(ITypeBuilderFactory typeBuilderFactory, IInterceptionFilter interceptionFilter)
        {
            if (typeBuilderFactory == null)
                throw new ArgumentNullException("typeBuilderFactory");

            if (interceptionFilter == null)
                throw new ArgumentNullException("interceptionFilter");

            _typeBuilderFactory = typeBuilderFactory;
            _interceptionFilter = interceptionFilter;
        }

        #region ITypeProvider<IProxyDescriptor> Members

        /// <inheritdoc/>
        public Type GetType(IProxyDescriptor proxyDescriptor)
        {
            if (proxyDescriptor == null)
                throw new ArgumentNullException("proxyDescriptor");

            var typeBuilder = _typeBuilderFactory.CreateBuilder(proxyDescriptor.ParentType);

            // Add custom attribute.
            typeBuilder.AddCustomAttribute(ProxyAttributeConstructorInfo);

            // Create type reflector.
            var typeReflector = proxyDescriptor.CreateReflector();

            // Add interfaces.
            var addInterfaceVisitor = Visitor.Create<Type>(typeBuilder.AddInterface);

            typeReflector.VisitInterfaces(addInterfaceVisitor);

            // Build constructors.
            var buildConstructorVisitor = Visitor.Create<ConstructorInfo>(typeBuilder.BuildConstructor);

            typeReflector.VisitConstructors(buildConstructorVisitor);

            // Build events.
            var interceptedEventInfos = new List<EventInfo>();
            var buildEventVisitor = Visitor.Create<EventInfo>(interceptedEventInfos.Add)
                                           .Do(typeBuilder.BuildEvent)
                                           .Where(_interceptionFilter.Accept);

            typeReflector.VisitEvents(buildEventVisitor);

            // Build properties.
            var interceptedPropertyInfos = new List<PropertyInfo>();
            var buildPropertyVisitor = Visitor.Create<PropertyInfo>(interceptedPropertyInfos.Add)
                                              .Do(typeBuilder.BuildProperty)
                                              .Where(_interceptionFilter.Accept);

            typeReflector.VisitProperties(buildPropertyVisitor);

            // Build methods.
            var interceptedMethodInfos = new List<MethodInfo>();
            var buildMethodVisitor = Visitor.Create<MethodInfo>(interceptedMethodInfos.Add)
                                            .Do(typeBuilder.BuildMethod)
                                            .Where(_interceptionFilter.Accept);

            typeReflector.VisitMethods(buildMethodVisitor);

            return typeBuilder.CreateType();
        }

        #endregion
    }
}