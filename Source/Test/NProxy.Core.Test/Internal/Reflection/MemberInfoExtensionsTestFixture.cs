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
    public sealed class MemberInfoExtensionsTestFixture
    {
        [Test]
        public void GetCustomAttributesTest()
        {
            // Arrange
            var methodInfo = typeof (INonIntercepted).GetMethod("Method");

            // Act
            var attributes = methodInfo.GetCustomAttributes<NonInterceptedAttribute>(false);

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType(attributes, typeof (NonInterceptedAttribute));
        }

        [Test]
        public void IsDefinedTest()
        {
            // Arrange
            var methodInfo = typeof (INonIntercepted).GetMethod("Method");

            // Act
            var isDefined = methodInfo.IsDefined<NonInterceptedAttribute>(false);

            // Assert
            Assert.That(isDefined, Is.True);
        }

        [Test]
        public void GetDeclaringTypeTest()
        {
            // Arrange
            var methodInfo = typeof (INonIntercepted).GetMethod("Method");

            // Act
            var declaringType = methodInfo.GetDeclaringType();

            // Assert
            Assert.That(declaringType, Is.EqualTo(typeof (INonIntercepted)));
        }

        [Test]
        public void GetFullNameTest()
        {
            // Arrange
            var methodInfo = typeof (INonIntercepted).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.INonIntercepted.Method"));
        }

        [Test]
        public void GetTokenTest()
        {
            // Arrange
            var firstMethodInfo = typeof (INonIntercepted).GetMethod("Method");
            var secondMethodInfo = typeof (INonIntercepted).GetMethod("Method");

            // Act
            var firstToken = firstMethodInfo.GetToken();
            var secondToken = secondMethodInfo.GetToken();

            // Assert
            Assert.That(firstToken, Is.EqualTo(secondToken));
        }
    }
}
