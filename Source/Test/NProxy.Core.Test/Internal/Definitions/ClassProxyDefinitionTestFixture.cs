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
using System.Linq;
using NProxy.Core.Internal.Definitions;
using NProxy.Core.Test.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal.Definitions
{
    [TestFixture]
    public sealed class ClassProxyDefinitionTestFixture
    {
        [Test]
        public void AcceptVisitorTest()
        {
            // Arrange
            var proxyDefinition = new ClassProxyDefinition(typeof (Other), new[] {typeof (IOne), typeof (ITwo), typeof (IOneTwo)});

            // Act
            var proxyDefinitionVisitor = new CollectingProxyDefinitionVisitor();

            proxyDefinition.AcceptVisitor(proxyDefinitionVisitor);

            // Assert
            Assert.That(proxyDefinition.DeclaringType, Is.EqualTo(typeof (Other)));
            Assert.That(proxyDefinition.ParentType, Is.EqualTo(typeof (Other)));
            Assert.That(proxyDefinition.ImplementedInterfaces.Count(), Is.EqualTo(5));
            Assert.That(proxyDefinition.ImplementedInterfaces, Contains.Item(typeof (IOther)));
            Assert.That(proxyDefinition.ImplementedInterfaces, Contains.Item(typeof (IBase)));
            Assert.That(proxyDefinition.ImplementedInterfaces, Contains.Item(typeof (IOne)));
            Assert.That(proxyDefinition.ImplementedInterfaces, Contains.Item(typeof (ITwo)));
            Assert.That(proxyDefinition.ImplementedInterfaces, Contains.Item(typeof (IOneTwo)));
            Assert.That(proxyDefinitionVisitor.InterfaceTypes.Count, Is.EqualTo(4));
            Assert.That(proxyDefinitionVisitor.InterfaceTypes, Contains.Item(typeof (IBase)));
            Assert.That(proxyDefinitionVisitor.InterfaceTypes, Contains.Item(typeof (IOne)));
            Assert.That(proxyDefinitionVisitor.InterfaceTypes, Contains.Item(typeof (ITwo)));
            Assert.That(proxyDefinitionVisitor.InterfaceTypes, Contains.Item(typeof (IOneTwo)));

            Assert.That(proxyDefinitionVisitor.ConstructorInfos.Count, Is.EqualTo(1));

            Assert.That(proxyDefinitionVisitor.EventInfos.Count, Is.EqualTo(3));

            Assert.That(proxyDefinitionVisitor.PropertyInfos.Count, Is.EqualTo(6));

            Assert.That(proxyDefinitionVisitor.MethodInfos.Count, Is.EqualTo(7));
        }

        [Test]
        public void EqualsWithoutInterfacesTest()
        {
            // Arrange
            var firstProxyDefinition = new ClassProxyDefinition(typeof (Other), Type.EmptyTypes);
            var secondProxyDefinition = new ClassProxyDefinition(typeof (Other), Type.EmptyTypes);

            // Act
            var equals = firstProxyDefinition.Equals(secondProxyDefinition);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithSwappedInterfacesTest()
        {
            // Arrange
            var firstProxyDefinition = new ClassProxyDefinition(typeof (Other), new[] {typeof (IOne), typeof (ITwo)});
            var secondProxyDefinition = new ClassProxyDefinition(typeof (Other), new[] {typeof (ITwo), typeof (IOne)});

            // Act
            var equals = firstProxyDefinition.Equals(secondProxyDefinition);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithDeclaringInterfaceTest()
        {
            // Arrange
            var firstProxyDefinition = new ClassProxyDefinition(typeof (Other), new[] {typeof (IOther)});
            var secondProxyDefinition = new ClassProxyDefinition(typeof (Other), Type.EmptyTypes);

            // Act
            var equals = firstProxyDefinition.Equals(secondProxyDefinition);

            // Assert
            Assert.That(equals, Is.True);
        }
    }
}