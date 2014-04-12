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
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core.Internal.Definitions
{
    /// <summary>
    /// Provides <see cref="IProxyDefinitionVisitor"/> extension methods.
    /// </summary>
    internal static class ProxyDefinitionVisitorExtensions
    {
        /// <summary>
        /// Visits all specified interface types.
        /// </summary>
        /// <param name="proxyDefinitionVisitor">The proxy definition visitor.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        public static void VisitInterfaces(this IProxyDefinitionVisitor proxyDefinitionVisitor, IEnumerable<Type> interfaceTypes)
        {
            foreach (var interfaceType in interfaceTypes)
            {
                proxyDefinitionVisitor.VisitInterface(interfaceType);

                proxyDefinitionVisitor.VisitMembers(interfaceType);
            }
        }

        /// <summary>
        /// Visits all constructors of the specified type.
        /// </summary>
        /// <param name="proxyDefinitionVisitor">The proxy definition visitor.</param>
        /// <param name="type">The type.</param>
        public static void VisitConstructors(this IProxyDefinitionVisitor proxyDefinitionVisitor, Type type)
        {
            if (proxyDefinitionVisitor == null)
                throw new ArgumentNullException("proxyDefinitionVisitor");

            if (type == null)
                throw new ArgumentNullException("type");

            var constructorInfos = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var constructorInfo in constructorInfos)
            {
                proxyDefinitionVisitor.VisitConstructor(constructorInfo);
            }
        }

        /// <summary>
        /// Visits all members of the specified type.
        /// </summary>
        /// <param name="proxyDefinitionVisitor">The proxy definition visitor.</param>
        /// <param name="type">The type.</param>
        public static void VisitMembers(this IProxyDefinitionVisitor proxyDefinitionVisitor, Type type)
        {
            // Visit events.
            proxyDefinitionVisitor.VisitEvents(type);

            // Visit properties.
            proxyDefinitionVisitor.VisitProperties(type);

            // Visit methods.
            proxyDefinitionVisitor.VisitMethods(type);
        }

        /// <summary>
        /// Visits all events of the specified type.
        /// </summary>
        /// <param name="proxyDefinitionVisitor">The proxy definition visitor.</param>
        /// <param name="type">The type.</param>
        private static void VisitEvents(this IProxyDefinitionVisitor proxyDefinitionVisitor, Type type)
        {
            if (proxyDefinitionVisitor == null)
                throw new ArgumentNullException("proxyDefinitionVisitor");

            if (type == null)
                throw new ArgumentNullException("type");

            // Visit only overridable instance events.
            var eventInfos = type.GetEvents(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(e => e.CanOverride());

            foreach (var eventInfo in eventInfos)
            {
                proxyDefinitionVisitor.VisitEvent(eventInfo);
            }
        }

        /// <summary>
        /// Visits all properties of the specified type.
        /// </summary>
        /// <param name="proxyDefinitionVisitor">The proxy definition visitor.</param>
        /// <param name="type">The type.</param>
        private static void VisitProperties(this IProxyDefinitionVisitor proxyDefinitionVisitor, Type type)
        {
            if (proxyDefinitionVisitor == null)
                throw new ArgumentNullException("proxyDefinitionVisitor");

            if (type == null)
                throw new ArgumentNullException("type");

            // Visit only overridable instance properties.
            var propertyInfos = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanOverride());

            foreach (var propertyInfo in propertyInfos)
            {
                proxyDefinitionVisitor.VisitProperty(propertyInfo);
            }
        }

        /// <summary>
        /// Visits all methods of the specified type.
        /// </summary>
        /// <param name="proxyDefinitionVisitor">The proxy definition visitor.</param>
        /// <param name="type">The type.</param>
        private static void VisitMethods(this IProxyDefinitionVisitor proxyDefinitionVisitor, Type type)
        {
            if (proxyDefinitionVisitor == null)
                throw new ArgumentNullException("proxyDefinitionVisitor");

            if (type == null)
                throw new ArgumentNullException("type");

            // Visit only non-accessor overridable instance methods.
            var methodInfos = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(m => !m.IsSpecialName && m.CanOverride());

            foreach (var methodInfo in methodInfos)
            {
                proxyDefinitionVisitor.VisitMethod(methodInfo);
            }
        }
    }
}