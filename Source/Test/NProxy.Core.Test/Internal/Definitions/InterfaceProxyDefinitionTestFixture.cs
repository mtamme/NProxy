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
    public sealed class InterfaceProxyDefinitionTestFixture
    {
        [Test]
        public void AcceptVisitorTest()
        {
            // Arrange
            var proxyDefinition = new InterfaceProxyDefinition(typeof (IOne), new[] {typeof (ITwo), typeof (IOneTwo)});

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

            Assert.That(proxyDefinitionVisitor.EventInfos.Count, Is.EqualTo(3));

            Assert.That(proxyDefinitionVisitor.PropertyInfos.Count, Is.EqualTo(6));

            Assert.That(proxyDefinitionVisitor.MethodInfos.Count, Is.EqualTo(12));
        }

        [Test]
        public void EqualsWithoutInterfacesTest()
        {
            // Arrange
            var firstProxyDefinition = new InterfaceProxyDefinition(typeof (IBase), Type.EmptyTypes);
            var secondProxyDefinition = new InterfaceProxyDefinition(typeof (IBase), Type.EmptyTypes);

            // Act
            var equals = firstProxyDefinition.Equals(secondProxyDefinition);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithSwappedInterfacesTest()
        {
            // Arrange
            var firstProxyDefinition = new InterfaceProxyDefinition(typeof (IBase), new[] {typeof (IOne), typeof (ITwo)});
            var secondProxyDefinition = new InterfaceProxyDefinition(typeof (IBase), new[] {typeof (ITwo), typeof (IOne)});

            // Act
            var equals = firstProxyDefinition.Equals(secondProxyDefinition);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithDeclaringInterfaceTest()
        {
            // Arrange
            var firstProxyDefinition = new InterfaceProxyDefinition(typeof (IOne), new[] {typeof (IBase)});
            var secondProxyDefinition = new InterfaceProxyDefinition(typeof (IOne), Type.EmptyTypes);

            // Act
            var equals = firstProxyDefinition.Equals(secondProxyDefinition);

            // Assert
            Assert.That(equals, Is.True);
        }
    }
}