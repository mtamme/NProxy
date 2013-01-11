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
using System.Reflection;
using NProxy.Core.Internal.Common;
using NProxy.Core.Internal.Definitions;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core.Internal.Generators
{
    /// <summary>
    /// Represents a proxy type generator.
    /// </summary>
    internal sealed class ProxyTypeGenerator : ITypeProvider<ITypeDefinition>
    {
        /// <summary>
        /// An empty object array.
        /// </summary>
        private static readonly object[] EmptyObjects = new object[0];

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

        #region ITypeProvider<ITypeDefinition> Members

        /// <inheritdoc/>
        public Type GetType(ITypeDefinition typeDefinition)
        {
            if (typeDefinition == null)
                throw new ArgumentNullException("typeDefinition");

            var typeBuilder = _typeBuilderFactory.CreateBuilder(typeDefinition.ParentType);

            // Add custom attribute.
            typeBuilder.AddCustomAttribute(typeof (ProxyAttribute), Type.EmptyTypes, EmptyObjects);

            // Add interfaces.
            var addInterfaceVisitor = Visitor.Create<Type>(typeBuilder.AddInterface);

            typeDefinition.VisitInterfaces(addInterfaceVisitor);

            // Build constructors.
            var buildConstructorVisitor = Visitor.Create<ConstructorInfo>(typeBuilder.BuildConstructor);

            typeDefinition.VisitConstructors(buildConstructorVisitor);

            // Build events.
            var buildEventVisitor = Visitor.Create<EventInfo>(typeBuilder.BuildEvent)
                .Where(e => e.IsAbstract() || _interceptionFilter.Accept(e))
                .Where(e => e.CanOverride());

            typeDefinition.VisitEvents(buildEventVisitor);

            // Build properties.
            var buildPropertyVisitor = Visitor.Create<PropertyInfo>(typeBuilder.BuildProperty)
                .Where(p => p.IsAbstract() || _interceptionFilter.Accept(p))
                .Where(p => p.CanOverride());

            typeDefinition.VisitProperties(buildPropertyVisitor);

            // Build methods.
            var buildMethodVisitor = Visitor.Create<MethodInfo>(typeBuilder.BuildMethod)
                .Where(m => m.IsAbstract || _interceptionFilter.Accept(m))
                .Where(m => m.CanOverride());

            typeDefinition.VisitMethods(buildMethodVisitor);

            return typeBuilder.CreateType();
        }

        #endregion
    }
}
