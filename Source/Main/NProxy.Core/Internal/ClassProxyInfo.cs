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
using System.Linq;

namespace NProxy.Core.Internal
{
    /// <summary>
    /// Represents a class proxy information.
    /// </summary>
    internal sealed class ClassProxyInfo : ProxyInfoBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassProxyInfo"/> class.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        public ClassProxyInfo(Type declaringType, IEnumerable<Type> interfaceTypes)
            : base(declaringType, declaringType, interfaceTypes)
        {
        }

        #region IProxyInfo Members

        /// <inheritdoc/>
        public override IEnumerable<Type> ImplementedInterfaces
        {
            get { return DeclaringInterfaces.Concat(AdditionalInterfaces); }
        }

        /// <inheritdoc/>
        public override void AcceptVisitor(IProxyInfoVisitor proxyInfoVisitor)
        {
            base.AcceptVisitor(proxyInfoVisitor);

            // Visit declaring type members.
            proxyInfoVisitor.VisitMembers(DeclaringType);
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