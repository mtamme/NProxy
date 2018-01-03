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
        public void GetMethodsTest()
        {
            // Arrange
            var eventInfo = typeof (IActionEvent).GetEvent("Event");

            // Act
            var methodInfos = eventInfo.GetMethods();

            // Assert
            Assert.That(methodInfos.Count(), Is.EqualTo(2));
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