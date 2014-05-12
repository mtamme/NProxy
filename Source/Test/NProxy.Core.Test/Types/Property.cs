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
    internal class ObjectGetProperty : IObjectGetProperty
    {
        #region IObjectGetProperty Members

        public virtual object Property
        {
            get { return null; }
        }

        #endregion
    }

    internal class ObjectGetSetProperty : IObjectGetSetProperty
    {
        #region IObjectGetSetProperty Members

        public virtual object Property
        {
            get { return null; }
            set { }
        }

        #endregion
    }

    internal class ObjectSetProperty : IObjectSetProperty
    {
        #region IObjectSetProperty Members

        public virtual object Property
        {
            set { }
        }

        #endregion
    }

    internal class ObjectGetIndexer : IObjectGetIndexer
    {
        #region IObjectGetIndexer Members

        public virtual object this[int index]
        {
            get { return null; }
        }

        #endregion
    }

    internal class ObjectGetSetIndexer : IObjectGetSetIndexer
    {
        #region IObjectGetSetIndexer Members

        public virtual object this[int index]
        {
            get { return null; }
            set { }
        }

        #endregion
    }

    internal class ObjectSetIndexer : IObjectSetIndexer
    {
        #region IObjectSetIndexer Members

        public virtual object this[int index]
        {
            set { }
        }

        #endregion
    }
}