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
using System.Threading;

namespace NProxy.Core.Internal.Caching
{
    /// <summary>
    /// Represents a read write lock.
    /// </summary>
    internal sealed class ReadWriteLock : IDisposable
    {
        /// <summary>
        /// The reader writer lock.
        /// </summary>
        private readonly ReaderWriterLockSlim _lock;

        /// <summary>
        /// A value indicating whether this <see cref="ReadWriteLock"/> was already disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadWriteLock"/> class.
        /// </summary>
        public ReadWriteLock()
        {
            _lock = new ReaderWriterLockSlim();

            _disposed = false;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ReadWriteLock"/> is reclaimed by garbage collection.
        /// </summary>
        ~ReadWriteLock()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose this <see cref="ReadWriteLock"/>.
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

        /// <summary>
        /// Tries to enter the lock in upgradeable mode.
        /// </summary>
        /// <returns>A disposable to exit the lock from upgradeable mode.</returns>
        public IDisposable UpgradeableRead()
        {
            _lock.EnterUpgradeableReadLock();

            return new Disposable(_ => _lock.ExitUpgradeableReadLock());
        }

        /// <summary>
        /// Tries to enter the lock in read mode.
        /// </summary>
        /// <returns>A disposable to exit the lock from read mode.</returns>
        public IDisposable Read()
        {
            _lock.EnterReadLock();

            return new Disposable(_ => _lock.ExitReadLock());
        }

        /// <summary>
        /// Tries to enter the lock in write mode.
        /// </summary>
        /// <returns>A disposable to exit the lock from write mode.</returns>
        public IDisposable Write()
        {
            _lock.EnterWriteLock();

            return new Disposable(_ => _lock.ExitWriteLock());
        }

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