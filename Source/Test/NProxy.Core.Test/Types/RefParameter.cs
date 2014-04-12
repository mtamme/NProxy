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

using System;
using System.Collections.Generic;

namespace NProxy.Core.Test.Types
{
    internal class EnumArrayRefParameter : IEnumArrayRefParameter
    {
        #region IEnumArrayRefParameter Members

        public virtual void Method(ref EnumType[] value)
        {
            value = new EnumType[0];
        }

        #endregion
    }

    internal class EnumRefParameter : IEnumRefParameter
    {
        #region IEnumRefParameter Members

        public virtual void Method(ref EnumType value)
        {
            value = default(EnumType);
        }

        #endregion
    }

    internal class GenericArrayRefParameter : IGenericArrayRefParameter
    {
        #region IGenericArrayRefParameter Members

        public virtual void Method<TValue>(ref TValue[] value)
        {
            value = new TValue[0];
        }

        #endregion
    }

    internal class GenericJaggedArrayRefParameter : IGenericJaggedArrayRefParameter
    {
        #region IGenericJaggedArrayRefParameter Members

        public virtual void Method<TValue>(ref TValue[][] value)
        {
            value = new TValue[0][];
        }

        #endregion
    }

    internal class GenericRankArrayRefParameter : IGenericRankArrayRefParameter
    {
        #region IGenericRankArrayRefParameter Members

        public virtual void Method<TValue>(ref TValue[,] value)
        {
            value = new TValue[0, 0];
        }

        #endregion
    }

    internal class GenericListRefParameter : IGenericListRefParameter
    {
        #region IGenericListRefParameter Members

        public virtual void Method<TValue>(ref List<TValue> value)
        {
            value = new List<TValue>();
        }

        #endregion
    }

    internal class GenericRefParameter : IGenericRefParameter
    {
        #region IGenericRefParameter Members

        public virtual void Method<TValue>(ref TValue value)
        {
            value = default(TValue);
        }

        #endregion
    }

    internal class IntArrayRefParameter : IIntArrayRefParameter
    {
        #region IIntArrayRefParameter Members

        public virtual void Method(ref int[] value)
        {
            value = new int[0];
        }

        #endregion
    }

    internal class IntRefParameter : IIntRefParameter
    {
        #region IIntRefParameter Members

        public virtual void Method(ref int value)
        {
            value = default(int);
        }

        #endregion
    }

    internal class StringArrayRefParameter : IStringArrayRefParameter
    {
        #region IStringArrayRefParameter Members

        public virtual void Method(ref string[] value)
        {
            value = new string[0];
        }

        #endregion
    }

    internal class StringRefParameter : IStringRefParameter
    {
        #region IStringRefParameter Members

        public virtual void Method(ref string value)
        {
            value = String.Empty;
        }

        #endregion
    }

    internal class StructArrayRefParameter : IStructArrayRefParameter
    {
        #region IStructArrayRefParameter Members

        public virtual void Method(ref StructType[] value)
        {
            value = new StructType[0];
        }

        #endregion
    }

    internal class StructRefParameter : IStructRefParameter
    {
        #region IStructRefParameter Members

        public virtual void Method(ref StructType value)
        {
            value = default(StructType);
        }

        #endregion
    }
}