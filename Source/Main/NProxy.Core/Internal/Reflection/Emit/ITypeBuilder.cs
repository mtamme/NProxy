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

namespace NProxy.Core.Internal.Reflection.Emit
{
    /// <summary>
    /// Defines a type builder.
    /// </summary>
    internal interface ITypeBuilder
    {
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
        /// <param name="constructorInfo">The constructor information.</param>                
        void BuildConstructor(ConstructorInfo constructorInfo);

        /// <summary>
        /// Builds a constructor based on the specified constructor.
        /// </summary>
        /// <param name="constructorInfo">The constructor information.</param>        
        /// <param name="invocationHandlerType">Invocation handler type</param>
        void BuildConstructor(ConstructorInfo constructorInfo, Type invocationHandlerFactoryType);        

        /// <summary>
        /// Returns a value indicating whether the specified event is concrete.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <returns>A value indicating whether the specified event is concrete.</returns>
        bool IsConcreteEvent(EventInfo eventInfo);

        /// <summary>
        /// Builds an event based on the specified event.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        void BuildEvent(EventInfo eventInfo);

        /// <summary>
        /// Returns a value indicating whether the specified property is concrete.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>A value indicating whether the specified property is concrete.</returns>
        bool IsConcreteProperty(PropertyInfo propertyInfo);

        /// <summary>
        /// Builds a property based on the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        void BuildProperty(PropertyInfo propertyInfo);

        /// <summary>
        /// Returns a value indicating whether the specified method is concrete.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>A value indicating whether the specified method is concrete.</returns>
        bool IsConcreteMethod(MethodInfo methodInfo);

        /// <summary>
        /// Builds a method based on the specified method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        void BuildMethod(MethodInfo methodInfo);

        /// <summary>
        /// Creates the type.
        /// </summary>
        /// <returns>The type.</returns>
        Type CreateType();        
    }
}