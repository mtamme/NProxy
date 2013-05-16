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
using NProxy.Core.Internal.Common;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core.Internal.Descriptors
{
    /// <summary>
    /// Represents the type reflector base class.
    /// </summary>
    internal abstract class TypeReflectorBase : ITypeReflector
    {
        /// <summary>
        /// The declaring type.
        /// </summary>
        protected readonly Type DeclaringType;

        /// <summary>
        /// The parent type.
        /// </summary>
        protected readonly Type ParentType;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeReflectorBase"/> class.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="parentType">The parent type.</param>
        protected TypeReflectorBase(Type declaringType, Type parentType)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");

            if (parentType == null)
                throw new ArgumentNullException("parentType");

            DeclaringType = declaringType;
            ParentType = parentType;
        }

        #region ITypeReflector Members

        /// <inheritdoc/>
        public abstract void VisitInterfaces(IVisitor<Type> visitor);

        /// <inheritdoc/>
        public void VisitConstructors(IVisitor<System.Reflection.ConstructorInfo> visitor)
        {
            ParentType.VisitConstructors(visitor);
        }

        /// <inheritdoc/>
        public abstract void VisitEvents(IVisitor<System.Reflection.EventInfo> visitor);

        /// <inheritdoc/>
        public abstract void VisitProperties(IVisitor<System.Reflection.PropertyInfo> visitor);

        /// <inheritdoc/>
        public abstract void VisitMethods(IVisitor<System.Reflection.MethodInfo> visitor);

        #endregion
    }
}