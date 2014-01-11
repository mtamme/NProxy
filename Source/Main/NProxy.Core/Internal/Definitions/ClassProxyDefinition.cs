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

namespace NProxy.Core.Internal.Definitions
{
    /// <summary>
    /// Represents a class proxy definition.
    /// </summary>
    internal sealed class ClassProxyDefinition : ProxyDefinitionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassProxyDefinition"/> class.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        public ClassProxyDefinition(Type declaringType, IEnumerable<Type> interfaceTypes)
            : base(declaringType, declaringType, interfaceTypes)
        {
        }

        #region IProxyDefinition Members

        /// <inheritdoc/>
        public override void AcceptVisitor(IProxyDefinitionVisitor proxyDefinitionVisitor)
        {
            base.AcceptVisitor(proxyDefinitionVisitor);

            // Visit declaring type members.
            proxyDefinitionVisitor.VisitMembers(DeclaringType);
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