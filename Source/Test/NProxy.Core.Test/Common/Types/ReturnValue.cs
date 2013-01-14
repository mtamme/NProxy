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

namespace NProxy.Core.Test.Common.Types
{
    internal class EnumArrayReturnValue : IEnumArrayReturnValue
    {
        #region IEnumArrayReturnValue Members

        public virtual EnumType[] Method()
        {
            return new EnumType[0];
        }

        #endregion
    }

    internal class EnumReturnValue : IEnumReturnValue
    {
        #region IEnumReturnValue Members

        public virtual EnumType Method()
        {
            return default(EnumType);
        }

        #endregion
    }

    internal class GenericArrayReturnValue : IGenericArrayReturnValue
    {
        #region IGenericArrayReturnValue Members

        public virtual TReturn[] Method<TReturn>()
        {
            return new TReturn[0];
        }

        #endregion
    }

    internal class GenericJaggedArrayReturnValue : IGenericJaggedArrayReturnValue
    {
        #region IGenericJaggedArrayReturnValue Members

        public virtual TReturn[][] Method<TReturn>()
        {
            return new TReturn[0][];
        }

        #endregion
    }

    internal class GenericRankArrayReturnValue : IGenericRankArrayReturnValue
    {
        #region IGenericRankArrayReturnValue Members

        public virtual TReturn[,] Method<TReturn>()
        {
            return new TReturn[0,0];
        }

        #endregion
    }

    internal class GenericListReturnValue : IGenericListReturnValue
    {
        #region IGenericListReturnValue Members

        public virtual List<TReturn> Method<TReturn>()
        {
            return new List<TReturn>();
        }

        #endregion
    }

    internal class GenericReturnValue : IGenericReturnValue
    {
        #region IGenericReturnValue Members

        public virtual TReturn Method<TReturn>()
        {
            return default(TReturn);
        }

        #endregion
    }

    internal class IntArrayReturnValue : IIntArrayReturnValue
    {
        #region IIntArrayReturnValue Members

        public virtual int[] Method()
        {
            return new int[0];
        }

        #endregion
    }

    internal class IntReturnValue : IIntReturnValue
    {
        #region IIntReturnValue Members

        public virtual int Method()
        {
            return default(int);
        }

        #endregion
    }

    internal class StringArrayReturnValue : IStringArrayReturnValue
    {
        #region IStringArrayReturnValue Members

        public virtual string[] Method()
        {
            return new string[0];
        }

        #endregion
    }

    internal class StringReturnValue : IStringReturnValue
    {
        #region IStringReturnValue Members

        public virtual string Method()
        {
            return String.Empty;
        }

        #endregion
    }

    internal class StructArrayReturnValue : IStructArrayReturnValue
    {
        #region IStructArrayReturnValue Members

        public virtual StructType[] Method()
        {
            return new StructType[0];
        }

        #endregion
    }

    internal class StructReturnValue : IStructReturnValue
    {
        #region IStructReturnValue Members

        public virtual StructType Method()
        {
            return default(StructType);
        }

        #endregion
    }

    internal class VoidReturnValue : IVoidReturnValue
    {
        #region IVoidReturnValue Members

        public virtual void Method()
        {
        }

        #endregion
    }
}