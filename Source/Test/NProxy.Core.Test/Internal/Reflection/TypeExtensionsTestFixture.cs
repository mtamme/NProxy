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
using NProxy.Core.Test.Common.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal.Reflection
{
    [TestFixture]
    public sealed class TypeExtensionsTestFixture
    {
        [Test]
        public void GetFullNameForClassTest()
        {
            // Arrange
            // Act
            var fullName = typeof (Class).GetFullName();

            // Assert
            Assert.That(fullName.ToString(), Is.EqualTo("NProxy.Core.Test.Common.Types.Class"));
        }

        [Test]
        public void GetFullNameForOpenGenericClassTest()
        {
            // Arrange
            // Act
            var fullName = typeof (Class<>).GetFullName();

            // Assert
            Assert.That(fullName.ToString(), Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1[TOuter]"));
        }

        [Test]
        public void GetFullNameForClosedGenericClassTest()
        {
            // Arrange
            // Act
            var fullName = typeof (Class<int>).GetFullName();

            // Assert
            Assert.That(fullName.ToString(), Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1[System.Int32]"));
        }

        [Test]
        public void GetFullNameForClosedGenericClassClosedGenericClassTest()
        {
            // Arrange
            // Act
            var fullName = typeof (Class<Class<int>>).GetFullName();

            // Assert
            Assert.That(fullName.ToString(), Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1[NProxy.Core.Test.Common.Types.Class`1[System.Int32]]"));
        }

        [Test]
        public void GetFullNameForNestedOpenGenericClassTest()
        {
            // Arrange
            // Act
            var fullName = typeof (Class.Nested<>).GetFullName();

            // Assert
            Assert.That(fullName.ToString(), Is.EqualTo("NProxy.Core.Test.Common.Types.Class+Nested`1[TInner]"));
        }

        [Test]
        public void GetFullNameForNestedClosedGenericClassTest()
        {
            // Arrange
            // Act
            var fullName = typeof (Class.Nested<int>).GetFullName();

            // Assert
            Assert.That(fullName.ToString(), Is.EqualTo("NProxy.Core.Test.Common.Types.Class+Nested`1[System.Int32]"));
        }

        [Test]
        public void GetFullNameForNestedClassTest()
        {
            // Arrange
            // Act
            var fullName = typeof (Class.Nested).GetFullName();

            // Assert
            Assert.That(fullName.ToString(), Is.EqualTo("NProxy.Core.Test.Common.Types.Class+Nested"));
        }

        [Test]
        public void GetFullNameForOpenGenericNestedOpenGenericClassTest()
        {
            // Arrange
            // Act
            var fullName = typeof (Class<>.Nested<>).GetFullName();

            // Assert
            Assert.That(fullName.ToString(), Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1+Nested`1[TOuter,TInner]"));
        }

        [Test]
        public void GetFullNameForClosedGenericNestedClosedGenericClassTest()
        {
            // Arrange
            // Act
            var fullName = typeof (Class<int>.Nested<int>).GetFullName();

            // Assert
            Assert.That(fullName.ToString(), Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1+Nested`1[System.Int32,System.Int32]"));
        }

        [Test]
        public void GetFullNameForOpenGenericNestedClassTest()
        {
            // Arrange
            // Act
            var fullName = typeof (Class<>.Nested).GetFullName();

            // Assert
            Assert.That(fullName.ToString(), Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1+Nested[TOuter]"));
        }

        [Test]
        public void GetFullNameForClosedGenericNestedClassTest()
        {
            // Arrange
            // Act
            var fullName = typeof (Class<int>.Nested).GetFullName();

            // Assert
            Assert.That(fullName.ToString(), Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1+Nested[System.Int32]"));
        }
    }
}
