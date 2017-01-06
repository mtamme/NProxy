//
// Copyright © Martin Tamme
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;

namespace NProxy.Core.Internal.Caching
{    
    /// <summary>
    /// Represents a lock-on-write cache.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    internal sealed class LockOnWriteCache<TKey, TValue> : IDisposableCache<TKey, TValue>
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
        /// A value indicating whether this <see cref="LockOnWriteCache{TKey,TValue}"/> was already disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="LockOnWriteCache{TKey,TValue}"/> class.
        /// </summary>
        public LockOnWriteCache()
        {
            _values = new Dictionary<TKey, TValue>();
            _lock = new ReadWriteLock();

            _disposed = false;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="LockOnWriteCache{TKey,TValue}"/> is reclaimed by garbage collection.
        /// </summary>
        ~LockOnWriteCache()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose this <see cref="LockOnWriteCache{TKey,TValue}"/>.
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