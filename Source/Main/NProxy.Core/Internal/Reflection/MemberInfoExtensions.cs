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

namespace NProxy.Core.Internal.Reflection
{
    /// <summary>
    /// Provides <see cref="MemberInfo"/> extension methods.
    /// </summary>
    internal static class MemberInfoExtensions
    {
        /// <summary>
        /// Returns a value indicating whether one or more attributes of the specified type or of its
        /// derived types is applied to this member.
        /// </summary>
        /// <typeparam name="TAttribute">The type, or a base type, of the custom attribute to search for.</typeparam>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="inherit">A value indicating whether to search the member's inheritance chain to find the attributes.</param>
        /// <returns>A value indicating weather the member is annotated with the specified custom attribute type.</returns>
        public static bool IsDefined<TAttribute>(this MemberInfo memberInfo, bool inherit) where TAttribute : Attribute
        {
            if (memberInfo == null)
                throw new ArgumentNullException("memberInfo");

            return Attribute.IsDefined(memberInfo, typeof (TAttribute), inherit);
        }

        /// <summary>
        /// Returns the declaring type of the specified member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The declaring type.</returns>
        public static Type GetDeclaringType(this MemberInfo memberInfo)
        {
            if (memberInfo == null)
                throw new ArgumentNullException("memberInfo");

            var declaringType = memberInfo.DeclaringType;

            if (declaringType == null)
                throw new InvalidOperationException(Resources.MemberHasNoDeclaringType);

            return declaringType;
        }
    }
}