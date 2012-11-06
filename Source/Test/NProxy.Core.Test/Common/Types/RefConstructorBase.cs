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
    internal abstract class EnumArrayRefConstructorBase : IConstructor
    {
        protected EnumArrayRefConstructorBase(ref EnumType[] value)
        {
            value = new EnumType[0];
        }
    }

    internal abstract class EnumRefConstructorBase : IConstructor
    {
        protected EnumRefConstructorBase(ref EnumType value)
        {
            value = default(EnumType);
        }
    }

    internal abstract class GenericArrayRefConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericArrayRefConstructorBase(ref TValue[] value)
        {
            value = new TValue[0];
        }
    }

    internal abstract class GenericJaggedArrayRefConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericJaggedArrayRefConstructorBase(ref TValue[][] value)
        {
            value = new TValue[0][];
        }
    }

    internal abstract class GenericRankArrayRefConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericRankArrayRefConstructorBase(ref TValue[,] value)
        {
            value = new TValue[0, 0];
        }
    }

    internal abstract class GenericListRefConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericListRefConstructorBase(ref List<TValue> value)
        {
            value = new List<TValue>();
        }
    }

    internal abstract class GenericRefConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericRefConstructorBase(ref TValue value)
        {
            value = default(TValue);
        }
    }

    internal abstract class IntArrayRefConstructorBase : IConstructor
    {
        protected IntArrayRefConstructorBase(ref int[] value)
        {
            value = new int[0];
        }
    }

    internal abstract class IntRefConstructorBase : IConstructor
    {
        protected IntRefConstructorBase(ref int value)
        {
            value = default(int);
        }
    }

    internal abstract class StringArrayRefConstructorBase : IConstructor
    {
        protected StringArrayRefConstructorBase(ref string[] value)
        {
            value = new string[0];
        }
    }

    internal abstract class StringRefConstructorBase : IConstructor
    {
        protected StringRefConstructorBase(ref string value)
        {
            value = String.Empty;
        }
    }

    internal abstract class StructArrayRefConstructorBase : IConstructor
    {
        protected StructArrayRefConstructorBase(ref StructType[] value)
        {
            value = new StructType[0];
        }
    }

    internal abstract class StructRefConstructorBase : IConstructor
    {
        protected StructRefConstructorBase(ref StructType value)
        {
            value = default(StructType);
        }
    }
}
