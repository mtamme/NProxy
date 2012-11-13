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
using System.Collections.Generic;

namespace NProxy.Core.Interceptors.Language
{
    /// <summary>
    /// Defines the <c>Invokes</c> verb.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    public interface IInvokes<out T> : IFluent where T : class
    {
        /// <summary>
        /// Applies the interception behaviors.
        /// </summary>
        /// <returns>The proxy object.</returns>
        T ApplyInterceptionBehaviors();

        /// <summary>
        /// Specifies interceptors to invoke.
        /// </summary>
        /// <param name="interceptors">The interceptors.</param>
        /// <returns>The proxy object.</returns>
        T Invokes(IEnumerable<IInterceptor> interceptors);

        /// <summary>
        /// Specifies an interceptor to invoke.
        /// </summary>
        /// <param name="interceptor">The interceptor.</param>
        /// <returns>The proxy object.</returns>
        T Invokes(IInterceptor interceptor);

        /// <summary>
        /// Specifies an invocation handler to invoke.
        /// </summary>
        /// <param name="invocationHandler">The invocation handler.</param>
        /// <returns>The proxy object.</returns>
        T Invokes(IInvocationHandler invocationHandler);
    }
}
