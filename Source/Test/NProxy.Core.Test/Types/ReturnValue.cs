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

using System;
using System.Collections.Generic;

namespace NProxy.Core.Test.Types
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
            return new TReturn[0, 0];
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