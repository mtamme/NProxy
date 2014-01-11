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
using NProxy.Core.Internal.Definitions;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core
{
    /// <summary>
    /// Represents the proxy generator.
    /// </summary>
    internal sealed class ProxyGenerator : IProxyDefinitionVisitor
    {
        /// <summary>
        /// The type builder.
        /// </summary>
        private readonly ITypeBuilder _typeBuilder;

        /// <summary>
        /// The interception filter.
        /// </summary>
        private readonly IInterceptionFilter _interceptionFilter;

        /// <summary>
        /// The event informations.
        /// </summary>
        private readonly List<EventInfo> _eventInfos;

        /// <summary>
        /// The property informations.
        /// </summary>
        private readonly List<PropertyInfo> _propertyInfos;

        /// <summary>
        /// The method informations.
        /// </summary>
        private readonly List<MethodInfo> _methodInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyGenerator"/> class.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="interceptionFilter">The interception filter.</param>
        public ProxyGenerator(ITypeBuilder typeBuilder, IInterceptionFilter interceptionFilter)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (interceptionFilter == null)
                throw new ArgumentNullException("interceptionFilter");

            _typeBuilder = typeBuilder;
            _interceptionFilter = interceptionFilter;

            _eventInfos = new List<EventInfo>();
            _propertyInfos = new List<PropertyInfo>();
            _methodInfos = new List<MethodInfo>();
        }

        /// <summary>
        /// Generates a proxy template.
        /// </summary>
        /// <param name="proxyDefinition">The proxy definition.</param>
        /// <returns>The proxy template.</returns>
        public IProxyTemplate GenerateProxyTemplate(IProxyDefinition proxyDefinition)
        {
            if (proxyDefinition == null)
                throw new ArgumentNullException("proxyDefinition");

            // Build type.
            proxyDefinition.AcceptVisitor(this);

            // Create type.
            var type = _typeBuilder.CreateType();

            return new ProxyTemplate(proxyDefinition, type, _eventInfos, _propertyInfos, _methodInfos);
        }

        #region IProxyDefinitionVisitor Members

        /// <inheritdoc/>
        public void VisitInterface(Type interfaceType)
        {
            _typeBuilder.AddInterface(interfaceType);
        }

        /// <inheritdoc/>
        public void VisitConstructor(ConstructorInfo constructorInfo)
        {
            _typeBuilder.BuildConstructor(constructorInfo);
        }

        /// <inheritdoc/>
        public void VisitEvent(EventInfo eventInfo)
        {
            if (!eventInfo.IsAbstract() && !_interceptionFilter.AcceptEvent(eventInfo))
                return;

            _typeBuilder.BuildEvent(eventInfo);
            _eventInfos.Add(eventInfo);
        }

        /// <inheritdoc/>
        public void VisitProperty(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.IsAbstract() && !_interceptionFilter.AcceptProperty(propertyInfo))
                return;

            _typeBuilder.BuildProperty(propertyInfo);
            _propertyInfos.Add(propertyInfo);
        }

        /// <inheritdoc/>
        public void VisitMethod(MethodInfo methodInfo)
        {
            if (!methodInfo.IsAbstract && !_interceptionFilter.AcceptMethod(methodInfo))
                return;

            _typeBuilder.BuildMethod(methodInfo);
            _methodInfos.Add(methodInfo);
        }

        #endregion
    }
}