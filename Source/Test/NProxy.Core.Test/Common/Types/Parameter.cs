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
    internal class EnumArrayParameter : IEnumArrayParameter
    {
        #region IEnumArrayParameter Members

        public virtual void Method(EnumType[] value)
        {
        }

        #endregion
    }

    internal class EnumParameter : IEnumParameter
    {
        #region IEnumParameter Members

        public virtual void Method(EnumType value)
        {
        }

        #endregion
    }

    internal class GenericArrayParameter : IGenericArrayParameter
    {
        #region IGenericArrayParameter Members

        public virtual void Method<TValue>(TValue[] value)
        {
        }

        #endregion
    }

    internal class GenericJaggedArrayParameter : IGenericJaggedArrayParameter
    {
        #region IGenericJaggedArrayParameter Members

        public virtual void Method<TValue>(TValue[][] value)
        {
        }

        #endregion
    }

    internal class GenericRankArrayParameter : IGenericRankArrayParameter
    {
        #region IGenericRankArrayParameter Members

        public virtual void Method<TValue>(TValue[,] value)
        {
        }

        #endregion
    }

    internal class GenericListParameter : IGenericListParameter
    {
        #region IGenericListParameter Members

        public virtual void Method<TValue>(List<TValue> value)
        {
        }

        #endregion
    }

    internal class GenericParameter : IGenericParameter
    {
        #region IGenericParameter Members

        public virtual void Method<TValue>(TValue value)
        {
        }

        #endregion
    }

    internal class IntArrayParameter : IIntArrayParameter
    {
        #region IIntArrayParameter Members

        public virtual void Method(int[] value)
        {
        }

        #endregion
    }

    internal class IntParameter : IIntParameter
    {
        #region IIntParameter Members

        public virtual void Method(int value)
        {
        }

        #endregion
    }

    internal class StringArrayParameter : IStringArrayParameter
    {
        #region IStringArrayParameter Members

        public virtual void Method(string[] value)
        {
        }

        #endregion
    }

    internal class StringParameter : IStringParameter
    {
        #region IStringParameter Members

        public virtual void Method(string value)
        {
        }

        #endregion
    }

    internal class StructArrayParameter : IStructArrayParameter
    {
        #region IStructArrayParameter Members

        public virtual void Method(StructType[] value)
        {
        }

        #endregion
    }

    internal class StructParameter : IStructParameter
    {
        #region IStructParameter Members

        public virtual void Method(StructType value)
        {
        }

        #endregion
    }
}
