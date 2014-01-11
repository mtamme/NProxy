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
        public override object UnwrapInstance(object instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            var delegateInstance = instance as Delegate;

            if (delegateInstance == null)
                throw new InvalidOperationException(Resources.InvalidInstanceType);

            var proxyInstance = delegateInstance.Target;

            if (proxyInstance == null)
                throw new InvalidOperationException(Resources.InvalidInstanceType);

            return proxyInstance;
        }

        /// <inheritdoc/>
        public override object CreateInstance(Type proxyType, object[] arguments)
        {
            if (proxyType == null)
                throw new ArgumentNullException("proxyType");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            var proxyInstance = Activator.CreateInstance(proxyType, arguments);

            return Delegate.CreateDelegate(DeclaringType, proxyInstance, DelegateMethodName);
        }

        #endregion
    }
}