//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © 2012 Martin Tamme
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
using NProxy.Core.Interceptors;
using NProxy.Core.Interceptors.Language;
using NProxy.Core.Internal.Common;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Represents the proxy configuration.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    internal sealed class ProxyConfiguration<T> : IProxyConfiguration<T> where T : class
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
        /// The mixin objects.
        /// </summary>
        private readonly Dictionary<Type, object> _mixins;

        /// <summary>
        /// The interface types.
        /// </summary>
        private readonly List<Type> _interfaceTypes;

        /// <summary>
        /// The invocation target.
        /// </summary>
        private IInvocationTarget _invocationTarget;

        /// <summary>
        /// Initializes a new instance of the <see cref="Configure{T}"/> class.
        /// </summary>
        /// <param name="proxyFactory">The proxy factory.</param>
        /// <param name="arguments">The constructor arguments.</param>
        public ProxyConfiguration(IProxyFactory proxyFactory, object[] arguments)
        {
            if (proxyFactory == null)
                throw new ArgumentNullException("proxyFactory");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            _proxyFactory = proxyFactory;
            _arguments = arguments;

            _mixins = new Dictionary<Type, object>();
            _interfaceTypes = new List<Type>();
            _invocationTarget = null;
        }

        #region IExtends<T> Members

        /// <inheritdoc/>
        public IExtends<T> Extends<TMixin>() where TMixin : class, new()
        {
            var mixin = new TMixin();

            return Extends(mixin);
        }

        /// <inheritdoc/>
        public IExtends<T> Extends<TMixin>(TMixin mixin) where TMixin : class
        {
            if (mixin == null)
                throw new ArgumentNullException("mixin");

            var mixinType = mixin.GetType();
            var interfaceVisitor = Visitor.Create<Type>(t => _mixins.Add(t, mixin))
                .Where(t => !_mixins.ContainsKey(t));

            mixinType.VisitInterfaces(interfaceVisitor);

            return this;
        }

        #endregion

        #region IImplements<T> Members

        /// <inheritdoc/>
        public IImplements<T> Implements<TInterface>() where TInterface : class
        {
            var interfaceType = typeof (TInterface);

            return Implements(interfaceType);
        }

        /// <inheritdoc/>
        public IImplements<T> Implements(Type interfaceType)
        {
            if (interfaceType == null)
                throw new ArgumentNullException("interfaceType");

            _interfaceTypes.Add(interfaceType);

            return this;
        }

        #endregion

        #region ITargets<T> Members

        /// <inheritdoc/>
        public IInvokes<T> Targets<TTarget>() where TTarget : class, new()
        {
            var target = new TTarget();

            return Targets(target);
        }

        /// <inheritdoc/>
        public IInvokes<T> Targets<TTarget>(TTarget target) where TTarget : class
        {
            var invocationTarget = new SingleInvocationTarget(target);

            return Targets(invocationTarget);
        }

        /// <inheritdoc/>
        public IInvokes<T> Targets(IInvocationTarget invocationTarget)
        {
            if (invocationTarget == null)
                throw new ArgumentNullException("invocationTarget");

            _invocationTarget = invocationTarget;

            return this;
        }

        #endregion

        #region IInvokes<T> Members

        /// <inheritdoc/>
        public T Invokes(IEnumerable<IInterceptor> interceptors)
        {
            var invocationHandler = new InterceptorChain(interceptors);

            return Invokes(invocationHandler);
        }

        /// <inheritdoc/>
        public T Invokes(IInterceptor interceptor)
        {
            var invocationHandler = new InterceptorChain(interceptor);

            return Invokes(invocationHandler);
        }

        /// <inheritdoc/>
        public T Invokes(IInvocationHandler invocationHandler)
        {
            if (_mixins.Count > 0)
                invocationHandler = new MixinInvocationHandler(_mixins, invocationHandler);

            return _proxyFactory.CreateProxy<T>(_interfaceTypes, invocationHandler, _arguments);
        }

        #endregion
    }
}
