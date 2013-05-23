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
using NProxy.Core.Internal.Descriptors;

namespace NProxy.Core.Internal.Builders
{
    /// <summary>
    /// Represents a type builder adapter.
    /// </summary>
    internal sealed class TypeBuilderAdapter : ITypeVisitor
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
        /// Initializes a new instance of the <see cref="TypeBuilderAdapter"/> class.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="interceptionFilter">The interception filter.</param>
        public TypeBuilderAdapter(ITypeBuilder typeBuilder, IInterceptionFilter interceptionFilter)
        {
            if (typeBuilder == null)
                throw new ArgumentNullException("typeBuilder");

            if (interceptionFilter == null)
                throw new ArgumentNullException("interceptionFilter");

            _typeBuilder = typeBuilder;
            _interceptionFilter = interceptionFilter;
        }

        #region ITypeVisitor Members

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
            if (_interceptionFilter.AcceptEvent(eventInfo))
                _typeBuilder.BuildEvent(eventInfo);
        }

        /// <inheritdoc/>
        public void VisitProperty(PropertyInfo propertyInfo)
        {
            if (_interceptionFilter.AcceptProperty(propertyInfo))
                _typeBuilder.BuildProperty(propertyInfo);
        }

        /// <inheritdoc/>
        public void VisitMethod(MethodInfo methodInfo)
        {
            if (_interceptionFilter.AcceptMethod(methodInfo))
                _typeBuilder.BuildMethod(methodInfo);
        }

        #endregion
    }
}