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

namespace NProxy.Core.Test.Types
{
    internal class Generic<TValue> : IGeneric<TValue>
    {
        #region IGeneric<TValue> Members

        public virtual void Method(TValue value)
        {
        }

        #endregion
    }

    internal class GenericParameter<TFirst> : IGenericParameter<TFirst>
    {
        #region IGenericParameter<TFirst> Members

        public virtual void Method<TSecond>(TFirst first, TSecond second)
        {
        }

        #endregion
    }

    internal class IntStringGenericEvent : IGenericEvent<int>, IGenericEvent<string>
    {
        #region IGenericEvent<int> Members

        event Action<int> IGenericEvent<int>.Event
        {
            add { }
            remove { }
        }

        #endregion

        #region IGenericEvent<string> Members

        event Action<string> IGenericEvent<string>.Event
        {
            add { }
            remove { }
        }

        #endregion
    }

    internal class IntStringGenericProperty : IGenericProperty<int>, IGenericProperty<string>
    {
        #region IGenericProperty<int> Members

        int IGenericProperty<int>.Property { get; set; }

        #endregion

        #region IGenericProperty<string> Members

        string IGenericProperty<string>.Property { get; set; }

        #endregion
    }

    internal class IntStringGenericMethod : IGenericMethod<int>, IGenericMethod<string>
    {
        #region IGenericMethod<int> Members

        int IGenericMethod<int>.Method()
        {
            return default (int);
        }

        #endregion

        #region IGenericMethod<string> Members

        string IGenericMethod<string>.Method()
        {
            return default (string);
        }

        #endregion
    }
}