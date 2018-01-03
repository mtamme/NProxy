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
using System.Text;

namespace NProxy.Core.Internal.Reflection
{
    /// <summary>
    /// Provides <see cref="PropertyInfo"/> extension methods.
    /// </summary>
    internal static class PropertyInfoExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the specified property is overrideable.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>A value indicating whether the specified property is overrideable.</returns>
        public static bool CanOverride(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var methodInfos = propertyInfo.GetMethods();

            return methodInfos.All(m => m.CanOverride());
        }

        /// <summary>
        /// Returns the methods for the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>The methods for the specified property.</returns>
        public static IEnumerable<MethodInfo> GetMethods(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var methodInfos = new List<MethodInfo>();
            var getMethodInfo = propertyInfo.GetGetMethod(true);

            if (getMethodInfo != null)
                methodInfos.Add(getMethodInfo);

            var setMethodInfo = propertyInfo.GetSetMethod(true);

            if (setMethodInfo != null)
                methodInfos.Add(setMethodInfo);

            return methodInfos;
        }

        /// <summary>
        /// Returns the full name of the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>The full name.</returns>
        public static string GetFullName(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var fullName = new StringBuilder();

            fullName.Append(propertyInfo.DeclaringType);
            fullName.Append(Type.Delimiter);
            fullName.Append(propertyInfo.Name);

            return fullName.ToString();
        }

        /// <summary>
        /// Returns the index parameter types of the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>The index parameter types.</returns>
        public static Type[] GetIndexParameterTypes(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var indexParameterInfos = propertyInfo.GetIndexParameters();

            return Array.ConvertAll(indexParameterInfos, p => p.ParameterType);
        }
    }
}