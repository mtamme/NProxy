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