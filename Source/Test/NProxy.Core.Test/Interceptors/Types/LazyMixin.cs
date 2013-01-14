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

namespace NProxy.Core.Test.Interceptors.Types
{
    internal class LazyMixin : ILazy
    {
        private bool _loaded;

        /// <summary>
        /// The Loaded property is "write-once" -
        /// after you have set it to true you can not set
        /// it to false again
        /// </summary>
        bool ILazy.Loaded
        {
            get { return _loaded; }
            set
            {
                if (_loaded)
                    return;

                _loaded = value;
            }
        }
    }
}