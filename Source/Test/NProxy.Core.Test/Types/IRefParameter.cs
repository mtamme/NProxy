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