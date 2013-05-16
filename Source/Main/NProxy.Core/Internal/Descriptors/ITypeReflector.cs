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

namespace NProxy.Core.Internal.Descriptors
{
    /// <summary>
    /// Defines a type reflector.
    /// </summary>
    internal interface ITypeReflector
    {
        /// <summary>
        /// Visits all interfaces.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        void VisitInterfaces(IVisitor<Type> visitor);

        /// <summary>
        /// Visits all constructors.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        void VisitConstructors(IVisitor<ConstructorInfo> visitor);

        /// <summary>
        /// Visits all events.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        void VisitEvents(IVisitor<EventInfo> visitor);

        /// <summary>
        /// Visits all properties.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        void VisitProperties(IVisitor<PropertyInfo> visitor);

        /// <summary>
        /// Visits all methods.
        /// </summary>
        /// <param name="visitor">The visitor.</param>
        void VisitMethods(IVisitor<MethodInfo> visitor);
    }
}