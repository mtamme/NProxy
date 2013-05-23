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
using NProxy.Core.Internal.Descriptors;

namespace NProxy.Core.Test.Internal.Descriptors
{
    /// <summary>
    /// Represents a collecting type visitor.
    /// </summary>
    internal sealed class CollectingTypeVisitor : ITypeVisitor
    {
        /// <summary>
        /// The interface types.
        /// </summary>
        private readonly List<Type> _interfaceTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectingTypeVisitor"/> class.
        /// </summary>
        public CollectingTypeVisitor()
        {
            _interfaceTypes = new List<Type>();
        }

        /// <summary>
        /// Returns the interface types.
        /// </summary>
        public ICollection<Type> InterfaceTypes
        {
            get { return _interfaceTypes; }
        }

        #region ITypeVisitor Members

        /// <inheritdoc/>
        public void VisitInterface(Type interfaceType)
        {
            _interfaceTypes.Add(interfaceType);
        }

        /// <inheritdoc/>
        public void VisitConstructor(ConstructorInfo constructorInfo)
        {
        }

        /// <inheritdoc/>
        public void VisitEvent(EventInfo eventInfo)
        {
        }

        /// <inheritdoc/>
        public void VisitProperty(PropertyInfo propertyInfo)
        {
        }

        /// <inheritdoc/>
        public void VisitMethod(MethodInfo methodInfo)
        {
        }

        #endregion
    }
}

