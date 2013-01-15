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

using System;
using System.Collections.Generic;
using System.Reflection;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Test.Common.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal.Reflection
{
    [TestFixture]
    public sealed class MemberInfoExtensionsTestFixture
    {
        [Test]
        public void GetCustomAttributeTest()
        {
            // Arrange
            var methodInfo = typeof (INonIntercepted).GetMethod("Method");

            // Act
            var attribute = methodInfo.GetCustomAttribute<NonInterceptedAttribute>(false);

            // Assert
            Assert.That(attribute, Is.InstanceOf<NonInterceptedAttribute>());
        }

        [Test]
        public void GetCustomAttributesTest()
        {
            // Arrange
            var methodInfo = typeof (INonIntercepted).GetMethod("Method");

            // Act
            var attributes = methodInfo.GetCustomAttributes<NonInterceptedAttribute>(false);

            // Assert
            Assert.That(attributes, Is.All.InstanceOf<NonInterceptedAttribute>());
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
        public void GetDeclaringTypeFromClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class).GetMethod("Method");

            // Act
            var declaringType = methodInfo.GetDeclaringType();

            // Assert
            Assert.That(declaringType, Is.EqualTo(typeof (Class)));
        }

        [Test]
        public void GetDeclaringTypeFromClassTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => typeof (Class).GetDeclaringType());
        }
    }
}