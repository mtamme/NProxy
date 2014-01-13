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