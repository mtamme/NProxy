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

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Represents an invocation target interceptor.
    /// </summary>
    internal sealed class InvocationTargetInterceptor : IInterceptor
    {
        /// <summary>
        /// The invocation target.
        /// </summary>
        private readonly IInvocationTarget _invocationTarget;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationTargetInterceptor"/> class.
        /// </summary>
        /// <param name="invocationTarget">The invocation target.</param>
        public InvocationTargetInterceptor(IInvocationTarget invocationTarget)
        {
            if (invocationTarget == null)
                throw new ArgumentNullException("invocationTarget");

            _invocationTarget = invocationTarget;
        }

        #region IInterceptor Members

        /// <inheritdoc/>
        public object Intercept(IInvocationContext invocationContext)
        {
            var methodInfo = invocationContext.Method;
            var target = _invocationTarget.GetTarget(methodInfo);

            return methodInfo.Invoke(target, invocationContext.Parameters);
        }

        #endregion
    }
}
