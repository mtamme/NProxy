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
using NProxy.Core.Interceptors;

namespace NProxy.Core.Test.Interceptors.Types
{
    internal sealed class LazyInterceptor : IInterceptor
    {
        public static readonly IInterceptor Instance = new LazyInterceptor();

        #region IInterceptor Members

        public object Intercept(IInvocationContext invocationContext)
        {
            var lazy = invocationContext.Target as ILazy;

            if (lazy != null)
            {
                if (!lazy.Loaded)
                {
                    lazy.Loaded = true;

                    // Perform lazy loading...
                }
            }

            return invocationContext.Proceed();
        }

        #endregion
    }
}
