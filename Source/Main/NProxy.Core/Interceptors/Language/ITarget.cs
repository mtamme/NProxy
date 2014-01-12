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

namespace NProxy.Core.Interceptors.Language
{
    /// <summary>
    /// Defines the <c>Target</c> verb.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    public interface ITarget<T> : IFluent where T : class
    {
        /// <summary>
        /// Specifies a singleton target and returns a new proxy.
        /// </summary>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <returns>The new proxy object.</returns>
        T Target<TTarget>() where TTarget : class, new();

        /// <summary>
        /// Specifies a singleton target and returns a new proxy.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <returns>The new proxy object.</returns>
        T Target(T target);

        /// <summary>
        /// Specifies a singleton target and returns a new proxy.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <returns>The new proxy object.</returns>
        T Target(object target);

        /// <summary>
        /// Specifies a target factory and returns a new proxy.
        /// </summary>
        /// <param name="targetFactory">The target factory.</param>
        /// <returns>The new proxy object.</returns>
        T Target(Func<object, object> targetFactory);

        /// <summary>
        /// Specifies the base class as the target and returns a new proxy.
        /// </summary>
        /// <returns>The new proxy object.</returns>
        T TargetBase();
    }
}