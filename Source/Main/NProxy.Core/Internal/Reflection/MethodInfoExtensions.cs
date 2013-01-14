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
    /// Provides method information extensions.
    /// </summary>
    internal static class MethodInfoExtensions
    {
        /// <summary>
        /// Maps a method to the specified generic types.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="genericTypes">The generic types.</param>
        /// <returns>The mapped method information.</returns>
        public static MethodInfo MapGenericMethod(this MethodInfo methodInfo, Type[] genericTypes)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            if (genericTypes == null)
                throw new ArgumentNullException("genericTypes");

            return methodInfo.IsGenericMethodDefinition ? methodInfo.MakeGenericMethod(genericTypes) : methodInfo;
        }

        /// <summary>
        /// Maps a return type to the specified generic types.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="genericTypes">The generic types.</param>
        /// <returns>The mapped return type.</returns>
        public static Type MapGenericReturnType(this MethodInfo methodInfo, Type[] genericTypes)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            var returnType = methodInfo.ReturnType;

            return returnType.MapGenericType(genericTypes);
        }
    }
}