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
using System.Collections.Generic;

namespace NProxy.Core.Test.Common.Types
{
    internal class EnumArrayOutConstructor : IConstructor
    {
        public EnumArrayOutConstructor(out EnumType[] value)
        {
            value = new EnumType[0];
        }
    }

    internal class EnumOutConstructor : IConstructor
    {
        public EnumOutConstructor(out EnumType value)
        {
            value = default(EnumType);
        }
    }

    internal class GenericArrayOutConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericArrayOutConstructor(out TValue[] value)
        {
            value = new TValue[0];
        }
    }

    internal class GenericJaggedArrayOutConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericJaggedArrayOutConstructor(out TValue[][] value)
        {
            value = new TValue[0][];
        }
    }

    internal class GenericRankArrayOutConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericRankArrayOutConstructor(out TValue[,] value)
        {
            value = new TValue[0, 0];
        }
    }

    internal class GenericListOutConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericListOutConstructor(out List<TValue> value)
        {
            value = new List<TValue>();
        }
    }

    internal class GenericOutConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericOutConstructor(out TValue value)
        {
            value = default(TValue);
        }
    }

    internal class IntArrayOutConstructor : IConstructor
    {
        public IntArrayOutConstructor(out int[] value)
        {
            value = new int[0];
        }
    }

    internal class IntOutConstructor : IConstructor
    {
        public IntOutConstructor(out int value)
        {
            value = default(int);
        }
    }

    internal class StringArrayOutConstructor : IConstructor
    {
        public StringArrayOutConstructor(out string[] value)
        {
            value = new string[0];
        }
    }

    internal class StringOutConstructor : IConstructor
    {
        public StringOutConstructor(out string value)
        {
            value = String.Empty;
        }
    }

    internal class StructArrayOutConstructor : IConstructor
    {
        public StructArrayOutConstructor(out StructType[] value)
        {
            value = new StructType[0];
        }
    }

    internal class StructOutConstructor : IConstructor
    {
        public StructOutConstructor(out StructType value)
        {
            value = default(StructType);
        }
    }
}
