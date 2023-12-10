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
using NProxy.Core;
using NProxy.Interceptors.Language;

namespace NProxy.Interceptors
{
    /// <summary>
    /// Represents a fluent interface implementation for creating a new proxy.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    internal sealed class CreateProxy<T> : ICreateProxy<T> where T : class
    {
        /// <summary>
        /// The proxy factory.
        /// </summary>
        private readonly IProxyTypeRegistry _proxyFactory;

        /// <summary>
        /// The constructor arguments.
        /// </summary>
        private readonly object[] _arguments;

        /// <summary>
        /// The mixin objects.
        /// </summary>
        private readonly Dictionary<Type, object> _mixins;

        /// <summary>
        /// The interface types.
        /// </summary>
        private readonly HashSet<Type> _interfaceTypes;

        /// <summary>
        /// The interceptors.
        /// </summary>
        private readonly List<IInterceptor> _interceptors;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProxy{T}"/> class.
        /// </summary>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="arguments">The constructor arguments.</param>
        public CreateProxy(IProxyTypeRegistry proxyFactory, object[] arguments)
        {
            if (proxyFactory == null)
                throw new ArgumentNullException("proxyFactory");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            _proxyFactory = proxyFactory;
            _arguments = arguments;

            _mixins = new Dictionary<Type, object>();
            _interfaceTypes = new HashSet<Type>();
            _interceptors = new List<IInterceptor>();
        }

        /// <summary>
        /// Adds a mixin.
        /// </summary>
        /// <param name="mixin">The mixin object.</param>
        private void AddMixin(object mixin)
        {
            if (mixin == null)
                throw new ArgumentNullException("mixin");

            var mixinType = mixin.GetType();
            var interfaceTypes = mixinType.GetInterfaces();

            foreach (var interfaceType in interfaceTypes)
            {
                AddMixin(interfaceType, mixin);
            }
        }

        /// <summary>
        /// Adds a mixin for the specified interface type.
        /// </summary>
        /// <param name="interfaceType">The interface type.</param>
        /// <param name="mixin">The mixin object.</param>
        private void AddMixin(Type interfaceType, object mixin)
        {
            AddInterface(interfaceType);

            _mixins.Add(interfaceType, mixin);
        }

        /// <summary>
        /// Adds an interface type.
        /// </summary>
        /// <param name="interfaceType">The interface type.</param>
        private void AddInterface(Type interfaceType)
        {
            if (!_interfaceTypes.Add(interfaceType))
                throw new InvalidOperationException(String.Format(Resources.InterfaceTypeWasAlreadyAdded, interfaceType));
        }

        /// <summary>
        /// Creates an invocation handler.
        /// </summary>
        /// <param name="proxyType">The proxy type.</param>
        /// <param name="defaultInterceptors">The default interceptors.</param>
        /// <returns>The invocation handler.</returns>
        private IInvocationHandler CreateInvocationHandler(IProxyType proxyType, params IInterceptor[] defaultInterceptors)
        {
            var invocationHandler = new InterceptorInvocationHandler(defaultInterceptors);

            invocationHandler.ApplyInterceptors(proxyType, _interceptors);

            if (_mixins.Count > 0)
                return new MixinInvocationHandler(_mixins, invocationHandler);

            return invocationHandler;
        }

        #region IExtendWith<T, IInterceptor> Members

        /// <inheritdoc/>
        public IExtendWith<T, IInterceptor> ExtendWith<TMixin>() where TMixin : class, new()
        {
            var mixin = new TMixin();

            AddMixin(mixin);

            return this;
        }

        /// <inheritdoc/>
        public IExtendWith<T, IInterceptor> ExtendWith(params object[] mixins)
        {
            if (mixins == null)
                throw new ArgumentNullException("mixins");

            foreach (var mixin in mixins)
            {
                AddMixin(mixin);
            }

            return this;
        }

        #endregion

        #region IImplement<T, IInterceptor> Members

        /// <inheritdoc/>
        public IImplement<T, IInterceptor> Implement<TInterface>() where TInterface : class
        {
            var interfaceType = typeof (TInterface);

            AddInterface(interfaceType);

            return this;
        }

        /// <inheritdoc/>
        public IImplement<T, IInterceptor> Implement(params Type[] interfaceTypes)
        {
            if (interfaceTypes == null)
                throw new ArgumentNullException("interfaceTypes");

            foreach (var interfaceType in interfaceTypes)
            {
                AddInterface(interfaceType);
            }

            return this;
        }

        #endregion

        #region IInterceptBy<T, IInterceptor> Members

        /// <inheritdoc/>
        public ITarget<T> InterceptBy(params IInterceptor[] interceptors)
        {
            if (interceptors == null)
                throw new ArgumentNullException("interceptors");

            _interceptors.AddRange(interceptors);

            return this;
        }

        #endregion

        #region ITarget<T> Members

        /// <inheritdoc/>
        public T Target<TTarget>() where TTarget : class, new()
        {
            var target = new TTarget();

            return Target(target);
        }

        /// <inheritdoc/>
        public T Target(T target)
        {
            return Target((object) target);
        }

        /// <inheritdoc/>
        public T Target(object target)
        {
            var proxyType = _proxyFactory.GetProxyType<T>(_interfaceTypes);
            var invocationHandler = CreateInvocationHandler(proxyType, new StaticTargetInterceptor(target));

            return proxyType.CreateProxy(invocationHandler, _arguments);
        }

        /// <inheritdoc/>
        public T Target(Func<object, object> targetFactory)
        {
            var proxyType = _proxyFactory.GetProxyType<T>(_interfaceTypes);
            var invocationHandler = CreateInvocationHandler(proxyType, new DynamicTargetInterceptor(targetFactory));

            return proxyType.CreateProxy(invocationHandler, _arguments);
        }

        /// <inheritdoc/>
        public T TargetBase()
        {
            var proxyType = _proxyFactory.GetProxyType<T>(_interfaceTypes);
            var invocationHandler = CreateInvocationHandler(proxyType, BaseTargetInterceptor.Instance);

            return proxyType.CreateProxy(invocationHandler, _arguments);
        }

        #endregion
    }
}