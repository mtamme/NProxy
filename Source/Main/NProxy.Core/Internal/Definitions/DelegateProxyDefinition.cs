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

namespace NProxy.Core.Internal.Definitions
{
    /// <summary>
    /// Represents a delegate proxy definition.
    /// </summary>
    internal sealed class DelegateProxyDefinition : ProxyDefinitionBase
    {
        /// <summary>
        /// The name of the delegate method.
        /// </summary>
        private const string DelegateMethodName = "Invoke";

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateProxyDefinition"/> class.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The interface types.</param>
        public DelegateProxyDefinition(Type declaringType, IEnumerable<Type> interfaceTypes)
            : base(declaringType, typeof (object), interfaceTypes)
        {
        }

        #region IProxyDefinition Members

        /// <inheritdoc/>
        public override IEnumerable<Type> ImplementedInterfaces
        {
            get { return AdditionalInterfaces; }
        }

        /// <inheritdoc/>
        public override void AcceptVisitor(IProxyDefinitionVisitor proxyDefinitionVisitor)
        {
            base.AcceptVisitor(proxyDefinitionVisitor);

            // Visit declaring type method.
            var methodInfo = DeclaringType.GetMethod(
                DelegateMethodName,
                BindingFlags.Public | BindingFlags.Instance);

            proxyDefinitionVisitor.VisitMethod(methodInfo);

            // Visit parent type members.
            proxyDefinitionVisitor.VisitMembers(ParentType);
        }

        /// <inheritdoc/>
        public override object UnwrapProxy(object proxy)
        {
            if (proxy == null)
                throw new ArgumentNullException("proxy");

            var delegateProxy = proxy as Delegate;

            if (delegateProxy == null)
                throw new InvalidOperationException(Resources.InvalidProxyType);

            var target = delegateProxy.Target;

            if (target == null)
                throw new InvalidOperationException(Resources.InvalidProxyType);

            return target;
        }

        /// <inheritdoc/>
        public override object CreateProxy(Type type, object[] arguments)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            var target = Activator.CreateInstance(type, arguments);

            return Delegate.CreateDelegate(DeclaringType, target, DelegateMethodName);
        }

        #endregion
    }
}