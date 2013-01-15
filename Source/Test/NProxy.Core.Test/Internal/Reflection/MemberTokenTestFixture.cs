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
    public class MemberTokenTestFixture
    {
        [Test]
        public void EqualsWithGetHashCodeMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (object).GetMethod("GetHashCode"));
            var secondMemberToken = new MemberToken(typeof (GenericReturnValue).GetMethod("GetHashCode"));

            // Act
            var isEqual = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(isEqual, Is.True);
        }

        [Test]
        public void EqualsWithEqualsMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (object).GetMethod("Equals", new[] {typeof (object)}));
            var secondMemberToken = new MemberToken(typeof (GenericReturnValue).GetMethod("Equals", new[] {typeof (object)}));

            // Act
            var isEqual = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(isEqual, Is.True);
        }

        [Test]
        public void EqualsWithToStringMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (object).GetMethod("ToString"));
            var secondMemberToken = new MemberToken(typeof (GenericReturnValue).GetMethod("ToString"));

            // Act
            var isEqual = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(isEqual, Is.True);
        }

        [Test]
        public void EqualsWithGenericReturnValueTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (IGenericReturnValue).GetMethod("Method"));
            var secondMemberToken = new MemberToken(typeof (IGenericReturnValue).GetMethod("Method"));

            // Act
            var isEqual = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(isEqual, Is.True);
        }

        [Test]
        public void EqualsWithGenericReturnValueMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (IGenericReturnValue).GetMethod("Method"));
            var secondMemberToken = new MemberToken(typeof (IGenericReturnValue).GetMethod("Method"));

            // Act
            var isEqual = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(isEqual, Is.True);
        }

        [Test]
        public void EqualsWithGenericAddEventMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (IGenericEvent<int>).GetMethod("add_Event"));
            var secondMemberToken = new MemberToken(typeof (IGenericEvent<string>).GetMethod("add_Event"));

            // Act
            var isEqual = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [Test]
        public void EqualsWithGenericGetPropertyMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (IGenericProperty<int>).GetMethod("get_Property"));
            var secondMemberToken = new MemberToken(typeof (IGenericProperty<string>).GetMethod("get_Property"));

            // Act
            var isEqual = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [Test]
        public void EqualsWithGenericMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (IGenericMethod<int>).GetMethod("Method"));
            var secondMemberToken = new MemberToken(typeof (IGenericMethod<string>).GetMethod("Method"));

            // Act
            var isEqual = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(isEqual, Is.False);
        }
    }
}