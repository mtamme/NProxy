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
    internal abstract class EnumArrayRefParameterBase : IEnumArrayRefParameter
    {
        #region IEnumArrayRefParameter Members

        public abstract void Method(ref EnumType[] value);

        #endregion
    }

    internal abstract class EnumRefParameterBase : IEnumRefParameter
    {
        #region IEnumRefParameter Members

        public abstract void Method(ref EnumType value);

        #endregion
    }

    internal abstract class GenericArrayRefParameterBase : IGenericArrayRefParameter
    {
        #region IGenericArrayRefParameter Members

        public abstract void Method<TValue>(ref TValue[] value);

        #endregion
    }

    internal abstract class GenericJaggedArrayRefParameterBase : IGenericJaggedArrayRefParameter
    {
        #region IGenericJaggedArrayRefParameter Members

        public abstract void Method<TValue>(ref TValue[][] value);

        #endregion
    }

    internal abstract class GenericRankArrayRefParameterBase : IGenericRankArrayRefParameter
    {
        #region IGenericRankArrayRefParameter Members

        public abstract void Method<TValue>(ref TValue[,] value);

        #endregion
    }

    internal abstract class GenericListRefParameterBase : IGenericListRefParameter
    {
        #region IGenericListRefParameter Members

        public abstract void Method<TValue>(ref List<TValue> value);

        #endregion
    }

    internal abstract class GenericRefParameterBase : IGenericRefParameter
    {
        #region IGenericRefParameter Members

        public abstract void Method<TValue>(ref TValue value);

        #endregion
    }

    internal abstract class IntArrayRefParameterBase : IIntArrayRefParameter
    {
        #region IIntArrayRefParameter Members

        public abstract void Method(ref int[] value);

        #endregion
    }

    internal abstract class IntRefParameterBase : IIntRefParameter
    {
        #region IIntRefParameter Members

        public abstract void Method(ref int value);

        #endregion
    }

    internal abstract class StringArrayRefParameterBase : IStringArrayRefParameter
    {
        #region IStringArrayRefParameter Members

        public abstract void Method(ref string[] value);

        #endregion
    }

    internal abstract class StringRefParameterBase : IStringRefParameter
    {
        #region IStringRefParameter Members

        public abstract void Method(ref string value);

        #endregion
    }

    internal abstract class StructArrayRefParameterBase : IStructArrayRefParameter
    {
        #region IStructArrayRefParameter Members

        public abstract void Method(ref StructType[] value);

        #endregion
    }

    internal abstract class StructRefParameterBase : IStructRefParameter
    {
        #region IStructRefParameter Members

        public abstract void Method(ref StructType value);

        #endregion
    }
}