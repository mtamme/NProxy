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
using System.Linq;
using System.Reflection;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core.Internal
{
    /// <summary>
    /// Provides <see cref="IProxyInfoVisitor"/> extension methods.
    /// </summary>
    internal static class ProxyInfoVisitorExtensions
    {
        /// <summary>
        /// Visits all specified interface types.
        /// </summary>
        /// <param name="proxyInfoVisitor">The proxy information visitor.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        public static void VisitInterfaces(this IProxyInfoVisitor proxyInfoVisitor, IEnumerable<Type> interfaceTypes)
        {
            foreach (var interfaceType in interfaceTypes)
            {
                proxyInfoVisitor.VisitInterface(interfaceType);

                proxyInfoVisitor.VisitMembers(interfaceType);
            }
        }

        /// <summary>
        /// Visits all constructors of the specified type.
        /// </summary>
        /// <param name="proxyInfoVisitor">The proxy information visitor.</param>
        /// <param name="type">The type.</param>
        public static void VisitConstructors(this IProxyInfoVisitor proxyInfoVisitor, Type type)
        {
            if (proxyInfoVisitor == null)
                throw new ArgumentNullException("proxyInfoVisitor");

            if (type == null)
                throw new ArgumentNullException("type");

            var constructorInfos = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var constructorInfo in constructorInfos)
            {
                proxyInfoVisitor.VisitConstructor(constructorInfo);
            }
        }

        /// <summary>
        /// Visits all members of the specified type.
        /// </summary>
        /// <param name="proxyInfoVisitor">The proxy information visitor.</param>
        /// <param name="type">The type.</param>
        public static void VisitMembers(this IProxyInfoVisitor proxyInfoVisitor, Type type)
        {
            // Visit events.
            proxyInfoVisitor.VisitEvents(type);

            // Visit properties.
            proxyInfoVisitor.VisitProperties(type);

            // Visit methods.
            proxyInfoVisitor.VisitMethods(type);
        }

        /// <summary>
        /// Visits all events of the specified type.
        /// </summary>
        /// <param name="proxyInfoVisitor">The proxy information visitor.</param>
        /// <param name="type">The type.</param>
        private static void VisitEvents(this IProxyInfoVisitor proxyInfoVisitor, Type type)
        {
            if (proxyInfoVisitor == null)
                throw new ArgumentNullException("proxyInfoVisitor");

            if (type == null)
                throw new ArgumentNullException("type");

            // Visit only overridable instance events.
            var eventInfos = type.GetEvents(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(e => e.CanOverride());

            foreach (var eventInfo in eventInfos)
            {
                proxyInfoVisitor.VisitEvent(eventInfo);
            }
        }

        /// <summary>
        /// Visits all properties of the specified type.
        /// </summary>
        /// <param name="proxyInfoVisitor">The proxy information visitor.</param>
        /// <param name="type">The type.</param>
        private static void VisitProperties(this IProxyInfoVisitor proxyInfoVisitor, Type type)
        {
            if (proxyInfoVisitor == null)
                throw new ArgumentNullException("proxyInfoVisitor");

            if (type == null)
                throw new ArgumentNullException("type");

            // Visit only overridable instance properties.
            var propertyInfos = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanOverride());

            foreach (var propertyInfo in propertyInfos)
            {
                proxyInfoVisitor.VisitProperty(propertyInfo);
            }
        }

        /// <summary>
        /// Visits all methods of the specified type.
        /// </summary>
        /// <param name="proxyInfoVisitor">The proxy information visitor.</param>
        /// <param name="type">The type.</param>
        private static void VisitMethods(this IProxyInfoVisitor proxyInfoVisitor, Type type)
        {
            if (proxyInfoVisitor == null)
                throw new ArgumentNullException("proxyInfoVisitor");

            if (type == null)
                throw new ArgumentNullException("type");

            // Visit only non-accessor overridable instance methods.
            var methodInfos = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.IsRegular() && m.CanOverride());

            foreach (var methodInfo in methodInfos)
            {
                proxyInfoVisitor.VisitMethod(methodInfo);
            }
        }
    }
}