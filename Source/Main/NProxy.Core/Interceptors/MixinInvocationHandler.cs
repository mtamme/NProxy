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
        public object Invoke(object target, MethodInfo methodInfo, object[] parameters)
        {
            var declaringType = methodInfo.DeclaringType;
            object mixin;

            if (_mixins.TryGetValue(declaringType, out mixin))
                return methodInfo.Invoke(mixin, parameters);

            return _invocationHandler.Invoke(target, methodInfo, parameters);
        }

        #endregion
    }
}