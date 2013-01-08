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
using NUnit.Framework;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Test.Common.Types;

namespace NProxy.Core.Test.Internal.Reflection
{
    [TestFixture]
    public sealed class MemberInfoExtensionsTestFixture
    {
        [Test]
        public void GetCustomAttributesTest()
        {
            // Arrange
            var method = typeof (INonIntercepted).GetMethod("Method");

            // Act
            var attributes = method.GetCustomAttributes<NonInterceptedAttribute>(false);

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType(attributes, typeof (NonInterceptedAttribute));
        }

        [Test]
        public void IsDefinedTest()
        {
            // Arrange
            var method = typeof (INonIntercepted).GetMethod("Method");

            // Act
            var isDefined = method.IsDefined<NonInterceptedAttribute>(false);

            // Assert
            Assert.That(isDefined, Is.True);
        }

        [Test]
        public void GetDeclaringTypeTest()
        {
            // Arrange
            var method = typeof (INonIntercepted).GetMethod("Method");

            // Act
            var declaringType = method.GetDeclaringType();

            // Assert
            Assert.That(declaringType, Is.EqualTo(typeof (INonIntercepted)));
        }

        [Test]
        public void GetFullNameTest()
        {
            // Arrange
            var method = typeof (INonIntercepted).GetMethod("Method");

            // Act
            var fullName = method.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.INonIntercepted.Method"));
        }

        [Test]
        public void GetTokenTest()
        {
            // Arrange
            var firstMethod = typeof (INonIntercepted).GetMethod("Method");
            var secondMethod = typeof (INonIntercepted).GetMethod("Method");

            // Act
            var firstToken = firstMethod.GetToken();
            var secondToken = secondMethod.GetToken();

            // Assert
            Assert.That(firstToken, Is.EqualTo(secondToken));
        }
    }
}
