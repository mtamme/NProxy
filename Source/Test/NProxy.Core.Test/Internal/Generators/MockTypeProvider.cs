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
using System;
using NProxy.Core.Internal.Generators;

namespace NProxy.Core.Test.Internal.Generators
{
    internal sealed class MockTypeProvider<TDefintion> : ITypeProvider<TDefintion>
    {
        private readonly Func<TDefintion, Type> _typeFactory;

        public MockTypeProvider(Func<TDefintion, Type> typeFactory)
        {
            if (typeFactory == null)
                throw new ArgumentNullException("typeFactory");

            _typeFactory = typeFactory;

            InvocationCount = 0;
        }

        public int InvocationCount { get; private set; }

        #region ITypeProvider Members

        public Type GetType(TDefintion definition)
        {
            InvocationCount++;

            return _typeFactory(definition);
        }

        #endregion
    }
}
