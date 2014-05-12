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