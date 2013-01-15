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

        [Test]
        public void GetFullNameForClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class.Property"));
        }

        [Test]
        public void GetFullNameForOpenGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1[TOuter].Property"));
        }

        [Test]
        public void GetFullNameForClosedGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<int>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1[System.Int32].Property"));
        }

        [Test]
        public void GetFullNameForClosedGenericClassClosedGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<Class<int>>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1[NProxy.Core.Test.Common.Types.Class`1[System.Int32]].Property"));
        }

        [Test]
        public void GetFullNameForNestedOpenGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class.Nested<>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class+Nested`1[TInner].Property"));
        }

        [Test]
        public void GetFullNameForNestedClosedGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class.Nested<int>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class+Nested`1[System.Int32].Property"));
        }

        [Test]
        public void GetFullNameForNestedClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class.Nested).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class+Nested.Property"));
        }

        [Test]
        public void GetFullNameForOpenGenericNestedOpenGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<>.Nested<>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1+Nested`1[TOuter,TInner].Property"));
        }

        [Test]
        public void GetFullNameForClosedGenericNestedClosedGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<int>.Nested<int>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1+Nested`1[System.Int32,System.Int32].Property"));
        }

        [Test]
        public void GetFullNameForOpenGenericNestedClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<>.Nested).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1+Nested[TOuter].Property"));
        }

        [Test]
        public void GetFullNameForClosedGenericNestedClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<int>.Nested).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1+Nested[System.Int32].Property"));
        }
    }
}