//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © 2012 Martin Tamme
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

namespace NProxy.Core.Internal.Common
{
    /// <summary>
    /// Provides enumerable extensions.
    /// </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Visits all elements of the specified enumeration.
        /// </summary>
        /// <typeparam name="TElement">The element type.</typeparam>
        /// <param name="elements">The element enumeration.</param>
        /// <param name="visit">The visit action.</param>
        public static void Visit<TElement>(this IEnumerable<TElement> elements, Action<TElement> visit)
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            if (visit == null)
                throw new ArgumentNullException("visit");

            foreach (var element in elements)
            {
                visit(element);
            }
        }

        /// <summary>
        /// Visits all elements of the specified enumeration.
        /// </summary>
        /// <typeparam name="TElement">The element type.</typeparam>
        /// <param name="elements">The element enumeration.</param>
        /// <param name="visitor">The visitor.</param>
        public static void Visit<TElement>(this IEnumerable<TElement> elements, IVisitor<TElement> visitor)
        {
            if (elements == null)
                throw new ArgumentNullException("elements");

            if (visitor == null)
                throw new ArgumentNullException("visitor");

            foreach (var element in elements)
            {
                visitor.Visit(element);
            }
        }
    }
}
