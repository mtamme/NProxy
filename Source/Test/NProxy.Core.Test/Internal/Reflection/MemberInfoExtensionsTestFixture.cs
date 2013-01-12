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

        [Test]
        public void GetFullNameForClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class.Method"));
        }

        [Test]
        public void GetFullNameForOpenGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1[TOuter].Method"));
        }

        [Test]
        public void GetFullNameForClosedGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<int>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1[System.Int32].Method"));
        }

        [Test]
        public void GetFullNameForNestedOpenGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class.Nested<>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class+Nested`1[TInner].Method"));
        }

        [Test]
        public void GetFullNameForNestedClosedGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class.Nested<int>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class+Nested`1[System.Int32].Method"));
        }

        [Test]
        public void GetFullNameForNestedClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class.Nested).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class+Nested.Method"));
        }

        [Test]
        public void GetFullNameForOpenGenericNestedOpenGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<>.Nested<>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1+Nested`1[TOuter,TInner].Method"));
        }

        [Test]
        public void GetFullNameForClosedGenericNestedClosedGenericClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<int>.Nested<int>).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1+Nested`1[System.Int32,System.Int32].Method"));
        }

        [Test]
        public void GetFullNameForOpenGenericNestedClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<>.Nested).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1+Nested[TOuter].Method"));
        }

        [Test]
        public void GetFullNameForClosedGenericNestedClassMethodTest()
        {
            // Arrange
            var methodInfo = typeof (Class<int>.Nested).GetMethod("Method");

            // Act
            var fullName = methodInfo.GetFullName();

            // Assert
            Assert.That(fullName, Is.EqualTo("NProxy.Core.Test.Common.Types.Class`1+Nested[System.Int32].Method"));
        }

        [Test]
        public void GetTokenEqualityTest()
        {
            // Arrange
            var firstMethodInfo = typeof (Action).GetMethod("Invoke");
            var secondMethodInfo = typeof (Action).GetMethod("Invoke");

            // Act
            var firstMemberToken = firstMethodInfo.GetToken();
            var secondMemberToken = secondMethodInfo.GetToken();

            // Assert
            Assert.That(firstMemberToken, Is.EqualTo(secondMemberToken));
        }

        [Test]
        public void GetTokenUniquenessTest()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var metadataTokensByModule = new Dictionary<Module, HashSet<int>>();
            var memberTokens = new HashSet<MemberToken>();

            foreach (var assembly in assemblies)
            {
                Type[] types;

                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException)
                {
                    continue;
                }

                foreach (var type in types)
                {
                    var memberInfos = type.GetMembers();

                    foreach (var memberInfo in memberInfos)
                    {
                        // Arrange
                        HashSet<int> metadataTokens;

                        if (!metadataTokensByModule.TryGetValue(memberInfo.Module, out metadataTokens))
                        {
                            metadataTokens = new HashSet<int>();
                            metadataTokensByModule.Add(memberInfo.Module, metadataTokens);
                        }

                        if (!metadataTokens.Add(memberInfo.MetadataToken))
                            continue;

                        // Act
                        var memberToken = memberInfo.GetToken();

                        // Assert
                        Assert.That(memberTokens.Add(memberToken), Is.True, "Member identifier is not unique");
                    }
                }
            }
        }
    }
}
