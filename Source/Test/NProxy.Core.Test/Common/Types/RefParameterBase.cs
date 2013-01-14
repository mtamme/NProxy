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
    internal abstract class EnumArrayRefParameterBase : IEnumArrayRefParameter
    {
        #region IEnumArrayRefParameter Members

        public abstract void Method(ref EnumType[] value);

        #endregion
    }

    internal abstract class EnumRefParameterBase : IEnumRefParameter
    {
        #region IEnumRefParameter Members

        public abstract void Method(ref EnumType value);

        #endregion
    }

    internal abstract class GenericArrayRefParameterBase : IGenericArrayRefParameter
    {
        #region IGenericArrayRefParameter Members

        public abstract void Method<TValue>(ref TValue[] value);

        #endregion
    }

    internal abstract class GenericJaggedArrayRefParameterBase : IGenericJaggedArrayRefParameter
    {
        #region IGenericJaggedArrayRefParameter Members

        public abstract void Method<TValue>(ref TValue[][] value);

        #endregion
    }

    internal abstract class GenericRankArrayRefParameterBase : IGenericRankArrayRefParameter
    {
        #region IGenericRankArrayRefParameter Members

        public abstract void Method<TValue>(ref TValue[,] value);

        #endregion
    }

    internal abstract class GenericListRefParameterBase : IGenericListRefParameter
    {
        #region IGenericListRefParameter Members

        public abstract void Method<TValue>(ref List<TValue> value);

        #endregion
    }

    internal abstract class GenericRefParameterBase : IGenericRefParameter
    {
        #region IGenericRefParameter Members

        public abstract void Method<TValue>(ref TValue value);

        #endregion
    }

    internal abstract class IntArrayRefParameterBase : IIntArrayRefParameter
    {
        #region IIntArrayRefParameter Members

        public abstract void Method(ref int[] value);

        #endregion
    }

    internal abstract class IntRefParameterBase : IIntRefParameter
    {
        #region IIntRefParameter Members

        public abstract void Method(ref int value);

        #endregion
    }

    internal abstract class StringArrayRefParameterBase : IStringArrayRefParameter
    {
        #region IStringArrayRefParameter Members

        public abstract void Method(ref string[] value);

        #endregion
    }

    internal abstract class StringRefParameterBase : IStringRefParameter
    {
        #region IStringRefParameter Members

        public abstract void Method(ref string value);

        #endregion
    }

    internal abstract class StructArrayRefParameterBase : IStructArrayRefParameter
    {
        #region IStructArrayRefParameter Members

        public abstract void Method(ref StructType[] value);

        #endregion
    }

    internal abstract class StructRefParameterBase : IStructRefParameter
    {
        #region IStructRefParameter Members

        public abstract void Method(ref StructType value);

        #endregion
    }
}