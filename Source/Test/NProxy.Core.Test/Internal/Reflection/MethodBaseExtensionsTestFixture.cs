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