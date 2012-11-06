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
    internal interface IEnumArrayReturnValue
    {
        EnumType[] Method();
    }

    internal interface IEnumReturnValue
    {
        EnumType Method();
    }

    internal interface IGenericArrayReturnValue
    {
        TReturn[] Method<TReturn>();
    }

    internal interface IGenericJaggedArrayReturnValue
    {
        TReturn[][] Method<TReturn>();
    }

    internal interface IGenericRankArrayReturnValue
    {
        TReturn[,] Method<TReturn>();
    }

    internal interface IGenericListReturnValue
    {
        List<TReturn> Method<TReturn>();
    }

    internal interface IGenericReturnValue
    {
        TReturn Method<TReturn>();
    }

    internal interface IIntArrayReturnValue
    {
        int[] Method();
    }

    internal interface IIntReturnValue
    {
        int Method();
    }

    internal interface IStringArrayReturnValue
    {
        string[] Method();
    }

    internal interface IStringReturnValue
    {
        string Method();
    }

    internal interface IStructArrayReturnValue
    {
        StructType[] Method();
    }

    internal interface IStructReturnValue
    {
        StructType Method();
    }

    internal interface IVoidReturnValue
    {
        void Method();
    }
}
