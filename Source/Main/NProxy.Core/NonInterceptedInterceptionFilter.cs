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
using System.Reflection;
using NProxy.Core.Internal.Builders;

namespace NProxy.Core
{
    /// <summary>
    /// Represents an non-intercepted interception filter.
    /// </summary>
    internal sealed class NonInterceptedInterceptionFilter : IInterceptionFilter
    {
        /// <summary>
        /// The name of the destructor method.
        /// </summary>
        private const string DestructorMethodName = "Finalize";

        #region IInterceptionFilter Members

        /// <inheritdoc/>
        public bool AcceptEvent(EventInfo eventInfo)
        {
            return !eventInfo.IsDefined(typeof (NonInterceptedAttribute), false);
        }

        /// <inheritdoc/>
        public bool AcceptProperty(PropertyInfo propertyInfo)
        {
            return !propertyInfo.IsDefined(typeof (NonInterceptedAttribute), false);
        }

        /// <inheritdoc/>
        public bool AcceptMethod(MethodInfo methodInfo)
        {
            if (methodInfo.IsDefined(typeof (NonInterceptedAttribute), false))
                return false;

            if (methodInfo.DeclaringType != typeof (object))
                return true;

            return !String.Equals(methodInfo.Name, DestructorMethodName);
        }

        #endregion
    }
}