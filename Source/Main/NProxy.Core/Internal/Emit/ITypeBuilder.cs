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

namespace NProxy.Core.Internal.Emit
{
    /// <summary>
    /// Defines a type builder.
    /// </summary>
    internal interface ITypeBuilder
    {
        /// <summary>
        /// Returns a value indicating whether the specified event is optional and therefor not required for building the type.
        /// </summary>
        /// <param name="declaringEventInfo">The declaring event information.</param>
        /// <returns>A value indicating whether the specified event is optional and therefor not required for building the type.</returns>
        bool IsOptionalEvent(EventInfo declaringEventInfo);

        /// <summary>
        /// Returns a value indicating whether the specified property is optional and therefor not required for building the type.
        /// </summary>
        /// <param name="declaringPropertyInfo">The declaring property information.</param>
        /// <returns>A value indicating whether the specified property is optional and therefor not required for building the type.</returns>
        bool IsOptionalProperty(PropertyInfo declaringPropertyInfo);

        /// <summary>
        /// Returns a value indicating whether the specified method is optional and therefor not required for building the type.
        /// </summary>
        /// <param name="declaringMethodInfo">The declaring method information.</param>
        /// <returns>A value indicating whether the specified method is optional and therefor not required for building the type.</returns>
        bool IsOptionalMethod(MethodInfo declaringMethodInfo);

        /// <summary>
        /// Adds the specified custom attribute.
        /// </summary>
        /// <param name="constructorInfo">The constructor information.</param>
        /// <param name="arguments">The constructor arguments.</param>
        void AddCustomAttribute(ConstructorInfo constructorInfo, params object[] arguments);

        /// <summary>
        /// Adds the specified interface.
        /// </summary>
        /// <param name="interfaceType">The interface type.</param>
        void AddInterface(Type interfaceType);

        /// <summary>
        /// Builds a constructor based on the specified constructor.
        /// </summary>
        /// <param name="declaringConstructorInfo">The declaring constructor information.</param>
        void BuildConstructor(ConstructorInfo declaringConstructorInfo);

        /// <summary>
        /// Builds an event based on the specified event.
        /// </summary>
        /// <param name="declaringEventInfo">The declaring event information.</param>
        void BuildEvent(EventInfo declaringEventInfo);

        /// <summary>
        /// Builds a property based on the specified property.
        /// </summary>
        /// <param name="declaringPropertyInfo">The declaring property information.</param>
        void BuildProperty(PropertyInfo declaringPropertyInfo);

        /// <summary>
        /// Builds a method based on the specified method.
        /// </summary>
        /// <param name="declaringMethodInfo">The declaring method information.</param>
        void BuildMethod(MethodInfo declaringMethodInfo);

        /// <summary>
        /// Creates the type.
        /// </summary>
        /// <returns>The type.</returns>
        Type CreateType();
    }
}