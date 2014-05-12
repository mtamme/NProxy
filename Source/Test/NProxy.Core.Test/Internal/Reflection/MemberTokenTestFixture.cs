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