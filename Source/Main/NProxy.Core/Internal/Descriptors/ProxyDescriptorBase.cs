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

namespace NProxy.Core.Internal.Descriptors
{
    /// <summary>
    /// Represents the proxy descriptor base class.
    /// </summary>
    internal abstract class ProxyDescriptorBase : IProxyDescriptor, IEquatable<ProxyDescriptorBase>
    {
        /// <summary>
        /// The declaring type.
        /// </summary>
        private readonly Type _declaringType;

        /// <summary>
        /// The parent type.
        /// </summary>
        private readonly Type _parentType;

        /// <summary>
        /// The declaring interface types.
        /// </summary>
        private readonly HashSet<Type> _declaringInterfaceTypes;

        /// <summary>
        /// The additional interface types.
        /// </summary>
        private readonly HashSet<Type> _additionalInterfaceTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyDescriptorBase"/> class.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="parentType">The parent type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        protected ProxyDescriptorBase(Type declaringType, Type parentType, IEnumerable<Type> interfaceTypes)
        {
            if (declaringType == null)
                throw new ArgumentNullException("declaringType");

            if (parentType == null)
                throw new ArgumentNullException("parentType");

            if (interfaceTypes == null)
                throw new ArgumentNullException("interfaceTypes");

            _declaringType = declaringType;
            _parentType = parentType;
            _declaringInterfaceTypes = ExtractInterfaces(declaringType);
            _additionalInterfaceTypes = ExtractAdditionalInterfaces(interfaceTypes, _declaringInterfaceTypes);
        }

        /// <summary>
        /// Extracts all interface types for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The interface types.</returns>
        private static HashSet<Type> ExtractInterfaces(Type type)
        {
            var interfaceTypes = new HashSet<Type>();

            // Add interface type.
            if (type.IsInterface)
                interfaceTypes.Add(type);

            // Add inherited interface types.
            var inheritedInterfaceTypes = type.GetInterfaces();

            interfaceTypes.UnionWith(inheritedInterfaceTypes);

            return interfaceTypes;
        }

        /// <summary>
        /// Extracts all additional interface types.
        /// </summary>
        /// <param name="interfaceTypes">The interface types.</param>
        /// <param name="declaringInterfaceTypes">The declaring interface types.</param>
        /// <returns>The additional interface types.</returns>
        private static HashSet<Type> ExtractAdditionalInterfaces(IEnumerable<Type> interfaceTypes, ICollection<Type> declaringInterfaceTypes)
        {
            var additionalInterfaceTypes = new HashSet<Type>();

            foreach (var interfaceType in interfaceTypes)
            {
                AddAdditionalInterfaces(interfaceType, declaringInterfaceTypes, additionalInterfaceTypes);
            }

            return additionalInterfaceTypes;
        }

        /// <summary>
        /// Adds additional interface types.
        /// </summary>
        /// <param name="interfaceType">The interface type.</param>
        /// <param name="declaringInterfaceTypes">The declaring interface types.</param>
        /// <param name="additionalInterfaceTypes">The additional interface types.</param>
        private static void AddAdditionalInterfaces(Type interfaceType, ICollection<Type> declaringInterfaceTypes, ISet<Type> additionalInterfaceTypes)
        {
            if (interfaceType == null)
                throw new ArgumentException("Interface type must not be null");

            if (!interfaceType.IsInterface)
                throw new ArgumentException(String.Format("Type '{0}' is not an interface type", interfaceType));

            if (interfaceType.IsGenericTypeDefinition)
                throw new ArgumentException(String.Format("Interface type '{0}' must not be a generic type definition", interfaceType));

            // Add interface type.
            if (declaringInterfaceTypes.Contains(interfaceType))
                return;

            if (!additionalInterfaceTypes.Add(interfaceType))
                return;

            // Add inherited interface types.
            var inheritedInterfaceTypes = interfaceType.GetInterfaces();

            foreach (var inheritedInterfaceType in inheritedInterfaceTypes)
            {
                if (declaringInterfaceTypes.Contains(inheritedInterfaceType))
                    continue;

                additionalInterfaceTypes.Add(inheritedInterfaceType);
            }
        }

        /// <summary>
        /// Returns all declaring interface types.
        /// </summary>
        protected IEnumerable<Type> DeclaringInterfaceTypes
        {
            get { return _declaringInterfaceTypes; }
        }

        #region IProxyDescriptor Members

        /// <inheritdoc/>
        public Type DeclaringType
        {
            get { return _declaringType; }
        }

        /// <inheritdoc/>
        public Type ParentType
        {
            get { return _parentType; }
        }

        /// <inheritdoc/>
        public virtual void Accept(IProxyDescriptorVisitor proxyDescriptorVisitor)
        {
            if (proxyDescriptorVisitor == null)
                throw new ArgumentNullException("proxyDescriptorVisitor");

            // Visit parent type constructors.
            proxyDescriptorVisitor.VisitConstructors(_parentType);

            // Visit additional interface types.
            proxyDescriptorVisitor.VisitInterfaces(_additionalInterfaceTypes);
        }

        /// <inheritdoc/>
        public abstract object GetProxyInstance(object instance);

        /// <inheritdoc/>
        public abstract object CreateInstance(Type proxyType, object[] arguments);

        #endregion

        #region IEquatable<ProxyDescriptorBase> Members

        /// <inheritdoc/>
        public bool Equals(ProxyDescriptorBase other)
        {
            if (other == null)
                return false;

            // Compare declaring type.
            if (other._declaringType != _declaringType)
                return false;

            // Compare parent type.
            if (other._parentType != _parentType)
                return false;

            // Compare additional interface types.
            var additionalInterfaceTypes = other._additionalInterfaceTypes;

            if (additionalInterfaceTypes.Count != _additionalInterfaceTypes.Count)
                return false;

            return additionalInterfaceTypes.All(_additionalInterfaceTypes.Contains);
        }

        #endregion

        #region Object Members

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return (obj is ProxyDescriptorBase) && Equals((ProxyDescriptorBase) obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return DeclaringType.GetHashCode();
        }

        #endregion
    }
}