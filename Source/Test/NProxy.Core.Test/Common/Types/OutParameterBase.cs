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
    internal abstract class EnumArrayOutParameterBase : IEnumArrayOutParameter
    {
        #region IEnumArrayOutParameter Members

        public abstract void Method(out EnumType[] value);

        #endregion
    }

    internal abstract class EnumOutParameterBase : IEnumOutParameter
    {
        #region IEnumOutParameter Members

        public abstract void Method(out EnumType value);

        #endregion
    }

    internal abstract class GenericArrayOutParameterBase : IGenericArrayOutParameter
    {
        #region IGenericArrayOutParameter Members

        public abstract void Method<TValue>(out TValue[] value);

        #endregion
    }

    internal abstract class GenericJaggedArrayOutParameterBase : IGenericJaggedArrayOutParameter
    {
        #region IGenericJaggedArrayOutParameter Members

        public abstract void Method<TValue>(out TValue[][] value);

        #endregion
    }

    internal abstract class GenericRankArrayOutParameterBase : IGenericRankArrayOutParameter
    {
        #region IGenericRankArrayOutParameter Members

        public abstract void Method<TValue>(out TValue[,] value);

        #endregion
    }

    internal abstract class GenericListOutParameterBase : IGenericListOutParameter
    {
        #region IGenericListOutParameter Members

        public abstract void Method<TValue>(out List<TValue> value);

        #endregion
    }

    internal abstract class GenericOutParameterBase : IGenericOutParameter
    {
        #region IGenericOutParameter Members

        public abstract void Method<TValue>(out TValue value);

        #endregion
    }

    internal abstract class IntArrayOutParameterBase : IIntArrayOutParameter
    {
        #region IIntArrayOutParameter Members

        public abstract void Method(out int[] value);

        #endregion
    }

    internal abstract class IntOutParameterBase : IIntOutParameter
    {
        #region IIntOutParameter Members

        public abstract void Method(out int value);

        #endregion
    }

    internal abstract class StringArrayOutParameterBase : IStringArrayOutParameter
    {
        #region IStringArrayOutParameter Members

        public abstract void Method(out string[] value);

        #endregion
    }

    internal abstract class StringOutParameterBase : IStringOutParameter
    {
        #region IStringOutParameter Members

        public abstract void Method(out string value);

        #endregion
    }

    internal abstract class StructArrayOutParameterBase : IStructArrayOutParameter
    {
        #region IStructArrayOutParameter Members

        public abstract void Method(out StructType[] value);

        #endregion
    }

    internal abstract class StructOutParameterBase : IStructOutParameter
    {
        #region IStructOutParameter Members

        public abstract void Method(out StructType value);

        #endregion
    }
}
