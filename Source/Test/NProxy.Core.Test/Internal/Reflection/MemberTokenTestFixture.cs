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
using NProxy.Core.Test.Types;
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
            var equals = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithEqualsMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (object).GetMethod("Equals", new[] {typeof (object)}));
            var secondMemberToken = new MemberToken(typeof (GenericReturnValue).GetMethod("Equals", new[] {typeof (object)}));

            // Act
            var equals = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithToStringMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (object).GetMethod("ToString"));
            var secondMemberToken = new MemberToken(typeof (GenericReturnValue).GetMethod("ToString"));

            // Act
            var equals = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithGenericReturnValueTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (IGenericParameter).GetMethod("Method"));
            var secondMemberToken = new MemberToken(typeof (IGenericParameter).GetMethod("Method"));

            // Act
            var equals = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithGenericReturnValueMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (IGenericReturnValue).GetMethod("Method"));
            var secondMemberToken = new MemberToken(typeof (IGenericReturnValue).GetMethod("Method"));

            // Act
            var equals = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithGenericAddEventMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (IGenericEvent<int>).GetMethod("add_Event"));
            var secondMemberToken = new MemberToken(typeof (IGenericEvent<string>).GetMethod("add_Event"));

            // Act
            var equals = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(equals, Is.False);
        }

        [Test]
        public void EqualsWithGenericGetPropertyMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (IGenericProperty<int>).GetMethod("get_Property"));
            var secondMemberToken = new MemberToken(typeof (IGenericProperty<string>).GetMethod("get_Property"));

            // Act
            var equals = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(equals, Is.False);
        }

        [Test]
        public void EqualsWithGenericMethodTest()
        {
            // Arrange
            var firstMemberToken = new MemberToken(typeof (IGenericMethod<int>).GetMethod("Method"));
            var secondMemberToken = new MemberToken(typeof (IGenericMethod<string>).GetMethod("Method"));

            // Act
            var equals = firstMemberToken.Equals(secondMemberToken);

            // Assert
            Assert.That(equals, Is.False);
        }

        [Test]
        public void EqualsWithAppDomainMethodsAndUniquenessTest()
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
                    MethodInfo[] methodInfos;

                    try
                    {
                        methodInfos = type.GetMethods();
                    }
                    catch (TypeLoadException)
                    {
                        continue;
                    }

                    foreach (var methodInfo in methodInfos)
                    {
                        // Arrange
                        HashSet<int> metadataTokens;

                        if (!metadataTokensByModule.TryGetValue(methodInfo.Module, out metadataTokens))
                        {
                            metadataTokens = new HashSet<int>();
                            metadataTokensByModule.Add(methodInfo.Module, metadataTokens);
                        }

                        if (!metadataTokens.Add(methodInfo.MetadataToken))
                            continue;

                        var memberToken = new MemberToken(methodInfo);

                        // Act
                        // Implicitly check equality.
                        var equals = !memberTokens.Add(memberToken);

                        // Assert
                        Assert.That(equals, Is.False);
                    }
                }
            }
        }
    }
}