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

namespace NProxy.Core.Internal.Caching
{
    /// <summary>
    /// Represents an anonymous disposable.
    /// </summary>
    internal sealed class AnonymousDisposable : IDisposable
    {
        /// <summary>
        /// The dispose action.
        /// </summary>
        private readonly Action<bool> _dispose;

        /// <summary>
        /// A value indicating whether this <see cref="AnonymousDisposable"/> was already disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousDisposable"/> class.
        /// </summary>
        /// <param name="dispose">The dispose action.</param>
        public AnonymousDisposable(Action<bool> dispose)
        {
            if (dispose == null)
                throw new ArgumentNullException("dispose");

            _dispose = dispose;

            _disposed = false;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="AnonymousDisposable"/> is reclaimed by garbage collection.
        /// </summary>
        ~AnonymousDisposable()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose this <see cref="AnonymousDisposable"/>.
        /// </summary>
        /// <param name="disposing">A value indicating whether disposing is in progress.</param>
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _dispose(disposing);

            _disposed = true;
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