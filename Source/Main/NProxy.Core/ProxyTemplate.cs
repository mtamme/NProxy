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
using NProxy.Core.Internal;
using NProxy.Core.Internal.Definitions;

namespace NProxy.Core
{
    /// <summary>
    /// Represents a proxy template.
    /// </summary>
    internal class ProxyTemplate : IProxyTemplate
    {
        /// <summary>
        /// The proxy definition.
        /// </summary>
        private readonly IProxyDefinition _proxyDefinition;

        /// <summary>
        /// The implementation type.
        /// </summary>
        private readonly Type _implementationType;

        /// <summary>
        /// The event informations.
        /// </summary>
        private readonly ICollection<EventInfo> _eventInfos;

        /// <summary>
        /// The property informations.
        /// </summary>
        private readonly ICollection<PropertyInfo> _propertyInfos;

        /// <summary>
        /// The method informations.
        /// </summary>
        private readonly ICollection<MethodInfo> _methodInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyTemplate"/> class.
        /// </summary>
        /// <param name="proxyDefinition">The proxy definition.</param>
        /// <param name="implementationType">The implementation type.</param>
        /// <param name="eventInfos">The event informations.</param>
        /// <param name="propertyInfos">The property informations.</param>
        /// <param name="methodInfos">The method informations.</param>
        public ProxyTemplate(IProxyDefinition proxyDefinition, Type implementationType, ICollection<EventInfo> eventInfos, ICollection<PropertyInfo> propertyInfos, ICollection<MethodInfo> methodInfos)
        {
            if (proxyDefinition == null)
                throw new ArgumentNullException("proxyDefinition");

            if (implementationType == null)
                throw new ArgumentNullException("implementationType");

            if (eventInfos == null)
                throw new ArgumentNullException("eventInfos");

            if (propertyInfos == null)
                throw new ArgumentNullException("propertyInfos");

            if (methodInfos == null)
                throw new ArgumentNullException("methodInfos");

            _proxyDefinition = proxyDefinition;
            _implementationType = implementationType;
            _eventInfos = eventInfos;
            _propertyInfos = propertyInfos;
            _methodInfos = methodInfos;
        }

        #region IProxyTemplate Members

        /// <inheritdoc/>
        public Type DeclaringType
        {
            get { return _proxyDefinition.DeclaringType; }
        }

        /// <inheritdoc/>
        public Type ParentType
        {
            get { return _proxyDefinition.ParentType; }
        }

        /// <inheritdoc/>
        public Type ImplementationType
        {
            get { return _implementationType; }
        }

        /// <inheritdoc/>
        public IEnumerable<Type> ImplementedInterfaces
        {
            get { return _proxyDefinition.ImplementedInterfaces; }
        }

        /// <inheritdoc/>
        public IEnumerable<EventInfo> InterceptedEvents
        {
            get { return _eventInfos; }
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyInfo> InterceptedProperties
        {
            get { return _propertyInfos; }
        }

        /// <inheritdoc/>
        public IEnumerable<MethodInfo> InterceptedMethods
        {
            get { return _methodInfos; }
        }

        /// <inheritdoc/>
        public object AdaptProxy(Type interfaceType, object proxy)
        {
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            if (!interfaceType.IsInterface)
                throw new ArgumentException(String.Format(Resources.TypeNotAnInterfaceType, interfaceType), "interfaceType");

            var instance = _proxyDefinition.UnwrapProxy(proxy);
            var instanceType = instance.GetType();

            if ((instanceType != _implementationType) || !interfaceType.IsAssignableFrom(instanceType))
                throw new InvalidOperationException(Resources.CannotAdaptProxy);

            return instance;
        }

        /// <inheritdoc/>
        public object CreateProxy(IInvocationHandler invocationHandler, params object[] arguments)
        {
            if (invocationHandler == null)
                throw new ArgumentNullException("invocationHandler");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            var constructorArguments = new List<object> {invocationHandler};

            constructorArguments.AddRange(arguments);

            return _proxyDefinition.CreateProxy(_implementationType, constructorArguments.ToArray());
        }

        #endregion
    }

    /// <summary>
    /// Represents a proxy template.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    internal sealed class ProxyTemplate<T> : IProxyTemplate<T> where T : class
    {
        /// <summary>
        /// The proxy template.
        /// </summary>
        private readonly IProxyTemplate _proxyTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyTemplate{T}"/> class.
        /// </summary>
        /// <param name="proxyTemplate">The proxy template.</param>
        public ProxyTemplate(IProxyTemplate proxyTemplate)
        {
            if (proxyTemplate == null)
                throw new ArgumentNullException("proxyTemplate");

            _proxyTemplate = proxyTemplate;
        }

        #region IProxyTemplate Members

        /// <inheritdoc/>
        public Type DeclaringType
        {
            get { return _proxyTemplate.DeclaringType; }
        }

        /// <inheritdoc/>
        public Type ParentType
        {
            get { return _proxyTemplate.ParentType; }
        }

        /// <inheritdoc/>
        public Type ImplementationType
        {
            get { return _proxyTemplate.ImplementationType; }
        }

        /// <inheritdoc/>
        public IEnumerable<Type> ImplementedInterfaces
        {
            get { return _proxyTemplate.ImplementedInterfaces; }
        }

        /// <inheritdoc/>
        public IEnumerable<EventInfo> InterceptedEvents
        {
            get { return _proxyTemplate.InterceptedEvents; }
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyInfo> InterceptedProperties
        {
            get { return _proxyTemplate.InterceptedProperties; }
        }

        /// <inheritdoc/>
        public IEnumerable<MethodInfo> InterceptedMethods
        {
            get { return _proxyTemplate.InterceptedMethods; }
        }

        /// <inheritdoc/>
        public object AdaptProxy(Type interfaceType, object proxy)
        {
            return _proxyTemplate.AdaptProxy(interfaceType, proxy);
        }

        /// <inheritdoc/>
        object IProxyTemplate.CreateProxy(IInvocationHandler invocationHandler, params object[] arguments)
        {
            return _proxyTemplate.CreateProxy(invocationHandler, arguments);
        }

        #endregion

        #region IProxyTemplate<T> Members

        /// <inheritdoc/>
        public T CreateProxy(IInvocationHandler invocationHandler, params object[] arguments)
        {
            return (T) _proxyTemplate.CreateProxy(invocationHandler, arguments);
        }

        #endregion
    }
}