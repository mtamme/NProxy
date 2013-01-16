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
    internal abstract class EnumArrayParameterBase : IEnumArrayParameter
    {
        #region IEnumArrayParameter Members

        public abstract void Method(EnumType[] value);

        #endregion
    }

    internal abstract class EnumParameterBase : IEnumParameter
    {
        #region IEnumParameter Members

        public abstract void Method(EnumType value);

        #endregion
    }

    internal abstract class GenericArrayParameterBase : IGenericArrayParameter
    {
        #region IGenericArrayParameter Members

        public abstract void Method<TValue>(TValue[] value);

        #endregion
    }

    internal abstract class GenericJaggedArrayParameterBase : IGenericJaggedArrayParameter
    {
        #region IGenericJaggedArrayParameter Members

        public abstract void Method<TValue>(TValue[][] value);

        #endregion
    }

    internal abstract class GenericRankArrayParameterBase : IGenericRankArrayParameter
    {
        #region IGenericRankArrayParameter Members

        public abstract void Method<TValue>(TValue[,] value);

        #endregion
    }

    internal abstract class GenericListParameterBase : IGenericListParameter
    {
        #region IGenericListParameter Members

        public abstract void Method<TValue>(List<TValue> value);

        #endregion
    }

    internal abstract class GenericParameterBase : IGenericParameter
    {
        #region IGenericParameter Members

        public abstract void Method<TValue>(TValue value);

        #endregion
    }

    internal abstract class IntArrayParameterBase : IIntArrayParameter
    {
        #region IIntArrayParameter Members

        public abstract void Method(int[] value);

        #endregion
    }

    internal abstract class IntParameterBase : IIntParameter
    {
        #region IIntParameter Members

        public abstract void Method(int value);

        #endregion
    }

    internal abstract class StringArrayParameterBase : IStringArrayParameter
    {
        #region IStringArrayParameter Members

        public abstract void Method(string[] value);

        #endregion
    }

    internal abstract class StringParameterBase : IStringParameter
    {
        #region IStringParameter Members

        public abstract void Method(string value);

        #endregion
    }

    internal abstract class StructArrayParameterBase : IStructArrayParameter
    {
        #region IStructArrayParameter Members

        public abstract void Method(StructType[] value);

        #endregion
    }

    internal abstract class StructParameterBase : IStructParameter
    {
        #region IStructParameter Members

        public abstract void Method(StructType value);

        #endregion
    }
}