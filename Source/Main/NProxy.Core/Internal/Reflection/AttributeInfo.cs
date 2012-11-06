//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © 2012 Martin Tamme
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
using System.Linq;

namespace NProxy.Core.Internal.Reflection
{
    /// <summary>
    /// Represents attribute information.
    /// </summary>
    internal sealed class AttributeInfo
    {
        /// <summary>
        /// An empty object array.
        /// </summary>
        private static readonly object[] EmptyObjects = new object[0];

        /// <summary>
        /// The attribute type.
        /// </summary>
        private readonly Type _attributeType;

        /// <summary>
        /// The argument types.
        /// </summary>
        private readonly Type[] _argumentTypes;

        /// <summary>
        /// The arguments.
        /// </summary>
        private readonly object[] _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeInfo"/> class.
        /// </summary>
        /// <param name="attributeType">The attribute type.</param>
        public AttributeInfo(Type attributeType)
            : this(attributeType, Type.EmptyTypes, EmptyObjects)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeInfo"/> class.
        /// </summary>
        /// <param name="attributeType">The attribute type.</param>
        /// <param name="argumentTypes">The argument types.</param>
        /// <param name="arguments">The arguments.</param>
        public AttributeInfo(Type attributeType, Type[] argumentTypes, object[] arguments)
        {
            if (attributeType == null)
                throw new ArgumentNullException("attributeType");

            if (argumentTypes == null)
                throw new ArgumentNullException("argumentTypes");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            _attributeType = attributeType;
            _argumentTypes = argumentTypes;
            _arguments = arguments;
        }

        /// <summary>
        /// Returns the attribute type.
        /// </summary>
        public Type AttributeType
        {
            get { return _attributeType; }
        }

        /// <summary>
        /// Returns the argument types.
        /// </summary>
        public Type[] ArgumentTypes
        {
            get { return _argumentTypes; }
        }

        /// <summary>
        /// Returns the arguments.
        /// </summary>
        public object[] Arguments
        {
            get { return _arguments; }
        }

        #region Object Members

        public override int GetHashCode()
        {
            return _attributeType.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var attributeInfo = obj as AttributeInfo;

            if (attributeInfo == null)
                return false;

            if (attributeInfo.AttributeType != AttributeType)
                return false;

            if (!attributeInfo.ArgumentTypes.SequenceEqual(ArgumentTypes))
                return false;

            return attributeInfo.Arguments.SequenceEqual(Arguments);
        }

        #endregion
    }
}
