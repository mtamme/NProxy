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

namespace NProxy.Core
{
    /// <summary>
    /// Defines a proxy type.
    /// </summary>
    public interface IProxyType
    {
        /// <summary>
        /// Returns the declaring type.
        /// </summary>
        Type DeclaringType { get; }

        /// <summary>
        /// Returns the parent type.
        /// </summary>
        Type ParentType { get; }

        /// <summary>
        /// Returns the implementation type.
        /// </summary>
        Type ImplementationType { get; }

        /// <summary>
        /// Returns all implemented interfaces.
        /// </summary>
        IEnumerable<Type> ImplementedInterfaces { get; }

        /// <summary>
        /// Returns all intercepted events.
        /// </summary>
        IEnumerable<EventInfo> InterceptedEvents { get; }

        /// <summary>
        /// Returns all intercepted properties.
        /// </summary>
        IEnumerable<PropertyInfo> InterceptedProperties { get; }

        /// <summary>
        /// Returns all intercepted methods.
        /// </summary>
        IEnumerable<MethodInfo> InterceptedMethods { get; }

        /// <summary>
        /// Adapts a proxy to the specified interface type.
        /// </summary>
        /// <param name="interfaceType">The interface type.</param>
        /// <param name="proxy">The proxy object.</param>
        /// <returns>The object, of the specified interface type, to which the proxy has been adapted.</returns>
        object AdaptProxy(Type interfaceType, object proxy);

        /// <summary>
        /// Creates a new proxy.
        /// </summary>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new proxy object.</returns>
        object CreateProxy(IInvocationHandler invocationHandler, params object[] arguments);
    }

    /// <summary>
    /// Defines a proxy type.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    public interface IProxyType<out T> : IProxyType where T : class
    {
        /// <summary>
        /// Creates a new proxy.
        /// </summary>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new proxy object.</returns>
        new T CreateProxy(IInvocationHandler invocationHandler, params object[] arguments);
    }
}