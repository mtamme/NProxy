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
using NProxy.Core.Test.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal.Reflection
{
    [TestFixture]
    public sealed class EventInfoExtensionsTestFixture
    {
        [Test]
        public void CanOverrideTest()
        {
            // Arrange
            var eventInfo = typeof (IActionEvent).GetEvent("Event");

            // Act
            var canOverride = eventInfo.CanOverride();

            // Assert
            Assert.That(canOverride, Is.True);
        }

        [Test]
        public void IsAbstractTest()
        {
            // Arrange
            var eventInfo = typeof (IActionEvent).GetEvent("Event");

            // Act
            var isAbstract = eventInfo.IsAbstract();

            // Assert
            Assert.That(isAbstract, Is.True);
        }

        [Test]
        public void GetAccessorMethodsTest()
        {
            // Arrange
            var eventInfo = typeof (IActionEvent).GetEvent("Event");

            // Act
            var accessorMethodInfos = eventInfo.GetAccessorMethods();

            // Assert
            Assert.That(accessorMethodInfos.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetFullNameForClassEventTest()
        {
            // Arrange
            var eventInfo = typeof (Class).GetEvent("Event");

            // Act
            var fullName = eventInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class.Event"));
        }

        [Test]
        public void GetFullNameForOpenGenericClassEventTest()
        {
            // Arrange
            var eventInfo = typeof (Class<>).GetEvent("Event");

            // Act
            var fullName = eventInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1[TOuter].Event"));
        }

        [Test]
        public void GetFullNameForClosedGenericClassEventTest()
        {
            // Arrange
            var eventInfo = typeof (Class<int>).GetEvent("Event");

            // Act
            var fullName = eventInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1[System.Int32].Event"));
        }

        [Test]
        public void GetFullNameForClosedGenericClassClosedGenericClassEventTest()
        {
            // Arrange
            var eventInfo = typeof (Class<Class<int>>).GetEvent("Event");

            // Act
            var fullName = eventInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1[NProxy.Core.Test.Types.Class`1[System.Int32]].Event"));
        }

        [Test]
        public void GetFullNameForNestedOpenGenericClassEventTest()
        {
            // Arrange
            var eventInfo = typeof (Class.Nested<>).GetEvent("Event");

            // Act
            var fullName = eventInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class+Nested`1[TInner].Event"));
        }

        [Test]
        public void GetFullNameForNestedClosedGenericClassEventTest()
        {
            // Arrange
            var eventInfo = typeof (Class.Nested<int>).GetEvent("Event");

            // Act
            var fullName = eventInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class+Nested`1[System.Int32].Event"));
        }

        [Test]
        public void GetFullNameForNestedClassEventTest()
        {
            // Arrange
            var eventInfo = typeof (Class.Nested).GetEvent("Event");

            // Act
            var fullName = eventInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class+Nested.Event"));
        }

        [Test]
        public void GetFullNameForOpenGenericNestedOpenGenericClassEventTest()
        {
            // Arrange
            var eventInfo = typeof (Class<>.Nested<>).GetEvent("Event");

            // Act
            var fullName = eventInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1+Nested`1[TOuter,TInner].Event"));
        }

        [Test]
        public void GetFullNameForClosedGenericNestedClosedGenericClassEventTest()
        {
            // Arrange
            var eventInfo = typeof (Class<int>.Nested<int>).GetEvent("Event");

            // Act
            var fullName = eventInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1+Nested`1[System.Int32,System.Int32].Event"));
        }

        [Test]
        public void GetFullNameForOpenGenericNestedClassEventTest()
        {
            // Arrange
            var eventInfo = typeof (Class<>.Nested).GetEvent("Event");

            // Act
            var fullName = eventInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1+Nested[TOuter].Event"));
        }

        [Test]
        public void GetFullNameForClosedGenericNestedClassEventTest()
        {
            // Arrange
            var eventInfo = typeof (Class<int>.Nested).GetEvent("Event");

            // Act
            var fullName = eventInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1+Nested[System.Int32].Event"));
        }
    }
}