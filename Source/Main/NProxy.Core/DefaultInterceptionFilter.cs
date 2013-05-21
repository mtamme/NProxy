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
using NProxy.Core.Internal.Generators;
using NProxy.Core.Internal.Reflection;

namespace NProxy.Core
{
    /// <summary>
    /// Represents the default interception filter.
    /// </summary>
    internal sealed class DefaultInterceptionFilter : IInterceptionFilter
    {
        /// <summary>
        /// The name of the destructor method.
        /// </summary>
        private const string DestructorMethodName = "Finalize";

        #region IInterceptionFilter Members

        /// <inheritdoc/>
        public bool AcceptEvent(EventInfo eventInfo)
        {
            if (!eventInfo.CanOverride())
                return false;

            if (eventInfo.IsAbstract())
                return true;

            return !eventInfo.IsDefined<NonInterceptedAttribute>(false);
        }

        /// <inheritdoc/>
        public bool AcceptProperty(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanOverride())
                return false;

            if (propertyInfo.IsAbstract())
                return true;

            return !propertyInfo.IsDefined<NonInterceptedAttribute>(false);
        }

        /// <inheritdoc/>
        public bool AcceptMethod(MethodInfo methodInfo)
        {
            if (!methodInfo.CanOverride())
                return false;

            if (methodInfo.IsAbstract)
                return true;

            if (methodInfo.IsDefined<NonInterceptedAttribute>(false))
                return false;

            var declaringType = methodInfo.DeclaringType;

            if (declaringType == null)
                return false;

            if (declaringType != typeof (object))
                return true;

            return !String.Equals(methodInfo.Name, DestructorMethodName);
        }

        #endregion
    }
}