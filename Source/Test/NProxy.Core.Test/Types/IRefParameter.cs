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
    internal interface IEnumArrayRefParameter
    {
        void Method(ref EnumType[] value);
    }

    internal interface IEnumRefParameter
    {
        void Method(ref EnumType value);
    }

    internal interface IGenericArrayRefParameter
    {
        void Method<TValue>(ref TValue[] value);
    }

    internal interface IGenericJaggedArrayRefParameter
    {
        void Method<TValue>(ref TValue[][] value);
    }

    internal interface IGenericRankArrayRefParameter
    {
        void Method<TValue>(ref TValue[,] value);
    }

    internal interface IGenericListRefParameter
    {
        void Method<TValue>(ref List<TValue> value);
    }

    internal interface IGenericRefParameter
    {
        void Method<TValue>(ref TValue value);
    }

    internal interface IIntArrayRefParameter
    {
        void Method(ref int[] value);
    }

    internal interface IIntRefParameter
    {
        void Method(ref int value);
    }

    internal interface IStringArrayRefParameter
    {
        void Method(ref string[] value);
    }

    internal interface IStringRefParameter
    {
        void Method(ref string value);
    }

    internal interface IStructArrayRefParameter
    {
        void Method(ref StructType[] value);
    }

    internal interface IStructRefParameter
    {
        void Method(ref StructType value);
    }
}