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

namespace NProxy.Core.Test.Types
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