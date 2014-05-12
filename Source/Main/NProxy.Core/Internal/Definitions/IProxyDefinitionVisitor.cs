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

namespace NProxy.Core.Internal.Definitions
{
    /// <summary>
    /// Defines a proxy definition visitor.
    /// </summary>
    internal interface IProxyDefinitionVisitor
    {
        /// <summary>
        /// Visits the specified interface type.
        /// </summary>
        /// <param name="interfaceType">The interface type.</param>
        void VisitInterface(Type interfaceType);

        /// <summary>
        /// Visits the specified constructor.
        /// </summary>
        /// <param name="constructorInfo">The constructor information.</param>
        void VisitConstructor(ConstructorInfo constructorInfo);

        /// <summary>
        /// Visits the specified event.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        void VisitEvent(EventInfo eventInfo);

        /// <summary>
        /// Visits the specified property.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        void VisitProperty(PropertyInfo propertyInfo);

        /// <summary>
        /// Visits the specified method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        void VisitMethod(MethodInfo methodInfo);
    }
}