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