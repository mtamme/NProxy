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
    internal abstract class EnumArrayReturnValueBase : IEnumArrayReturnValue
    {
        #region IEnumArrayReturnValue Members

        public abstract EnumType[] Method();

        #endregion
    }

    internal abstract class EnumReturnValueBase : IEnumReturnValue
    {
        #region IEnumReturnValue Members

        public abstract EnumType Method();

        #endregion
    }

    internal abstract class GenericArrayReturnValueBase : IGenericArrayReturnValue
    {
        #region IGenericArrayReturnValue Members

        public abstract TReturn[] Method<TReturn>();

        #endregion
    }

    internal abstract class GenericJaggedArrayReturnValueBase : IGenericJaggedArrayReturnValue
    {
        #region IGenericJaggedArrayReturnValue Members

        public abstract TReturn[][] Method<TReturn>();

        #endregion
    }

    internal abstract class GenericRankArrayReturnValueBase : IGenericRankArrayReturnValue
    {
        #region IGenericRankArrayReturnValue Members

        public abstract TReturn[,] Method<TReturn>();

        #endregion
    }

    internal abstract class GenericListReturnValueBase : IGenericListReturnValue
    {
        #region IGenericListReturnValue Members

        public abstract List<TReturn> Method<TReturn>();

        #endregion
    }

    internal abstract class GenericReturnValueBase : IGenericReturnValue
    {
        #region IGenericReturnValue Members

        public abstract TReturn Method<TReturn>();

        #endregion
    }

    internal abstract class IntArrayReturnValueBase : IIntArrayReturnValue
    {
        #region IIntArrayReturnValue Members

        public abstract int[] Method();

        #endregion
    }

    internal abstract class IntReturnValueBase : IIntReturnValue
    {
        #region IIntReturnValue Members

        public abstract int Method();

        #endregion
    }

    internal abstract class StringArrayReturnValueBase : IStringArrayReturnValue
    {
        #region IStringArrayReturnValue Members

        public abstract string[] Method();

        #endregion
    }

    internal abstract class StringReturnValueBase : IStringReturnValue
    {
        #region IStringReturnValue Members

        public abstract string Method();

        #endregion
    }

    internal abstract class StructArrayReturnValueBase : IStructArrayReturnValue
    {
        #region IStructArrayReturnValue Members

        public abstract StructType[] Method();

        #endregion
    }

    internal abstract class StructReturnValueBase : IStructReturnValue
    {
        #region IStructReturnValue Members

        public abstract StructType Method();

        #endregion
    }

    internal abstract class VoidReturnValueBase : IVoidReturnValue
    {
        #region IVoidReturnValue Members

        public abstract void Method();

        #endregion
    }
}
