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
    /// Represents a delegate proxy descriptor.
    /// </summary>
    internal sealed class DelegateProxyDescriptor : ProxyDescriptorBase
    {
        /// <summary>
        /// Represents a delegate proxy type reflector.
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
            }

            /// <inheritdoc/>
            public override void VisitProperties(IVisitor<PropertyInfo> visitor)
            {
                // Visit interface properties.
                _interfaceTypes.Visit(t => t.VisitProperties(visitor));
            }

            /// <inheritdoc/>
            public override void VisitMethods(IVisitor<MethodInfo> visitor)
            {
                if (visitor == null)
                    throw new ArgumentNullException("visitor");

                // Visit interface methods.
                _interfaceTypes.Visit(t => t.VisitMethods(visitor));

                // Visit declaring type method.
                var methodInfo = DeclaringType.GetMethod(
                    DelegateMethodName,
                    BindingFlags.Public | BindingFlags.Instance);

                visitor.Visit(methodInfo);
            }

            #endregion
        }

        /// <summary>
        /// The name of the delegate method.
        /// </summary>
        private const string DelegateMethodName = "Invoke";

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateProxyDescriptor"/> class.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        public DelegateProxyDescriptor(Type declaringType, IEnumerable<Type> interfaceTypes)
            : base(declaringType, typeof (object), interfaceTypes)
        {
        }

        #region ProxyDescriptorBase Members

        /// <inheritdoc/>
		protected override ITypeReflector CreateReflector(Type declaringType, Type parentType, ICollection<Type> declaringInterfaceTypes, ICollection<Type> additionalInterfaceTypes)
        {
			return new TypeReflector(declaringType, parentType, additionalInterfaceTypes);
        }

        #endregion

        #region IProxyDescriptor Members

        /// <inheritdoc/>
        public override object CreateInstance(Type type, object[] arguments)
        {
            var instance = Activator.CreateInstance(type, arguments);

            return Delegate.CreateDelegate(DeclaringType, instance, DelegateMethodName);
        }

        #endregion
    }
}