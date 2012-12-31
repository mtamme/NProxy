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

namespace NProxy.Core.Test.Common.Types
{
    internal class EnumArrayRefConstructor : IConstructor
    {
        public EnumArrayRefConstructor(ref EnumType[] value)
        {
            value = new EnumType[0];
        }
    }

    internal class EnumRefConstructor : IConstructor
    {
        public EnumRefConstructor(ref EnumType value)
        {
            value = default(EnumType);
        }
    }

    internal class GenericArrayRefConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericArrayRefConstructor(ref TValue[] value)
        {
            value = new TValue[0];
        }
    }

    internal class GenericJaggedArrayRefConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericJaggedArrayRefConstructor(ref TValue[][] value)
        {
            value = new TValue[0][];
        }
    }

    internal class GenericRankArrayRefConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericRankArrayRefConstructor(ref TValue[,] value)
        {
            value = new TValue[0, 0];
        }
    }

    internal class GenericListRefConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericListRefConstructor(ref List<TValue> value)
        {
            value = new List<TValue>();
        }
    }

    internal class GenericRefConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericRefConstructor(ref TValue value)
        {
            value = default(TValue);
        }
    }

    internal class IntArrayRefConstructor : IConstructor
    {
        public IntArrayRefConstructor(ref int[] value)
        {
            value = new int[0];
        }
    }

    internal class IntRefConstructor : IConstructor
    {
        public IntRefConstructor(ref int value)
        {
            value = default(int);
        }
    }

    internal class StringArrayRefConstructor : IConstructor
    {
        public StringArrayRefConstructor(ref string[] value)
        {
            value = new string[0];
        }
    }

    internal class StringRefConstructor : IConstructor
    {
        public StringRefConstructor(ref string value)
        {
            value = String.Empty;
        }
    }

    internal class StructArrayRefConstructor : IConstructor
    {
        public StructArrayRefConstructor(ref StructType[] value)
        {
            value = new StructType[0];
        }
    }

    internal class StructRefConstructor : IConstructor
    {
        public StructRefConstructor(ref StructType value)
        {
            value = default(StructType);
        }
    }
}
