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
using System.Collections.Generic;

namespace NProxy.Core.Test.Types
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