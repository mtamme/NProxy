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
    internal interface IAsyncReturnValue
    {
        Task Method();
    }

    internal interface IEnumArrayReturnValue
    {
        EnumType[] Method();
    }

    internal interface IEnumReturnValue
    {
        EnumType Method();
    }

    internal interface IGenericArrayReturnValue
    {
        TReturn[] Method<TReturn>();
    }

    internal interface IGenericJaggedArrayReturnValue
    {
        TReturn[][] Method<TReturn>();
    }

    internal interface IGenericRankArrayReturnValue
    {
        TReturn[,] Method<TReturn>();
    }

    internal interface IGenericListReturnValue
    {
        List<TReturn> Method<TReturn>();
    }

    internal interface IGenericReturnValue
    {
        TReturn Method<TReturn>();
    }

    internal interface IIntArrayReturnValue
    {
        int[] Method();
    }

    internal interface IIntReturnValue
    {
        int Method();
    }

    internal interface IStringArrayReturnValue
    {
        string[] Method();
    }

    internal interface IStringReturnValue
    {
        string Method();
    }

    internal interface IStructArrayReturnValue
    {
        StructType[] Method();
    }

    internal interface IStructReturnValue
    {
        StructType Method();
    }

    internal interface IVoidReturnValue
    {
        void Method();
    }
}