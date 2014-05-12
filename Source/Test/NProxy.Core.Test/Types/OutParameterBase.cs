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
    internal abstract class EnumArrayOutParameterBase : IEnumArrayOutParameter
    {
        #region IEnumArrayOutParameter Members

        public abstract void Method(out EnumType[] value);

        #endregion
    }

    internal abstract class EnumOutParameterBase : IEnumOutParameter
    {
        #region IEnumOutParameter Members

        public abstract void Method(out EnumType value);

        #endregion
    }

    internal abstract class GenericArrayOutParameterBase : IGenericArrayOutParameter
    {
        #region IGenericArrayOutParameter Members

        public abstract void Method<TValue>(out TValue[] value);

        #endregion
    }

    internal abstract class GenericJaggedArrayOutParameterBase : IGenericJaggedArrayOutParameter
    {
        #region IGenericJaggedArrayOutParameter Members

        public abstract void Method<TValue>(out TValue[][] value);

        #endregion
    }

    internal abstract class GenericRankArrayOutParameterBase : IGenericRankArrayOutParameter
    {
        #region IGenericRankArrayOutParameter Members

        public abstract void Method<TValue>(out TValue[,] value);

        #endregion
    }

    internal abstract class GenericListOutParameterBase : IGenericListOutParameter
    {
        #region IGenericListOutParameter Members

        public abstract void Method<TValue>(out List<TValue> value);

        #endregion
    }

    internal abstract class GenericOutParameterBase : IGenericOutParameter
    {
        #region IGenericOutParameter Members

        public abstract void Method<TValue>(out TValue value);

        #endregion
    }

    internal abstract class IntArrayOutParameterBase : IIntArrayOutParameter
    {
        #region IIntArrayOutParameter Members

        public abstract void Method(out int[] value);

        #endregion
    }

    internal abstract class IntOutParameterBase : IIntOutParameter
    {
        #region IIntOutParameter Members

        public abstract void Method(out int value);

        #endregion
    }

    internal abstract class StringArrayOutParameterBase : IStringArrayOutParameter
    {
        #region IStringArrayOutParameter Members

        public abstract void Method(out string[] value);

        #endregion
    }

    internal abstract class StringOutParameterBase : IStringOutParameter
    {
        #region IStringOutParameter Members

        public abstract void Method(out string value);

        #endregion
    }

    internal abstract class StructArrayOutParameterBase : IStructArrayOutParameter
    {
        #region IStructArrayOutParameter Members

        public abstract void Method(out StructType[] value);

        #endregion
    }

    internal abstract class StructOutParameterBase : IStructOutParameter
    {
        #region IStructOutParameter Members

        public abstract void Method(out StructType value);

        #endregion
    }
}