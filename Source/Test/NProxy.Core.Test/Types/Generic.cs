//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © Martin Tamme
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
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