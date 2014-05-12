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
    internal class EnumArrayConstructor : IConstructor
    {
        public EnumArrayConstructor(EnumType[] value)
        {
        }
    }

    internal class EnumConstructor : IConstructor
    {
        public EnumConstructor(EnumType value)
        {
        }
    }

    internal class GenericArrayConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericArrayConstructor(TValue[] value)
        {
        }
    }

    internal class GenericJaggedArrayConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericJaggedArrayConstructor(TValue[][] value)
        {
        }
    }

    internal class GenericRankArrayConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericRankArrayConstructor(TValue[,] value)
        {
        }
    }

    internal class GenericListConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericListConstructor(List<TValue> value)
        {
        }
    }

    internal class GenericConstructor<TValue> : IGenericConstructor<TValue>
    {
        public GenericConstructor(TValue value)
        {
        }
    }

    internal class IntArrayConstructor : IConstructor
    {
        public IntArrayConstructor(int[] value)
        {
        }
    }

    internal class IntConstructor : IConstructor
    {
        public IntConstructor(int value)
        {
        }
    }

    internal class StringArrayConstructor : IConstructor
    {
        public StringArrayConstructor(string[] value)
        {
        }
    }

    internal class StringConstructor : IConstructor
    {
        public StringConstructor(string value)
        {
        }
    }

    internal class StructArrayConstructor : IConstructor
    {
        public StructArrayConstructor(StructType[] value)
        {
        }
    }

    internal class StructConstructor : IConstructor
    {
        public StructConstructor(StructType value)
        {
        }
    }
}