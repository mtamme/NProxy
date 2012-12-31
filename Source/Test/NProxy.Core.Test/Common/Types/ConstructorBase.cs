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

namespace NProxy.Core.Test.Common.Types
{
    internal abstract class EnumArrayConstructorBase : IConstructor
    {
        protected EnumArrayConstructorBase(EnumType[] value)
        {
        }
    }

    internal abstract class EnumConstructorBase : IConstructor
    {
        protected EnumConstructorBase(EnumType value)
        {
        }
    }

    internal abstract class GenericArrayConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericArrayConstructorBase(TValue[] value)
        {
        }
    }

    internal abstract class GenericJaggedArrayConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericJaggedArrayConstructorBase(TValue[][] value)
        {
        }
    }

    internal abstract class GenericRankArrayConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericRankArrayConstructorBase(TValue[,] value)
        {
        }
    }

    internal abstract class GenericListConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericListConstructorBase(List<TValue> value)
        {
        }
    }

    internal abstract class GenericConstructorBase<TValue> : IGenericConstructor<TValue>
    {
        protected GenericConstructorBase(TValue value)
        {
        }
    }

    internal abstract class IntArrayConstructorBase : IConstructor
    {
        protected IntArrayConstructorBase(int[] value)
        {
        }
    }

    internal abstract class IntConstructorBase : IConstructor
    {
        protected IntConstructorBase(int value)
        {
        }
    }

    internal abstract class StringArrayConstructorBase : IConstructor
    {
        protected StringArrayConstructorBase(string[] value)
        {
        }
    }

    internal abstract class StringConstructorBase : IConstructor
    {
        protected StringConstructorBase(string value)
        {
        }
    }

    internal abstract class StructArrayConstructorBase : IConstructor
    {
        protected StructArrayConstructorBase(StructType[] value)
        {
        }
    }

    internal abstract class StructConstructorBase : IConstructor
    {
        protected StructConstructorBase(StructType value)
        {
        }
    }
}
