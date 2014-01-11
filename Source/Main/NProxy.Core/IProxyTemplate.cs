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

namespace NProxy.Core
{
    /// <summary>
    /// Defines a proxy template.
    /// </summary>
    public interface IProxyTemplate
    {
        /// <summary>
        /// Returns the declaring type.
        /// </summary>
        Type DeclaringType { get; }

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
        /// <param name="proxy">The proxy.</param>
        /// <returns>The object, of the specified interface type, to which the proxy has been adapted.</returns>
        object AdaptProxy(Type interfaceType, object proxy);

        /// <summary>
        /// Creates a new proxy.
        /// </summary>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new proxy.</returns>
        object CreateProxy(IInvocationHandler invocationHandler, params object[] arguments);
    }

    /// <summary>
    /// Defines a proxy template.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    public interface IProxyTemplate<out T> : IProxyTemplate where T : class
    {
        /// <summary>
        /// Creates a new proxy.
        /// </summary>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new proxy.</returns>
        new T CreateProxy(IInvocationHandler invocationHandler, params object[] arguments);
    }
}