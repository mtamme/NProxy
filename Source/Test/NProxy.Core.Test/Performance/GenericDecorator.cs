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
using NProxy.Core.Test.Performance.Types;

namespace NProxy.Core.Test.Performance
{
    public class GenericDecorator : IGeneric
    {
        private readonly IGeneric _target;

        public GenericDecorator(IGeneric target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            
            _target = target;
        }

        #region IGeneric Members

        public TValue Invoke<TValue>(TValue value)
        {
            return _target.Invoke(value);
        }

        #endregion
    }
}