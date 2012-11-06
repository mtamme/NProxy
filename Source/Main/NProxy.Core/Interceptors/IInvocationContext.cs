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
using System.Reflection;

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Defines an invocation context.
    /// </summary>
    public interface IInvocationContext
    {
        /// <summary>
        /// Returns the target object. 
        /// </summary>
        object Target { get; }

        /// <summary>
        /// Returns the method of the target class for which the interceptor was invoked.
        /// </summary>
        MethodInfo Method { get; }

        /// <summary>
        /// Returns the parameter values that will be passed to the method of the target class.
        /// </summary>
        object[] Parameters { get; }

        /// <summary>
        /// Returns a value indicating whether there is a next interceptor in the interceptor
        /// chain to proceed to.
        /// </summary>
        bool CanProceed { get; }

        /// <summary>
        /// Proceed to the next interceptor in the interceptor chain.
        /// </summary>
        /// <returns>The return value of the next method in the chain.</returns>
        object Proceed();
    }
}
