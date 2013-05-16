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
        /// The interface types.
        /// </summary>
        private readonly HashSet<Type> _interfaceTypes;

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
            _interfaceTypes = GetInterfaces(interfaceTypes);
        }

        /// <summary>
        /// Returns all interface types.
        /// </summary>
        /// <param name="types">The interface types.</param>
        /// <returns>All interface types.</returns>
        private static HashSet<Type> GetInterfaces(IEnumerable<Type> types)
        {
            var interfaceTypes = new HashSet<Type>();

            foreach (var type in types)
            {
                AddInterfaces(type, interfaceTypes);
            }

            return interfaceTypes;
        }

        /// <summary>
        /// Adds all interface types.
        /// </summary>
        /// <param name="type">The interface type.</param>
        /// <param name="interfaceTypes"></param>
        private static void AddInterfaces(Type type, ISet<Type> interfaceTypes)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (!type.IsInterface)
                throw new ArgumentException(String.Format("Type '{0}' is not an interface type", type), "type");

            if (type.IsGenericTypeDefinition)
                throw new ArgumentException("Interface type must not be a generic type definition", "type");

            // Add interface type.
            interfaceTypes.Add(type);

            // Add inherited interface types.
            var inheritedInterfaceTypes = type.GetInterfaces();

            interfaceTypes.UnionWith(inheritedInterfaceTypes);
        }

        /// <summary>
        /// Creates a type reflector.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="parentType">The parent type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        /// <returns></returns>
        protected abstract ITypeReflector CreateReflector(Type declaringType, Type parentType, IEnumerable<Type> interfaceTypes);

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
        public abstract object CreateInstance(Type type, object[] arguments);

        /// <inheritdoc/>
        public ITypeReflector CreateReflector()
        {
            return CreateReflector(_declaringType, _parentType, _interfaceTypes);
        }

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

            // Compare interface types.
            var interfaceTypes = other._interfaceTypes;

            if (interfaceTypes.Count != _interfaceTypes.Count)
                return false;

            return interfaceTypes.All(_interfaceTypes.Contains);
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