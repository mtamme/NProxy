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
        public void GetMethodsTest()
        {
            // Arrange
            var propertyInfo = typeof (IObjectGetSetProperty).GetProperty("Property");

            // Act
            var methodInfos = propertyInfo.GetMethods();

            // Assert
            Assert.That(methodInfos.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetFullNameForClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class.Property"));
        }

        [Test]
        public void GetFullNameForOpenGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1[TOuter].Property"));
        }

        [Test]
        public void GetFullNameForClosedGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<int>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1[System.Int32].Property"));
        }

        [Test]
        public void GetFullNameForClosedGenericClassClosedGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<Class<int>>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1[NProxy.Core.Test.Types.Class`1[System.Int32]].Property"));
        }

        [Test]
        public void GetFullNameForNestedOpenGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class.Nested<>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class+Nested`1[TInner].Property"));
        }

        [Test]
        public void GetFullNameForNestedClosedGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class.Nested<int>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class+Nested`1[System.Int32].Property"));
        }

        [Test]
        public void GetFullNameForNestedClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class.Nested).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class+Nested.Property"));
        }

        [Test]
        public void GetFullNameForOpenGenericNestedOpenGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<>.Nested<>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1+Nested`1[TOuter,TInner].Property"));
        }

        [Test]
        public void GetFullNameForClosedGenericNestedClosedGenericClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<int>.Nested<int>).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1+Nested`1[System.Int32,System.Int32].Property"));
        }

        [Test]
        public void GetFullNameForOpenGenericNestedClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<>.Nested).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1+Nested[TOuter].Property"));
        }

        [Test]
        public void GetFullNameForClosedGenericNestedClassPropertyTest()
        {
            // Arrange
            var propertyInfo = typeof (Class<int>.Nested).GetProperty("Property");

            // Act
            var fullName = propertyInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1+Nested[System.Int32].Property"));
        }
    }
}