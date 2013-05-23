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

namespace NProxy.Core.Internal.Descriptors
{
    /// <summary>
    /// Represents a delegate proxy descriptor.
    /// </summary>
    internal sealed class DelegateProxyDescriptor : ProxyDescriptorBase
    {
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

        #region IDescriptor Members

        /// <inheritdoc/>
        public override void Accept(IDescriptorVisitor descriptorVisitor)
        {
            base.Accept(descriptorVisitor);

            // Visit declaring type method.
            var methodInfo = DeclaringType.GetMethod(
                DelegateMethodName,
                BindingFlags.Public | BindingFlags.Instance);

            descriptorVisitor.VisitMethod(methodInfo);
        }

        /// <inheritdoc/>
        public override TInterface Cast<TInterface>(object instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            var interfaceType = typeof (TInterface);

            if (!interfaceType.IsInterface)
                throw new ArgumentException(String.Format("Type '{0}' is not an interface type", interfaceType));

            var target = ((Delegate) instance).Target;

            return (TInterface) target;
        }

        /// <inheritdoc/>
        public override object CreateInstance(Type type, object[] arguments)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            var instance = Activator.CreateInstance(type, arguments);

            return Delegate.CreateDelegate(DeclaringType, instance, DelegateMethodName);
        }

        #endregion
    }
}