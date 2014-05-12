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
    internal abstract class EnumArrayParameterBase : IEnumArrayParameter
    {
        #region IEnumArrayParameter Members

        public abstract void Method(EnumType[] value);

        #endregion
    }

    internal abstract class EnumParameterBase : IEnumParameter
    {
        #region IEnumParameter Members

        public abstract void Method(EnumType value);

        #endregion
    }

    internal abstract class GenericArrayParameterBase : IGenericArrayParameter
    {
        #region IGenericArrayParameter Members

        public abstract void Method<TValue>(TValue[] value);

        #endregion
    }

    internal abstract class GenericJaggedArrayParameterBase : IGenericJaggedArrayParameter
    {
        #region IGenericJaggedArrayParameter Members

        public abstract void Method<TValue>(TValue[][] value);

        #endregion
    }

    internal abstract class GenericRankArrayParameterBase : IGenericRankArrayParameter
    {
        #region IGenericRankArrayParameter Members

        public abstract void Method<TValue>(TValue[,] value);

        #endregion
    }

    internal abstract class GenericListParameterBase : IGenericListParameter
    {
        #region IGenericListParameter Members

        public abstract void Method<TValue>(List<TValue> value);

        #endregion
    }

    internal abstract class GenericParameterBase : IGenericParameter
    {
        #region IGenericParameter Members

        public abstract void Method<TValue>(TValue value);

        #endregion
    }

    internal abstract class IntArrayParameterBase : IIntArrayParameter
    {
        #region IIntArrayParameter Members

        public abstract void Method(int[] value);

        #endregion
    }

    internal abstract class IntParameterBase : IIntParameter
    {
        #region IIntParameter Members

        public abstract void Method(int value);

        #endregion
    }

    internal abstract class StringArrayParameterBase : IStringArrayParameter
    {
        #region IStringArrayParameter Members

        public abstract void Method(string[] value);

        #endregion
    }

    internal abstract class StringParameterBase : IStringParameter
    {
        #region IStringParameter Members

        public abstract void Method(string value);

        #endregion
    }

    internal abstract class StructArrayParameterBase : IStructArrayParameter
    {
        #region IStructArrayParameter Members

        public abstract void Method(StructType[] value);

        #endregion
    }

    internal abstract class StructParameterBase : IStructParameter
    {
        #region IStructParameter Members

        public abstract void Method(StructType value);

        #endregion
    }
}