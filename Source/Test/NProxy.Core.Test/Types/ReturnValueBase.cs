//
// Copyright © Martin Tamme
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System.Collections.Generic;
using System.Threading.Tasks;

namespace NProxy.Core.Test.Types
{
    internal abstract class AsyncReturnValueBase : IAsyncReturnValue
    {
        #region IAsyncReturnValue Members

        public abstract Task Method();

        #endregion
    }

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