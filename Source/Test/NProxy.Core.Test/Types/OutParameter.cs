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
    internal class EnumArrayOutParameter : IEnumArrayOutParameter
    {
        #region IEnumArrayOutParameter Members

        public virtual void Method(out EnumType[] value)
        {
            value = new EnumType[0];
        }

        #endregion
    }

    internal class EnumOutParameter : IEnumOutParameter
    {
        #region IEnumOutParameter Members

        public virtual void Method(out EnumType value)
        {
            value = default(EnumType);
        }

        #endregion
    }

    internal class GenericArrayOutParameter : IGenericArrayOutParameter
    {
        #region IGenericArrayOutParameter Members

        public virtual void Method<TValue>(out TValue[] value)
        {
            value = new TValue[0];
        }

        #endregion
    }

    internal class GenericJaggedArrayOutParameter : IGenericJaggedArrayOutParameter
    {
        #region IGenericJaggedArrayOutParameter Members

        public virtual void Method<TValue>(out TValue[][] value)
        {
            value = new TValue[0][];
        }

        #endregion
    }

    internal class GenericRankArrayOutParameter : IGenericRankArrayOutParameter
    {
        #region IGenericRankArrayOutParameter Members

        public virtual void Method<TValue>(out TValue[,] value)
        {
            value = new TValue[0, 0];
        }

        #endregion
    }

    internal class GenericListOutParameter : IGenericListOutParameter
    {
        #region IGenericListOutParameter Members

        public virtual void Method<TValue>(out List<TValue> value)
        {
            value = new List<TValue>();
        }

        #endregion
    }

    internal class GenericOutParameter : IGenericOutParameter
    {
        #region IGenericOutParameter Members

        public virtual void Method<TValue>(out TValue value)
        {
            value = default(TValue);
        }

        #endregion
    }

    internal class IntArrayOutParameter : IIntArrayOutParameter
    {
        #region IIntArrayOutParameter Members

        public virtual void Method(out int[] value)
        {
            value = new int[0];
        }

        #endregion
    }

    internal class IntOutParameter : IIntOutParameter
    {
        #region IIntOutParameter Members

        public virtual void Method(out int value)
        {
            value = default(int);
        }

        #endregion
    }

    internal class StringArrayOutParameter : IStringArrayOutParameter
    {
        #region IStringArrayOutParameter Members

        public virtual void Method(out string[] value)
        {
            value = new string[0];
        }

        #endregion
    }

    internal class StringOutParameter : IStringOutParameter
    {
        #region IStringOutParameter Members

        public virtual void Method(out string value)
        {
            value = String.Empty;
        }

        #endregion
    }

    internal class StructArrayOutParameter : IStructArrayOutParameter
    {
        #region IStructArrayOutParameter Members

        public virtual void Method(out StructType[] value)
        {
            value = new StructType[0];
        }

        #endregion
    }

    internal class StructOutParameter : IStructOutParameter
    {
        #region IStructOutParameter Members

        public virtual void Method(out StructType value)
        {
            value = default(StructType);
        }

        #endregion
    }
}