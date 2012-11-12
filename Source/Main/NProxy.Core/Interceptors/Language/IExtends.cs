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
namespace NProxy.Core.Interceptors.Language
{
    /// <summary>
    /// Defines the <c>Extends</c> verb.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    public interface IExtends<out T> : IImplements<T> where T : class
    {
        /// <summary>
        /// Specifies a mixin to extend.
        /// </summary>
        /// <typeparam name="TMixin">The mixin type.</typeparam>
        /// <returns>The setup.</returns>
        IExtends<T> Extends<TMixin>() where TMixin : class, new();

        /// <summary>
        /// Specifies a mixin to extend.
        /// </summary>
        /// <typeparam name="TMixin">The mixin type.</typeparam>
        /// <param name="mixin">The mixin object.</param>
        /// <returns>The setup.</returns>
        IExtends<T> Extends<TMixin>(TMixin mixin) where TMixin : class;
    }
}
