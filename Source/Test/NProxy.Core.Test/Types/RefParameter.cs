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