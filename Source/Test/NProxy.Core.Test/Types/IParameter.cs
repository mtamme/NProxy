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
    internal interface IEnumArrayParameter
    {
        void Method(EnumType[] value);
    }

    internal interface IEnumParameter
    {
        void Method(EnumType value);
    }

    internal interface IGenericArrayParameter
    {
        void Method<TValue>(TValue[] value);
    }

    internal interface IGenericJaggedArrayParameter
    {
        void Method<TValue>(TValue[][] value);
    }

    internal interface IGenericRankArrayParameter
    {
        void Method<TValue>(TValue[,] value);
    }

    internal interface IGenericListParameter
    {
        void Method<TValue>(List<TValue> value);
    }

    internal interface IGenericParameter
    {
        void Method<TValue>(TValue value);
    }

    internal interface IIntArrayParameter
    {
        void Method(int[] value);
    }

    internal interface IIntParameter
    {
        void Method(int value);
    }

    internal interface IStringArrayParameter
    {
        void Method(string[] value);
    }

    internal interface IStringParameter
    {
        void Method(string value);
    }

    internal interface IStructArrayParameter
    {
        void Method(StructType[] value);
    }

    internal interface IStructParameter
    {
        void Method(StructType value);
    }
}