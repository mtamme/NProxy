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
            Assert.That(proxyDefinitionVisitor.InterfaceTypes.Count, Is.EqualTo(4));
            Assert.That(proxyDefinitionVisitor.InterfaceTypes, Contains.Item(typeof (IBase)));
            Assert.That(proxyDefinitionVisitor.InterfaceTypes, Contains.Item(typeof (IOne)));
            Assert.That(proxyDefinitionVisitor.InterfaceTypes, Contains.Item(typeof (ITwo)));
            Assert.That(proxyDefinitionVisitor.InterfaceTypes, Contains.Item(typeof (IOneTwo)));

            Assert.That(proxyDefinitionVisitor.ConstructorInfos.Count, Is.EqualTo(1));

            Assert.That(proxyDefinitionVisitor.EventInfos.Count, Is.EqualTo(4));

            Assert.That(proxyDefinitionVisitor.PropertyInfos.Count, Is.EqualTo(8));

            Assert.That(proxyDefinitionVisitor.MethodInfos.Count, Is.EqualTo(10));
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