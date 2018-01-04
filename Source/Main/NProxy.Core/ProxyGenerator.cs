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
using System.Reflection;
using NProxy.Core.Internal.Definitions;
using NProxy.Core.Internal.Reflection.Emit;

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
        /// Generates a proxy template based on the specified proxy definition.
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
        void IProxyDefinitionVisitor.VisitInterface(Type interfaceType)
        {
            _typeBuilder.AddInterface(interfaceType);
        }

        /// <inheritdoc/>
        void IProxyDefinitionVisitor.VisitConstructor(ConstructorInfo constructorInfo)
        {
            _typeBuilder.BuildConstructor(constructorInfo);
        }

        /// <inheritdoc/>
        void IProxyDefinitionVisitor.VisitEvent(EventInfo eventInfo)
        {
            if (_typeBuilder.IsConcreteEvent(eventInfo) && !_interceptionFilter.AcceptEvent(eventInfo))
                return;

            _typeBuilder.BuildEvent(eventInfo);
            _eventInfos.Add(eventInfo);
        }

        /// <inheritdoc/>
        void IProxyDefinitionVisitor.VisitProperty(PropertyInfo propertyInfo)
        {
            if (_typeBuilder.IsConcreteProperty(propertyInfo) && !_interceptionFilter.AcceptProperty(propertyInfo))
                return;

            _typeBuilder.BuildProperty(propertyInfo);
            _propertyInfos.Add(propertyInfo);
        }

        /// <inheritdoc/>
        void IProxyDefinitionVisitor.VisitMethod(MethodInfo methodInfo)
        {
            if (_typeBuilder.IsConcreteMethod(methodInfo) && !_interceptionFilter.AcceptMethod(methodInfo))
                return;

            _typeBuilder.BuildMethod(methodInfo);
            _methodInfos.Add(methodInfo);
        }

        #endregion
    }
}