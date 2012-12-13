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
    /// Represents a single invocation target.
    /// </summary>
    internal sealed class SingleInvocationTarget : IInvocationTarget
    {
        /// <summary>
        /// The target object.
        /// </summary>
        private readonly object _target;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleInvocationTarget"/> class.
        /// </summary>
        /// <param name="target"></param>
        public SingleInvocationTarget(object target)
        {
            _target = target;
        }

        #region IInvocationTarget Members

        /// <inheritdoc/>
        public object GetTarget(MethodInfo methodInfo)
        {
            return _target;
        }

        #endregion
    }
}
