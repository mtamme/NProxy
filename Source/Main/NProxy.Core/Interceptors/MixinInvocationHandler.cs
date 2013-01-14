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
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Represents a mixin invocation handler.
    /// </summary>
    [Serializable]
    internal sealed class MixinInvocationHandler : IInvocationHandler
    {
        /// <summary>
        /// The next invocation handler.
        /// </summary>
        private readonly IInvocationHandler _invocationHandler;

        /// <summary>
        /// The mixin objects.
        /// </summary>
        private readonly Dictionary<Type, object> _mixins;

        /// <summary>
        /// Initializes a new instance of the <see cref="MixinInvocationHandler"/> class.
        /// </summary>
        /// <param name="mixins">The mixin objects.</param>
        /// <param name="invocationHandler">The next invocation handler.</param>
        public MixinInvocationHandler(IDictionary<Type, object> mixins, IInvocationHandler invocationHandler)
        {
            if (mixins == null)
                throw new ArgumentNullException("mixins");

            if (invocationHandler == null)
                throw new ArgumentNullException("invocationHandler");

            _invocationHandler = invocationHandler;

            _mixins = new Dictionary<Type, object>(mixins);
        }

        #region IInvocationHandler Members

        /// <inheritdoc/>
        public object Invoke(object proxy, MethodInfo methodInfo, object[] parameters)
        {
            var declaringType = methodInfo.GetDeclaringType();
            object target;

            if (_mixins.TryGetValue(declaringType, out target))
                return methodInfo.Invoke(target, parameters);

            return _invocationHandler.Invoke(proxy, methodInfo, parameters);
        }

        #endregion
    }
}