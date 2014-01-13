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