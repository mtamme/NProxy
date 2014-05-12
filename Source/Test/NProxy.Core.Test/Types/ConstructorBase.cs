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

using System.Collections.Generic;

namespace NProxy.Core.Test.Types
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