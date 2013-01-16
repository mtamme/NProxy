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

using System.Collections.Generic;

namespace NProxy.Core.Test.Types
{
    internal class EnumArrayConstructor : IConstructor
    {
        public EnumArrayConstructor(EnumType[] value)
        {
        }
    }

    internal class EnumConstructor : IConstructor
    {
        public EnumConstructor(EnumType value)
        {
        }
    }

    internal class GenericArrayConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericArrayConstructor(TValue[] value)
        {
        }
    }

    internal class GenericJaggedArrayConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericJaggedArrayConstructor(TValue[][] value)
        {
        }
    }

    internal class GenericRankArrayConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericRankArrayConstructor(TValue[,] value)
        {
        }
    }

    internal class GenericListConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericListConstructor(List<TValue> value)
        {
        }
    }

    internal class GenericConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericConstructor(TValue value)
        {
        }
    }

    internal class IntArrayConstructor : IConstructor
    {
        public IntArrayConstructor(int[] value)
        {
        }
    }

    internal class IntConstructor : IConstructor
    {
        public IntConstructor(int value)
        {
        }
    }

    internal class StringArrayConstructor : IConstructor
    {
        public StringArrayConstructor(string[] value)
        {
        }
    }

    internal class StringConstructor : IConstructor
    {
        public StringConstructor(string value)
        {
        }
    }

    internal class StructArrayConstructor : IConstructor
    {
        public StructArrayConstructor(StructType[] value)
        {
        }
    }

    internal class StructConstructor : IConstructor
    {
        public StructConstructor(StructType value)
        {
        }
    }
}