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
using System.Reflection;
using NProxy.Core.Internal.Definitions;

namespace NProxy.Core.Test.Internal.Definitions
{
    /// <summary>
    /// Represents a collecting proxy definition visitor.
    /// </summary>
    internal sealed class CollectingProxyDefinitionVisitor : IProxyDefinitionVisitor
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
        /// Initializes a new instance of the <see cref="CollectingProxyDefinitionVisitor"/> class.
        /// </summary>
        public CollectingProxyDefinitionVisitor()
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

        #region IProxyDefinitionVisitor Members

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