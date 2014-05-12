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
    internal interface IEnumArrayOutParameter
    {
        void Method(out EnumType[] value);
    }

    internal interface IEnumOutParameter
    {
        void Method(out EnumType value);
    }

    internal interface IGenericArrayOutParameter
    {
        void Method<TValue>(out TValue[] value);
    }

    internal interface IGenericJaggedArrayOutParameter
    {
        void Method<TValue>(out TValue[][] value);
    }

    internal interface IGenericRankArrayOutParameter
    {
        void Method<TValue>(out TValue[,] value);
    }

    internal interface IGenericListOutParameter
    {
        void Method<TValue>(out List<TValue> value);
    }

    internal interface IGenericOutParameter
    {
        void Method<TValue>(out TValue value);
    }

    internal interface IIntArrayOutParameter
    {
        void Method(out int[] value);
    }

    internal interface IIntOutParameter
    {
        void Method(out int value);
    }

    internal interface IStringArrayOutParameter
    {
        void Method(out string[] value);
    }

    internal interface IStringOutParameter
    {
        void Method(out string value);
    }

    internal interface IStructArrayOutParameter
    {
        void Method(out StructType[] value);
    }

    internal interface IStructOutParameter
    {
        void Method(out StructType value);
    }
}