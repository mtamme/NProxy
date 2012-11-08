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
using System.Linq;
using System.Reflection;

namespace NProxy.Core.Internal.Reflection
{
    /// <summary>
    /// Represents method base extensions.
    /// </summary>
    internal static class MethodBaseExtensions
    {
        /// <summary>
        /// The accessor method name prefixes.
        /// </summary>
        private static readonly string[] AccessorMethodNamePrefixes = new[]
                                                                          {
                                                                              // Event accessor method name prefixes.
                                                                              "add_", "remove_", "raise_",
                                                                              // Property accessor method name prefixes.
                                                                              "get_", "set_"
                                                                          };

        /// <summary>
        /// Returns a value indicating weather the specified method is overrideable.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <returns>A value indicating weather the specified method is overrideable.</returns>
        public static bool CanOverride(this MethodBase methodBase)
        {
            if (methodBase == null)
                throw new ArgumentNullException("methodBase");

            return methodBase.IsVirtual && !methodBase.IsFinal;
        }

        /// <summary>
        /// Returns the parameter types of the specified method.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <returns>The parameter types.</returns>
        public static Type[] GetParameterTypes(this MethodBase methodBase)
        {
            if (methodBase == null)
                throw new ArgumentNullException("methodBase");

            var parameterInfos = methodBase.GetParameters();

            return Array.ConvertAll(parameterInfos, p => p.ParameterType);
        }

        /// <summary>
        /// Returns a value indicating weather the specified method is an accessor.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <returns>A value indicating weather the specified method is an accessor.</returns>
        public static bool IsAccessor(this MethodBase methodBase)
        {
            if (!methodBase.IsSpecialName)
                return false;

            var methodName = methodBase.Name;

            return AccessorMethodNamePrefixes.Any(methodName.StartsWith);
        }

        /// <summary>
        /// Maps the parameter types to the specified generic types.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="genericTypes">The generic types.</param>
        /// <returns>The mapped parameter types.</returns>
        public static Type[] MapGenericParameterTypes(this MethodBase methodBase, Type[] genericTypes)
        {
            if (methodBase == null)
                throw new ArgumentNullException("methodBase");

            var parameterInfos = methodBase.GetParameters();

            return Array.ConvertAll(parameterInfos, p => p.ParameterType.MapGenericType(genericTypes));
        }
    }
}
