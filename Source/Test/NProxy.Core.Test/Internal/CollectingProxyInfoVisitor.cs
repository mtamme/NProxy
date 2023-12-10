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
using System.Reflection;
using NProxy.Core.Internal;

namespace NProxy.Core.Test.Internal
{
    /// <summary>
    /// Represents a collecting proxy information visitor.
    /// </summary>
    internal sealed class CollectingProxyInfoVisitor : IProxyInfoVisitor
    {
        /// <summary>
        /// The interface types.
        /// </summary>
        private readonly List<Type> _interfaceTypes;

        /// <summary>
        /// The constructor informations.
        /// </summary>
        private readonly List<ConstructorInfo> _constructorInfos;

        /// <summary>
        /// The event informations.
        /// </summary>
        private readonly List<EventInfo> _eventInfos;

        /// <summary>
        /// The property informations.
        /// </summary>
        private readonly List<PropertyInfo> _propertyInfos;

        /// <summary>
        /// The method informations.
        /// </summary>
        private readonly List<MethodInfo> _methodInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectingProxyInfoVisitor"/> class.
        /// </summary>
        public CollectingProxyInfoVisitor()
        {
            _interfaceTypes = new List<Type>();
            _constructorInfos = new List<ConstructorInfo>();
            _eventInfos = new List<EventInfo>();
            _propertyInfos = new List<PropertyInfo>();
            _methodInfos = new List<MethodInfo>();
        }

        /// <summary>
        /// Returns the interface types.
        /// </summary>
        public ICollection<Type> InterfaceTypes
        {
            get { return _interfaceTypes; }
        }

        /// <summary>
        /// Returns the constructor informations.
        /// </summary>
        public ICollection<ConstructorInfo> ConstructorInfos
        {
            get { return _constructorInfos; }
        }

        /// <summary>
        /// Returns the event informations.
        /// </summary>
        public ICollection<EventInfo> EventInfos
        {
            get { return _eventInfos; }
        }

        /// <summary>
        /// Returns the property informations.
        /// </summary>
        public ICollection<PropertyInfo> PropertyInfos
        {
            get { return _propertyInfos; }
        }

        /// <summary>
        /// Returns the method informations.
        /// </summary>
        public ICollection<MethodInfo> MethodInfos
        {
            get { return _methodInfos; }
        }

        #region IProxyInfoVisitor Members

        /// <inheritdoc/>
        public void VisitInterface(Type interfaceType)
        {
            _interfaceTypes.Add(interfaceType);
        }

        /// <inheritdoc/>
        public void VisitConstructor(ConstructorInfo constructorInfo)
        {
            _constructorInfos.Add(constructorInfo);
        }

        /// <inheritdoc/>
        public void VisitEvent(EventInfo eventInfo)
        {
            _eventInfos.Add(eventInfo);
        }

        /// <inheritdoc/>
        public void VisitProperty(PropertyInfo propertyInfo)
        {
            _propertyInfos.Add(propertyInfo);
        }

        /// <inheritdoc/>
        public void VisitMethod(MethodInfo methodInfo)
        {
            _methodInfos.Add(methodInfo);
        }

        #endregion
    }
}