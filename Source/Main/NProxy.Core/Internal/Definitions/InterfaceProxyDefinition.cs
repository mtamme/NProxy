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
    /// Represents an interface proxy definition.
    /// </summary>
    internal sealed class InterfaceProxyDefinition : ProxyDefinitionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceProxyDefinition"/> class.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        public InterfaceProxyDefinition(Type declaringType, IEnumerable<Type> interfaceTypes, Type invocationHandlerFactoryType)
            : base(declaringType, typeof (object), interfaceTypes, invocationHandlerFactoryType)
        {
        }

        public InterfaceProxyDefinition(Type declaringType, IEnumerable<Type> interfaceTypes)
            : this(declaringType, interfaceTypes, null)
        {
        }

        #region IProxyDefinition Members

        /// <inheritdoc/>
        public override IEnumerable<Type> ImplementedInterfaces
        {
            get { return DeclaringInterfaces.Concat(AdditionalInterfaces); }
        }

        /// <inheritdoc/>
        public override void AcceptVisitor(IProxyDefinitionVisitor proxyDefinitionVisitor)
        {
            base.AcceptVisitor(proxyDefinitionVisitor);

            // Visit declaring interface types.
            proxyDefinitionVisitor.VisitInterfaces(DeclaringInterfaces);

            // Visit parent type members.
            proxyDefinitionVisitor.VisitMembers(ParentType);
        }

        /// <inheritdoc/>
        public override object UnwrapProxy(object proxy)
        {
            if (proxy == null)
                throw new ArgumentNullException("proxy");

            return proxy;
        }

        /// <inheritdoc/>
        public override object CreateProxy(Type type, object[] arguments)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            return Activator.CreateInstance(type, arguments);
        }

        #endregion
    }
}