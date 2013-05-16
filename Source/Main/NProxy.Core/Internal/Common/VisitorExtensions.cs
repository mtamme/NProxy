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

namespace NProxy.Core.Internal.Common
{
    /// <summary>
    /// Provides visitor extensions.
    /// </summary>
    internal static class VisitorExtensions
    {
        /// <summary>
        /// Invokes an action for each visited element.
        /// </summary>
        /// <typeparam name="TElement">The element type.</typeparam>
        /// <param name="visitor">The visitor.</param>
        /// <param name="action">The action.</param>
        /// <returns>The visitor with the side-effecting behavior applied.</returns>
        public static IVisitor<TElement> Do<TElement>(this IVisitor<TElement> visitor, Action<TElement> action)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");

            if (action == null)
                throw new ArgumentNullException("action");

            return new AnonymousVisitor<TElement>(e =>
                {
                    action(e);
                    visitor.Visit(e);
                });
        }

        /// <summary>
        /// Filters the elements of a visitor based on a predicate.
        /// </summary>
        /// <typeparam name="TElement">The element type.</typeparam>
        /// <param name="visitor">The visitor.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A visitor that propagates only elements that satisfy the condition.</returns>
        public static IVisitor<TElement> Where<TElement>(this IVisitor<TElement> visitor, Func<TElement, bool> predicate)
        {
            if (visitor == null)
                throw new ArgumentNullException("visitor");

            if (predicate == null)
                throw new ArgumentNullException("predicate");

            return new AnonymousVisitor<TElement>(e =>
                {
                    if (predicate(e))
                        visitor.Visit(e);
                });
        }
    }
}