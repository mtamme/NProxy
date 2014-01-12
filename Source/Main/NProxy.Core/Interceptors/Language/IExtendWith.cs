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

namespace NProxy.Core.Interceptors.Language
{
    /// <summary>
    /// Defines the <c>ExtendWith</c> verb.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    /// <typeparam name="TInterceptor">The interceptor type.</typeparam>
    public interface IExtendWith<T, in TInterceptor> : IImplement<T, TInterceptor> where T : class
    {
        /// <summary>
        /// Specifies a mixin.
        /// </summary>
        /// <typeparam name="TMixin">The mixin type.</typeparam>
        /// <returns>The <c>ExtendWith</c> verb.</returns>
        IExtendWith<T, TInterceptor> ExtendWith<TMixin>() where TMixin : class, new();

        /// <summary>
        /// Specifies mixins.
        /// </summary>
        /// <param name="mixins">The mixins.</param>
        /// <returns>The <c>ExtendWith</c> verb.</returns>
        IExtendWith<T, TInterceptor> ExtendWith(params object[] mixins);
    }
}