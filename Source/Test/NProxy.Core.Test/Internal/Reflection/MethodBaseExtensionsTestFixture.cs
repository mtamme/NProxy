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

using NProxy.Core.Internal.Reflection;
using NProxy.Core.Test.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal.Reflection
{
    [TestFixture]
    public sealed class MethodBaseExtensionsTestFixture
    {
        [Test]
        public void CanOverrideTest()
        {
            // Arrange
            var methodInfo = typeof (IIntParameter).GetMethod("Method");

            // Act
            var canOverride = methodInfo.CanOverride();

            // Assert
            Assert.That(canOverride, Is.True);
        }

        [Test]
        public void GetFullNameForClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class.Method"));
        }

        [Test]
        public void GetFullNameForOpenGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1[TOuter].Method"));
        }

        [Test]
        public void GetFullNameForClosedGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<int>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1[System.Int32].Method"));
        }

        [Test]
        public void GetFullNameForClosedGenericClassClosedGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<Class<int>>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1[NProxy.Core.Test.Types.Class`1[System.Int32]].Method"));
        }

        [Test]
        public void GetFullNameForNestedOpenGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class.Nested<>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class+Nested`1[TInner].Method"));
        }

        [Test]
        public void GetFullNameForNestedClosedGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class.Nested<int>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class+Nested`1[System.Int32].Method"));
        }

        [Test]
        public void GetFullNameForNestedClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class.Nested).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class+Nested.Method"));
        }

        [Test]
        public void GetFullNameForOpenGenericNestedOpenGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<>.Nested<>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1+Nested`1[TOuter,TInner].Method"));
        }

        [Test]
        public void GetFullNameForClosedGenericNestedClosedGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<int>.Nested<int>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1+Nested`1[System.Int32,System.Int32].Method"));
        }

        [Test]
        public void GetFullNameForOpenGenericNestedClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<>.Nested).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1+Nested[TOuter].Method"));
        }

        [Test]
        public void GetFullNameForClosedGenericNestedClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<int>.Nested).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Types.Class`1+Nested[System.Int32].Method"));
        }

        [Test]
        public void GetParameterTypesTest()
        {
            // Arrange
            var methodInfo = typeof (IIntParameter).GetMethod("Method");

            // Act
            var parameterTypes = methodInfo.GetParameterTypes();

            // Assert
            Assert.That(parameterTypes, Is.EqualTo(new[] {typeof (int)}));
        }
    }
}