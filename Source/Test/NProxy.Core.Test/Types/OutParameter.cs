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