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
