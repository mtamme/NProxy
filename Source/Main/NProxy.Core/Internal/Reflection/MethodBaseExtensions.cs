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
using System.Linq;
using System.Reflection;
using System.Text;

namespace NProxy.Core.Internal.Reflection
{
    /// <summary>
    /// Provides <see cref="MethodBase"/> extension methods.
    /// </summary>
    internal static class MethodBaseExtensions
    {
        /// <summary>
        /// The special name prefixes.
        /// </summary>
        private static readonly string[] SpecialNamePrefixes =
        {
            "get_",
            "set_",
            "add_",
            "remove_",
            "raise_"
        };

        /// <summary>
        /// Returns a value indicating whether the specified method is overrideable.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <returns>A value indicating whether the specified method is overrideable.</returns>
        public static bool CanOverride(this MethodBase methodBase)
        {
            if (methodBase == null)
                throw new ArgumentNullException("methodBase");

            if (!methodBase.IsVirtual || methodBase.IsFinal)
                return false;

            var declaringType = methodBase.DeclaringType;

            return !declaringType.IsSealed;
        }

        /// <summary>
        /// Returns the full name of the specified method.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <returns>The full name.</returns>
        public static string GetFullName(this MethodBase methodBase)
        {
            if (methodBase == null)
                throw new ArgumentNullException("methodBase");

            var fullName = new StringBuilder();

            fullName.Append(methodBase.DeclaringType);
            fullName.Append(Type.Delimiter);
            fullName.Append(methodBase.Name);

            return fullName.ToString();
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
        /// Returns a value indicating whether the specified method is a regular method.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <returns>A value indicating whether the specified method is a regular method.</returns>
        public static bool IsRegular(this MethodBase methodBase)
        {
            if (methodBase == null)
                throw new ArgumentNullException("methodBase");

            if (!methodBase.IsSpecialName)
                return true;

            var name = methodBase.Name;

            return !SpecialNamePrefixes.Any(name.StartsWith);
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