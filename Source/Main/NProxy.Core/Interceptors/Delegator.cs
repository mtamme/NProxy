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
using System.Reflection;

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Represents a delegator.
    /// </summary>
    internal sealed class Delegator : IInvocationHandler
    {
        /// <summary>
        /// The invocation target.
        /// </summary>
        private readonly IInvocationTarget _invocationTarget;

        /// <summary>
        /// The invocation handler.
        /// </summary>
        private readonly IInvocationHandler _invocationHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="Delegator"/> class.
        /// </summary>
        /// <param name="invocationTarget">The invocation target.</param>
        /// <param name="invocationHandler">The invocation handler.</param>
        public Delegator(IInvocationTarget invocationTarget, IInvocationHandler invocationHandler)
        {
            if (invocationTarget == null)
                throw new ArgumentNullException("invocationTarget");

            if (invocationHandler == null)
                throw new ArgumentNullException("invocationHandler");

            _invocationTarget = invocationTarget;
            _invocationHandler = invocationHandler;
        }

        #region IInvocationHandler Members

        /// <inheritdoc/>
        public object Invoke(object proxy, MethodInfo methodInfo, object[] parameters)
        {
            var target = _invocationTarget.GetTarget(methodInfo);

            return _invocationHandler.Invoke(target ?? proxy, methodInfo, parameters);
        }

        #endregion
    }
}
