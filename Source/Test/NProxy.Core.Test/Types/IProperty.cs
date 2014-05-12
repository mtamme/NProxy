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

namespace NProxy.Core.Test.Types
{
    internal interface IObjectGetProperty
    {
        object Property { get; }
    }

    internal interface IObjectGetSetProperty
    {
        object Property { get; set; }
    }

    internal interface IObjectSetProperty
    {
        object Property { set; }
    }

    internal interface IObjectGetIndexer
    {
        object this[int index] { get; }
    }

    internal interface IObjectSetIndexer
    {
        object this[int index] { set; }
    }

    internal interface IObjectGetSetIndexer
    {
        object this[int index] { get; set; }
    }
}