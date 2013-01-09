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
using System.Linq;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Test.Common.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal.Reflection
{
    [TestFixture]
    public sealed class PropertyInfoExtensionsTestFixture
    {
        [Test]
        public void CanOverrideTest()
        {
            // Arrange
            var propertyInfo = typeof (IObjectGetSetProperty).GetProperty("Property");

            // Act
            var canOverride = propertyInfo.CanOverride();

            // Assert
            Assert.That(canOverride, Is.True);
        }

        [Test]
        public void IsAbstractTest()
        {
            // Arrange
            var propertyInfo = typeof (IObjectGetSetProperty).GetProperty("Property");

            // Act
            var canOverride = propertyInfo.IsAbstract();

            // Assert
            Assert.That(canOverride, Is.True);
        }

        [Test]
        public void GetAccessorMethodsTest()
        {
            // Arrange
            var propertyInfo = typeof (IObjectGetSetProperty).GetProperty("Property");

            // Act
            var accessorMethodInfos = propertyInfo.GetAccessorMethods();

            // Assert
            Assert.That(accessorMethodInfos.Count(), Is.EqualTo(2));
        }
    }
}
