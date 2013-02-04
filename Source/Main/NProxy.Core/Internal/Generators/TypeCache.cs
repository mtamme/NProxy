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

namespace NProxy.Core.Internal.Generators
{
    /// <summary>
    /// Represents a type cache.
    /// </summary>
    /// <typeparam name="TDefinition">The definition type.</typeparam>
    /// <typeparam name="TKey">The key type.</typeparam>
    internal sealed class TypeCache<TDefinition, TKey> : ITypeProvider<TDefinition> where TDefinition : class
    {
        /// <summary>
        /// The key factory.
        /// </summary>
        private readonly Func<TDefinition, TKey> _keyFactory;

        /// <summary>
        /// The underlying type provider.
        /// </summary>
        private readonly ITypeProvider<TDefinition> _typeProvider;

        /// <summary>
        /// The known types.
        /// </summary>
        private readonly Dictionary<TKey, Type> _knownTypes;

        /// <summary>
        /// The known types lock.
        /// </summary>
        private readonly ReadWriteLock _knownTypesLock;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeCache{TDefinition, TKey}"/> class.
        /// </summary>
        /// <param name="keyFactory">The key factory.</param>
        /// <param name="typeProvider">The underlying type provider.</param>
        public TypeCache(Func<TDefinition, TKey> keyFactory, ITypeProvider<TDefinition> typeProvider)
        {
            if (keyFactory == null)
                throw new ArgumentNullException("keyFactory");

            if (typeProvider == null)
                throw new ArgumentNullException("typeProvider");

            _keyFactory = keyFactory;
            _typeProvider = typeProvider;

            _knownTypes = new Dictionary<TKey, Type>();
            _knownTypesLock = new ReadWriteLock();
        }

        #region ITypeProvider<TDefintion> Members

        /// <inheritdoc/>
        public Type GetType(TDefinition definition)
        {
            if (definition == null)
                throw new ArgumentNullException("definition");

            var key = _keyFactory(definition);
            Type type;

            using (_knownTypesLock.UpgradeableRead())
            {
                if (_knownTypes.TryGetValue(key, out type))
                    return type;

                using (_knownTypesLock.Write())
                {
                    if (_knownTypes.TryGetValue(key, out type))
                        return type;

                    type = _typeProvider.GetType(definition);
                    _knownTypes.Add(key, type);
                }
            }

            return type;
        }

        #endregion
    }
}