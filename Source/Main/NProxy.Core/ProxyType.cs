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
using NProxy.Core.Internal;

namespace NProxy.Core
{
    /// <summary>
    /// Represents a proxy type.
    /// </summary>
    internal class ProxyType : IProxyType
    {
        /// <summary>
        /// The proxy information.
        /// </summary>
        private readonly IProxyInfo _proxyInfo;

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
        /// Initializes a new instance of the <see cref="ProxyType"/> class.
        /// </summary>
        /// <param name="proxyInfo">The proxy information.</param>
        /// <param name="implementationType">The implementation type.</param>
        /// <param name="eventInfos">The event informations.</param>
        /// <param name="propertyInfos">The property informations.</param>
        /// <param name="methodInfos">The method informations.</param>
        public ProxyType(IProxyInfo proxyInfo, Type implementationType, ICollection<EventInfo> eventInfos, ICollection<PropertyInfo> propertyInfos, ICollection<MethodInfo> methodInfos)
        {
            if (proxyInfo == null)
                throw new ArgumentNullException("proxyInfo");

            if (implementationType == null)
                throw new ArgumentNullException("implementationType");

            if (eventInfos == null)
                throw new ArgumentNullException("eventInfos");

            if (propertyInfos == null)
                throw new ArgumentNullException("propertyInfos");

            if (methodInfos == null)
                throw new ArgumentNullException("methodInfos");

            _proxyInfo = proxyInfo;
            _implementationType = implementationType;
            _eventInfos = eventInfos;
            _propertyInfos = propertyInfos;
            _methodInfos = methodInfos;
        }

        #region IProxyType Members

        /// <inheritdoc/>
        public Type DeclaringType
        {
            get { return _proxyInfo.DeclaringType; }
        }

        /// <inheritdoc/>
        public Type ParentType
        {
            get { return _proxyInfo.ParentType; }
        }

        /// <inheritdoc/>
        public Type ImplementationType
        {
            get { return _implementationType; }
        }

        /// <inheritdoc/>
        public IEnumerable<Type> ImplementedInterfaces
        {
            get { return _proxyInfo.ImplementedInterfaces; }
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

            var instance = _proxyInfo.UnwrapProxy(proxy);
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

            return _proxyInfo.CreateProxy(_implementationType, constructorArguments.ToArray());
        }

        #endregion
    }

    /// <summary>
    /// Represents a proxy type.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    internal sealed class ProxyType<T> : IProxyType<T> where T : class
    {
        /// <summary>
        /// The proxy type.
        /// </summary>
        private readonly IProxyType _proxyType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyType{T}"/> class.
        /// </summary>
        /// <param name="proxyType">The proxy type.</param>
        public ProxyType(IProxyType proxyType)
        {
            if (proxyType == null)
                throw new ArgumentNullException("proxyType");

            _proxyType = proxyType;
        }

        #region IProxyType Members

        /// <inheritdoc/>
        public Type DeclaringType
        {
            get { return _proxyType.DeclaringType; }
        }

        /// <inheritdoc/>
        public Type ParentType
        {
            get { return _proxyType.ParentType; }
        }

        /// <inheritdoc/>
        public Type ImplementationType
        {
            get { return _proxyType.ImplementationType; }
        }

        /// <inheritdoc/>
        public IEnumerable<Type> ImplementedInterfaces
        {
            get { return _proxyType.ImplementedInterfaces; }
        }

        /// <inheritdoc/>
        public IEnumerable<EventInfo> InterceptedEvents
        {
            get { return _proxyType.InterceptedEvents; }
        }

        /// <inheritdoc/>
        public IEnumerable<PropertyInfo> InterceptedProperties
        {
            get { return _proxyType.InterceptedProperties; }
        }

        /// <inheritdoc/>
        public IEnumerable<MethodInfo> InterceptedMethods
        {
            get { return _proxyType.InterceptedMethods; }
        }

        /// <inheritdoc/>
        public object AdaptProxy(Type interfaceType, object proxy)
        {
            return _proxyType.AdaptProxy(interfaceType, proxy);
        }

        /// <inheritdoc/>
        object IProxyType.CreateProxy(IInvocationHandler invocationHandler, params object[] arguments)
        {
            return _proxyType.CreateProxy(invocationHandler, arguments);
        }

        #endregion

        #region IProxyType<T> Members

        /// <inheritdoc/>
        public T CreateProxy(IInvocationHandler invocationHandler, params object[] arguments)
        {
            return (T) _proxyType.CreateProxy(invocationHandler, arguments);
        }

        #endregion
    }
}