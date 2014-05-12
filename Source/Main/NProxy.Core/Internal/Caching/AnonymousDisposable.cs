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