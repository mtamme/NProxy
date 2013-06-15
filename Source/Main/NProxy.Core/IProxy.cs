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
    /// Defines a proxy.
    /// </summary>
    public interface IProxy
    {
        /// <summary>
        /// Returns the declaring type.
        /// </summary>
        Type DeclaringType { get; }

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
        /// Casts an instance to the specified interface type.
        /// </summary>
        /// <typeparam name="TInterface">The interface type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>The object, of the specified interface type, to which the instance has been casted.</returns>
        TInterface Cast<TInterface>(object instance) where TInterface : class;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new instance.</returns>
        object CreateInstance(IInvocationHandler invocationHandler, params object[] arguments);
    }

    /// <summary>
    /// Defines a proxy.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    public interface IProxy<out T> : IProxy where T : class
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new instance.</returns>
        new T CreateInstance(IInvocationHandler invocationHandler, params object[] arguments);
    }
}