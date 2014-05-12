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
    internal abstract class ObjectGetPropertyBase : IObjectGetProperty
    {
        #region IObjectGetProperty Members

        public abstract object Property { get; }

        #endregion
    }

    internal abstract class ObjectGetSetPropertyBase : IObjectGetSetProperty
    {
        #region IObjectGetSetProperty Members

        public abstract object Property { get; set; }

        #endregion
    }

    internal abstract class ObjectSetPropertyBase : IObjectSetProperty
    {
        #region IObjectSetProperty Members

        public abstract object Property { set; }

        #endregion
    }

    internal abstract class ObjectGetIndexerBase : IObjectGetIndexer
    {
        #region IObjectGetIndexer Members

        public abstract object this[int index] { get; }

        #endregion
    }

    internal abstract class ObjectGetSetIndexerBase : IObjectGetSetIndexer
    {
        #region IObjectGetSetIndexer Members

        public abstract object this[int index] { get; set; }

        #endregion
    }

    internal abstract class ObjectSetIndexerBase : IObjectSetIndexer
    {
        #region IObjectSetIndexer Members

        public abstract object this[int index] { set; }

        #endregion
    }
}