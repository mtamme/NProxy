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
using System.Linq;
using System.Reflection;

namespace NProxy.Core.Internal.Descriptors
{
    /// <summary>
    /// Represents type visitor extensions.
    /// </summary>
    internal static class TypeVisitorExtensions
    {
        /// <summary>
        /// Visits all specified interface types.
        /// </summary>
        /// <param name="typeVisitor">The type visitor.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        public static void VisitInterfaces(this ITypeVisitor typeVisitor, IEnumerable<Type> interfaceTypes)
        {
            foreach (var interfaceType in interfaceTypes)
            {
                typeVisitor.VisitInterface(interfaceType);

                typeVisitor.VisitMembers(interfaceType);
            }
        }

        /// <summary>
        /// Visits all constructors of the specified type.
        /// </summary>
        /// <param name="typeVisitor">The type visitor.</param>
        /// <param name="type">The type.</param>
        public static void VisitConstructors(this ITypeVisitor typeVisitor, Type type)
        {
            if (typeVisitor == null)
                throw new ArgumentNullException("typeVisitor");

            if (type == null)
                throw new ArgumentNullException("type");

            var constructorInfos = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var constructorInfo in constructorInfos)
            {
                typeVisitor.VisitConstructor(constructorInfo);
            }
        }

        /// <summary>
        /// Visits all members of the specified type.
        /// </summary>
        /// <param name="typeVisitor">The type visitor.</param>
        /// <param name="type">The type.</param>
        public static void VisitMembers(this ITypeVisitor typeVisitor, Type type)
        {
            // Visit events.
            typeVisitor.VisitEvents(type);

            // Visit properties.
            typeVisitor.VisitProperties(type);

            // Visit methods.
            typeVisitor.VisitMethods(type);
        }

        /// <summary>
        /// Visits all events of the specified type.
        /// </summary>
        /// <param name="typeVisitor">The type visitor.</param>
        /// <param name="type">The type.</param>
        private static void VisitEvents(this ITypeVisitor typeVisitor, Type type)
        {
            if (typeVisitor == null)
                throw new ArgumentNullException("typeVisitor");

            if (type == null)
                throw new ArgumentNullException("type");

            // Only visit instance events.
            var eventInfos = type.GetEvents(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var eventInfo in eventInfos)
            {
                typeVisitor.VisitEvent(eventInfo);
            }
        }

        /// <summary>
        /// Visits all properties of the specified type.
        /// </summary>
        /// <param name="typeVisitor">The type visitor.</param>
        /// <param name="type">The type.</param>
        private static void VisitProperties(this ITypeVisitor typeVisitor, Type type)
        {
            if (typeVisitor == null)
                throw new ArgumentNullException("typeVisitor");

            if (type == null)
                throw new ArgumentNullException("type");

            // Only visit instance properties.
            var propertyInfos = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in propertyInfos)
            {
                typeVisitor.VisitProperty(propertyInfo);
            }
        }

        /// <summary>
        /// Visits all methods of the specified type.
        /// </summary>
        /// <param name="typeVisitor">The type visitor.</param>
        /// <param name="type">The type.</param>
        private static void VisitMethods(this ITypeVisitor typeVisitor, Type type)
        {
            if (typeVisitor == null)
                throw new ArgumentNullException("typeVisitor");

            if (type == null)
                throw new ArgumentNullException("type");

            // Only visit non-accessor instance methods.
            var methodInfos = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                                  .Where(m => !m.IsSpecialName);

            foreach (var methodInfo in methodInfos)
            {
                typeVisitor.VisitMethod(methodInfo);
            }
        }
    }
}