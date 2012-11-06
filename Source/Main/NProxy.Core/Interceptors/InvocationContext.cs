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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NProxy.Core.Interceptors
{
    /// <summary>
    /// Represents an invocation context.
    /// </summary>
    internal sealed class InvocationContext : IInvocationContext
    {
        /// <summary>
        /// The target object.
        /// </summary>
        private readonly object _target;

        /// <summary>
        /// The method information.
        /// </summary>
        private readonly MethodInfo _methodInfo;

        /// <summary>
        /// The parameter values.
        /// </summary>
        private readonly object[] _parameters;

        /// <summary>
        /// The next interceptor.
        /// </summary>
        private readonly Stack<IInterceptor> _interceptors;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvocationContext"/> class.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="parameters">The parameter values.</param>
        /// <param name="interceptors">The interceptors.</param>
        public InvocationContext(object target, MethodInfo methodInfo, object[] parameters, IEnumerable<IInterceptor> interceptors)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            if (parameters == null)
                throw new ArgumentNullException("parameters");

            if (interceptors == null)
                throw new ArgumentNullException("interceptors");

            _target = target;
            _methodInfo = methodInfo;
            _parameters = parameters;

            _interceptors = new Stack<IInterceptor>();

            // Add target interceptor.
            if (target != null)
                _interceptors.Push(new TargetInterceptor());

            // Add interceptors.
            foreach (var interceptor in interceptors.Reverse())
                _interceptors.Push(interceptor);
        }

        #region IInvocationContext Members

        /// <inheritdoc/>
        public object Target
        {
            get { return _target; }
        }

        /// <inheritdoc/>
        public MethodInfo Method
        {
            get { return _methodInfo; }
        }

        /// <inheritdoc/>
        public object[] Parameters
        {
            get { return _parameters; }
        }

        /// <inheritdoc/>
        public bool CanProceed
        {
            get { return _interceptors.Count > 0; }
        }

        /// <inheritdoc/>
        public object Proceed()
        {
            if (!CanProceed)
                throw new InvalidOperationException("No more interceptors in the interceptor chain");

            var interceptor = _interceptors.Pop();

            return interceptor.Intercept(this);
        }

        #endregion
    }
}
