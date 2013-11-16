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
        /// A value indicating weather this <see cref="ReadWriteLock"/> was already disposed.
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
        /// Dispose this <see cref="AnonymousDisposable"/>.
        /// </summary>
        /// <param name="disposing">A value indicating weather disposing is in progress.</param>
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

            return new AnonymousDisposable(_ => _lock.ExitUpgradeableReadLock());
        }

        /// <summary>
        /// Tries to enter the lock in read mode.
        /// </summary>
        /// <returns>A disposable to exit the lock from read mode.</returns>
        public IDisposable Read()
        {
            _lock.EnterReadLock();

            return new AnonymousDisposable(_ => _lock.ExitReadLock());
        }

        /// <summary>
        /// Tries to enter the lock in write mode.
        /// </summary>
        /// <returns>A disposable to exit the lock from write mode.</returns>
        public IDisposable Write()
        {
            _lock.EnterWriteLock();

            return new AnonymousDisposable(_ => _lock.ExitWriteLock());
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