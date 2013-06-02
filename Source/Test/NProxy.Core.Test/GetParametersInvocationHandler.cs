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

namespace NProxy.Core.Test
{
    internal sealed class GetParametersInvocationHandler : IInvocationHandler
    {
        public object[] Parameters { get; private set; }

        #region IInvocationHandler Members

        public object Invoke(object target, MethodInfo methodInfo, object[] parameters)
        {
            Parameters = new object[parameters.Length];

            Array.Copy(parameters, Parameters, Parameters.Length);

            return null;
        }

        #endregion
    }
}