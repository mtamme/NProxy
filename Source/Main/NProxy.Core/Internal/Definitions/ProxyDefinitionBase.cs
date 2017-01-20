﻿//
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
using System.Linq;

namespace NProxy.Core.Internal.Definitions
{
    /// <summary>
    /// Represents the proxy definition base class.
    /// </summary>
    internal abstract class ProxyDefinitionBase : IProxyDefinition, IEquatable<ProxyDefinitionBase>
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

        private readonly Type _invocationHandlerFactoryType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyDefinitionBase"/> class.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="parentType">The parent type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        protected ProxyDefinitionBase(Type declaringType, Type parentType, IEnumerable<Type> interfaceTypes, Type invocationHandlerFactoryType)
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
            _invocationHandlerFactoryType = invocationHandlerFactoryType;
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
        private static void AddAdditionalInterfaces(Type interfaceType, ICollection<Type> declaringInterfaceTypes, HashSet<Type> additionalInterfaceTypes)
        {
            if (interfaceType == null)
                throw new ArgumentException(Resources.InterfaceTypeMustNotBeNull, "interfaceType");

            if (!interfaceType.IsInterface)
                throw new ArgumentException(String.Format(Resources.TypeNotAnInterfaceType, interfaceType), "interfaceType");

            if (interfaceType.IsGenericTypeDefinition)
                throw new ArgumentException(String.Format(Resources.InterfaceTypeMustNotBeAGenericTypeDefinition, interfaceType), "interfaceType");

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
        protected IEnumerable<Type> DeclaringInterfaces
        {
            get { return _declaringInterfaceTypes; }
        }

        /// <summary>
        /// Returns all additional interface types.
        /// </summary>
        protected IEnumerable<Type> AdditionalInterfaces
        {
            get { return _additionalInterfaceTypes; }
        }

        #region IProxyDefinition Members

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
        public abstract IEnumerable<Type> ImplementedInterfaces { get; }

        public Type InvocationHandlerFactoryType { get { return _invocationHandlerFactoryType; } }

        /// <inheritdoc/>
        public virtual void AcceptVisitor(IProxyDefinitionVisitor proxyDefinitionVisitor)
        {
            if (proxyDefinitionVisitor == null)
                throw new ArgumentNullException("proxyDefinitionVisitor");

            // Visit parent type constructors.
            proxyDefinitionVisitor.VisitConstructors(_parentType);

            // Visit additional interface types.
            proxyDefinitionVisitor.VisitInterfaces(_additionalInterfaceTypes);
        }

        /// <inheritdoc/>
        public abstract object UnwrapProxy(object proxy);

        /// <inheritdoc/>
        public abstract object CreateProxy(Type type, object[] arguments);

        #endregion

        #region IEquatable<ProxyDefinitionBase> Members

        /// <inheritdoc/>
        public bool Equals(ProxyDefinitionBase other)
        {
            if (other == null)
                return false;

            // Compare declaring type.
            if (other._declaringType != _declaringType)
                return false;

            // Compare parent type.
            if (other._parentType != _parentType)
                return false;

            if (other._invocationHandlerFactoryType != _invocationHandlerFactoryType)
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
            return (obj is ProxyDefinitionBase) && Equals((ProxyDefinitionBase)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return _declaringType.GetHashCode();
        }

        #endregion
    }
}