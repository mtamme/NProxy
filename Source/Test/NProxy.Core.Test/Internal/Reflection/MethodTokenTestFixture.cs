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
using System.Linq;
using System.Reflection;
using NProxy.Core.Internal.Reflection;
using NProxy.Core.Test.Common.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal.Reflection
{
    [TestFixture]
    public class MethodTokenTestFixture
    {
        [Test]
        public void EqualsWithGetHashCodeMethodTest()
        {
            // Arrange
            var firstMethodToken = new MethodToken(typeof (object).GetMethod("GetHashCode"));
            var secondMethodToken = new MethodToken(typeof (GenericReturnValue).GetMethod("GetHashCode"));

            // Act
            var isEqual = firstMethodToken.Equals(secondMethodToken);

            // Assert
            Assert.That(isEqual, Is.True);
        }

        [Test]
        public void EqualsWithEqualsMethodTest()
        {
            // Arrange
            var firstMethodToken = new MethodToken(typeof (object).GetMethod("Equals", new[] {typeof (object)}));
            var secondMethodToken = new MethodToken(typeof (GenericReturnValue).GetMethod("Equals", new[] {typeof (object)}));

            // Act
            var isEqual = firstMethodToken.Equals(secondMethodToken);

            // Assert
            Assert.That(isEqual, Is.True);
        }

        [Test]
        public void EqualsWithToStringMethodTest()
        {
            // Arrange
            var firstMethodToken = new MethodToken(typeof (object).GetMethod("ToString"));
            var secondMethodToken = new MethodToken(typeof (GenericReturnValue).GetMethod("ToString"));

            // Act
            var isEqual = firstMethodToken.Equals(secondMethodToken);

            // Assert
            Assert.That(isEqual, Is.True);
        }

        [Test]
        public void EqualsWithGenericReturnValueTest()
        {
            // Arrange
            var firstMethodToken = new MethodToken(typeof (IGenericParameter).GetMethod("Method"));
            var secondMethodToken = new MethodToken(typeof (IGenericParameter).GetMethod("Method"));

            // Act
            var isEqual = firstMethodToken.Equals(secondMethodToken);

            // Assert
            Assert.That(isEqual, Is.True);
        }

        [Test]
        public void EqualsWithGenericReturnValueMethodTest()
        {
            // Arrange
            var firstMethodToken = new MethodToken(typeof (IGenericReturnValue).GetMethod("Method"));
            var secondMethodToken = new MethodToken(typeof (IGenericReturnValue).GetMethod("Method"));

            // Act
            var isEqual = firstMethodToken.Equals(secondMethodToken);

            // Assert
            Assert.That(isEqual, Is.True);
        }

        [Test]
        public void EqualsWithGenericAddEventMethodTest()
        {
            // Arrange
            var firstMethodToken = new MethodToken(typeof (IGenericEvent<int>).GetMethod("add_Event"));
            var secondMethodToken = new MethodToken(typeof (IGenericEvent<string>).GetMethod("add_Event"));

            // Act
            var isEqual = firstMethodToken.Equals(secondMethodToken);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [Test]
        public void EqualsWithGenericGetPropertyMethodTest()
        {
            // Arrange
            var firstMethodToken = new MethodToken(typeof (IGenericProperty<int>).GetMethod("get_Property"));
            var secondMethodToken = new MethodToken(typeof (IGenericProperty<string>).GetMethod("get_Property"));

            // Act
            var isEqual = firstMethodToken.Equals(secondMethodToken);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [Test]
        public void EqualsWithGenericMethodTest()
        {
            // Arrange
            var firstMethodToken = new MethodToken(typeof (IGenericMethod<int>).GetMethod("Method"));
            var secondMethodToken = new MethodToken(typeof (IGenericMethod<string>).GetMethod("Method"));

            // Act
            var isEqual = firstMethodToken.Equals(secondMethodToken);

            // Assert
            Assert.That(isEqual, Is.False);
        }

        [Test]
        public void EqualsWithAppDomainMethodsAndUniquenessTest()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var metadataTokensByModule = new Dictionary<Module, HashSet<int>>();
            var methodTokens = new HashSet<MethodToken>();

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

                foreach (var methodInfo in types.Select(t => t.GetMethods()).SelectMany(m => m))
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

                    var methodToken = new MethodToken(methodInfo);

                    // Act
                    // Implicitly check equality.
                    var isEqual = !methodTokens.Add(methodToken);

                    // Assert
                    Assert.That(isEqual, Is.False);
                }
            }
        }
    }
}