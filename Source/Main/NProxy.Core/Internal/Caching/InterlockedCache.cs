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
using System.Collections.Generic;
using NProxy.Core.Internal.Common;

namespace NProxy.Core.Internal.Caching
{
    /// <summary>
    /// Represents an interlocked cache.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    internal sealed class InterlockedCache<TKey, TValue> : ICache<TKey, TValue>
    {
        /// <summary>
        /// The values.
        /// </summary>
        private readonly Dictionary<TKey, TValue> _values;

        /// <summary>
        /// The lock.
        /// </summary>
        private readonly ReadWriteLock _lock;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterlockedCache{TKey, TValue}"/> class.
        /// </summary>
        public InterlockedCache()
        {
            _values = new Dictionary<TKey, TValue>();
            _lock = new ReadWriteLock();
        }

        #region ICache<TKey, TValue> Members

        /// <inheritdoc/>
        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            TValue value;

            using (_lock.UpgradeableRead())
            {
                if (_values.TryGetValue(key, out value))
                    return value;

                using (_lock.Write())
                {
                    if (_values.TryGetValue(key, out value))
                        return value;

                    value = valueFactory(key);
                    _values.Add(key, value);
                }
            }

            return value;
        }

        #endregion
    }
}