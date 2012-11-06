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

namespace NProxy.Core.Internal.Common
{
    /// <summary>
    /// Provides a set of static methods for creating visitors.
    /// </summary>
    internal static class Visitor
    {
        /// <summary>
        /// Creates a visitor for the specified visit action.
        /// </summary>
        /// <typeparam name="TElement">The element type.</typeparam>
        /// <param name="visit">The visit action.</param>
        /// <returns>The visitor with the specified implementation for the visit method.</returns>
        public static IVisitor<TElement> Create<TElement>(Action<TElement> visit)
        {
            return new AnonymousVisitor<TElement>(visit);
        }
    }
}
