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
using NProxy.Core.Interceptors.Language;
using NProxy.Core.Internal;

namespace NProxy.Core.Interceptors
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
        private readonly IProxyFactory _proxyFactory;

        /// <summary>
        /// The constructor arguments.
        /// </summary>
        private readonly object[] _arguments;

        /// <summary>
        /// The mixins.
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
        public CreateProxy(IProxyFactory proxyFactory, params object[] arguments)
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
        /// <param name="mixin">The mixin.</param>
        private void AddMixin(object mixin)
        {
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
        /// <param name="mixin">The mixin.</param>
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
        /// <param name="proxyTemplate">The proxy template.</param>
        /// <param name="defaultInterceptors">The default interceptors.</param>
        /// <returns>The invocation handler.</returns>
        private IInvocationHandler CreateInvocationHandler(IProxyTemplate proxyTemplate, params IInterceptor[] defaultInterceptors)
        {
            var invocationHandler = new InterceptorInvocationHandler(defaultInterceptors);

            invocationHandler.ApplyInterceptors(proxyTemplate, _interceptors);

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
            var proxyTemplate = _proxyFactory.GetProxyTemplate<T>(_interfaceTypes);
            var invocationHandler = CreateInvocationHandler(proxyTemplate, new StaticTargetInterceptor(target));

            return proxyTemplate.CreateProxy(invocationHandler, _arguments);
        }

        /// <inheritdoc/>
        public T Target(Func<object, object> targetFactory)
        {
            var proxyTemplate = _proxyFactory.GetProxyTemplate<T>(_interfaceTypes);
            var invocationHandler = CreateInvocationHandler(proxyTemplate, new DynamicTargetInterceptor(targetFactory));

            return proxyTemplate.CreateProxy(invocationHandler, _arguments);
        }

        /// <inheritdoc/>
        public T TargetBase()
        {
            var proxyTemplate = _proxyFactory.GetProxyTemplate<T>(_interfaceTypes);
            var invocationHandler = CreateInvocationHandler(proxyTemplate, BaseTargetInterceptor.Instance);

            return proxyTemplate.CreateProxy(invocationHandler, _arguments);
        }

        #endregion
    }
}