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
using System.Reflection;

namespace NProxy.Core.Internal.Reflection
{
    /// <summary>
    /// Provides <see cref="MethodInfo"/> extension methods.
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