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
using System.Linq;
using System.Reflection;

namespace NProxy.Core.Internal.Reflection
{
    /// <summary>
    /// Represents member information extensions.
    /// </summary>
    internal static class MemberInfoExtensions
    {
        /// <summary>
        /// Returns the custom attributes of the specified member.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type.</typeparam>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The custom attributes.</returns>
        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this MemberInfo memberInfo)
        {
            if (memberInfo == null)
                throw new ArgumentNullException("memberInfo");

            var customAttributes = memberInfo.GetCustomAttributes(typeof (TAttribute), false);

            return customAttributes.OfType<TAttribute>();
        }

        /// <summary>
        /// Returns a value indicating weather the member is annotated with the specified custom attribute type.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type.</typeparam>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>A value indicating weather the member is annotated with the specified custom attribute type.</returns>
        public static bool HasCustomAttribute<TAttribute>(this MemberInfo memberInfo)
        {
            if (memberInfo == null)
                throw new ArgumentNullException("memberInfo");

            var customAttributes = memberInfo.GetCustomAttributes(typeof (TAttribute), false);

            return (customAttributes.Length > 0);
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
                throw new InvalidOperationException("Member has no declaring type");

            return declaringType;
        }

        /// <summary>
        /// Returns the full name of the member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The full name.</returns>
        public static string GetFullName(this MemberInfo memberInfo)
        {
            var declaringType = memberInfo.GetDeclaringType();
            var name = declaringType.GetFullName();

            name.Append(Type.Delimiter);
            name.Append(memberInfo.Name);

            return name.ToString();
        }

        /// <summary>
        /// Returns a value which uniquely identifies a member.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The unique identifier.</returns>
        public static long GetToken(this MemberInfo memberInfo)
        {
            long moduleToken = memberInfo.Module.MetadataToken;
            long memberToken = memberInfo.MetadataToken;

            return (moduleToken << 32) | (memberToken & 0xffffffffL);
        }
    }
}
