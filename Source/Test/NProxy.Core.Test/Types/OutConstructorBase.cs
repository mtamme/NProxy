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

namespace NProxy.Core.Test.Types
{
    internal abstract class EnumArrayOutConstructorBase : IConstructor
    {
        protected EnumArrayOutConstructorBase(out EnumType[] value)
        {
            value = new EnumType[0];
        }
    }

    internal abstract class EnumOutConstructorBase : IConstructor
    {
        protected EnumOutConstructorBase(out EnumType value)
        {
            value = default(EnumType);
        }
    }

    internal abstract class GenericArrayOutConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericArrayOutConstructorBase(out TValue[] value)
        {
            value = new TValue[0];
        }
    }

    internal abstract class GenericJaggedArrayOutConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericJaggedArrayOutConstructorBase(out TValue[][] value)
        {
            value = new TValue[0][];
        }
    }

    internal abstract class GenericRankArrayOutConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericRankArrayOutConstructorBase(out TValue[,] value)
        {
            value = new TValue[0, 0];
        }
    }

    internal abstract class GenericListOutConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericListOutConstructorBase(out List<TValue> value)
        {
            value = new List<TValue>();
        }
    }

    internal abstract class GenericOutConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericOutConstructorBase(out TValue value)
        {
            value = default(TValue);
        }
    }

    internal abstract class IntArrayOutConstructorBase : IConstructor
    {
        protected IntArrayOutConstructorBase(out int[] value)
        {
            value = new int[0];
        }
    }

    internal abstract class IntOutConstructorBase : IConstructor
    {
        protected IntOutConstructorBase(out int value)
        {
            value = default(int);
        }
    }

    internal abstract class StringArrayOutConstructorBase : IConstructor
    {
        protected StringArrayOutConstructorBase(out string[] value)
        {
            value = new string[0];
        }
    }

    internal abstract class StringOutConstructorBase : IConstructor
    {
        protected StringOutConstructorBase(out string value)
        {
            value = String.Empty;
        }
    }

    internal abstract class StructArrayOutConstructorBase : IConstructor
    {
        protected StructArrayOutConstructorBase(out StructType[] value)
        {
            value = new StructType[0];
        }
    }

    internal abstract class StructOutConstructorBase : IConstructor
    {
        protected StructOutConstructorBase(out StructType value)
        {
            value = default(StructType);
        }
    }
}