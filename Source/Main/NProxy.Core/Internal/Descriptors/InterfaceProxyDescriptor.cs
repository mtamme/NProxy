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
using NProxy.Core.Internal.Common;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core.Internal.Descriptors
{
    /// <summary>
    /// Represents an interface proxy descriptor.
    /// </summary>
    internal sealed class InterfaceProxyDescriptor : ProxyDescriptorBase
    {
        /// <summary>
        /// Represents an interface proxy type reflector.
        /// </summary>
        private sealed class TypeReflector : TypeReflectorBase
        {
            /// <summary>
            /// The interface types.
            /// </summary>
            private readonly ICollection<Type> _interfaceTypes;

            /// <summary>
            /// Initializes a new instance of the <see cref="TypeReflector"/> class.
            /// </summary>
            /// <param name="declaringType">The declaring type.</param>
            /// <param name="parentType">The parent type.</param>
            /// <param name="interfaceTypes">The interface types.</param>
            public TypeReflector(Type declaringType, Type parentType, ICollection<Type> interfaceTypes)
                : base(declaringType, parentType)
            {
                _interfaceTypes = interfaceTypes;
            }

            #region ITypeReflector Members

            /// <inheritdoc/>
            public override void VisitInterfaces(IVisitor<Type> visitor)
            {
                // Visit interfaces.
                _interfaceTypes.Visit(visitor);
            }

            /// <inheritdoc/>
            public override void VisitEvents(IVisitor<EventInfo> visitor)
            {
                // Visit interface events.
                _interfaceTypes.Visit(t => t.VisitEvents(visitor));

                // Visit parent type events.
                ParentType.VisitEvents(visitor);
            }

            /// <inheritdoc/>
            public override void VisitProperties(IVisitor<PropertyInfo> visitor)
            {
                // Visit interface properties.
                _interfaceTypes.Visit(t => t.VisitProperties(visitor));

                // Visit parent type properties.
                ParentType.VisitProperties(visitor);
            }

            /// <inheritdoc/>
            public override void VisitMethods(IVisitor<MethodInfo> visitor)
            {
                // Visit interface methods.
                _interfaceTypes.Visit(t => t.VisitMethods(visitor));

                // Visit parent type methods.
                ParentType.VisitMethods(visitor);
            }

            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceProxyDescriptor"/> class.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        public InterfaceProxyDescriptor(Type declaringType, IEnumerable<Type> interfaceTypes)
            : base(declaringType, typeof (object), interfaceTypes)
        {
        }

        #region ProxyDescriptorBase Members

        /// <inheritdoc/>
        protected override ITypeReflector CreateReflector(Type declaringType, Type parentType, ICollection<Type> declaringInterfaceTypes, ICollection<Type> additionalInterfaceTypes)
        {
            var interfaceTypes = new List<Type>();

            interfaceTypes.AddRange(declaringInterfaceTypes);
            interfaceTypes.AddRange(additionalInterfaceTypes);

            return new TypeReflector(declaringType, parentType, interfaceTypes);
        }

        #endregion

        #region IProxyDescriptor Members

        /// <inheritdoc/>
        public override TInterface AdaptInstance<TInterface>(object instance)
        {
            return (TInterface) instance;
        }

        /// <inheritdoc/>
        public override object CreateInstance(Type type, object[] arguments)
        {
            return Activator.CreateInstance(type, arguments);
        }

        #endregion
    }
}