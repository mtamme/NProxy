//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © 2012 Martin Tamme
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
