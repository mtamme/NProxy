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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NProxy.Core.Internal.Reflection
{
    /// <summary>
    /// Provides property information extensions.
    /// </summary>
    internal static class PropertyInfoExtensions
    {
        /// <summary>
        /// Returns a value indicating weather the specified property is overrideable.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>A value indicating weather the specified property is overrideable.</returns>
        public static bool CanOverride(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var methodInfos = propertyInfo.GetAccessorMethods();

            return methodInfos.All(m => m.CanOverride());
        }

        /// <summary>
        /// Returns a value indicating weather the specified property is abstract.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>A value indicating weather the specified property is abstract.</returns>
        public static bool IsAbstract(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var methodInfos = propertyInfo.GetAccessorMethods();

            return methodInfos.Any(m => m.IsAbstract);
        }

        /// <summary>
        /// Returns the accessor methods for the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>The accessor method informations.</returns>
        public static IEnumerable<MethodInfo> GetAccessorMethods(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var methodInfos = new List<MethodInfo>();

            var getMethodInfo = propertyInfo.GetGetMethod();

            if (getMethodInfo != null)
                methodInfos.Add(getMethodInfo);

            var setMethodInfo = propertyInfo.GetSetMethod();

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

            var declaringType = propertyInfo.DeclaringType;
            var name = declaringType.GetFullName();

            name.Append(Type.Delimiter);
            name.Append(propertyInfo.Name);

            return name.ToString();
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