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

namespace NProxy.Core.Internal.Caching
{
    /// <summary>
    /// Represents a concurrent cache.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    internal sealed class ConcurrentCache<TKey, TValue> : ICache<TKey, TValue>, IDisposable
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
        /// A value indicating whether this <see cref="ConcurrentCache{TKey, TValue}"/> was already disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentCache{TKey, TValue}"/> class.
        /// </summary>
        public ConcurrentCache()
        {
            _values = new Dictionary<TKey, TValue>();
            _lock = new ReadWriteLock();

            _disposed = false;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ConcurrentCache{TKey, TValue}"/> is reclaimed by garbage collection.
        /// </summary>
        ~ConcurrentCache()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose this <see cref="ConcurrentCache{TKey, TValue}"/>.
        /// </summary>
        /// <param name="disposing">A value indicating whether disposing is in progress.</param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _lock.Dispose();

            _disposed = true;
        }

        #region ICache<TKey, TValue> Members

        /// <inheritdoc/>
        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            TValue value;

            using (_lock.Read())
            {
                if (_values.TryGetValue(key, out value))
                    return value;
            }

            var newValue = valueFactory(key);

            using (_lock.Write())
            {
                if (_values.TryGetValue(key, out value))
                    return value;

                _values.Add(key, newValue);
            }

            return value;
        }

        #endregion

        #region IDisposable Members

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}