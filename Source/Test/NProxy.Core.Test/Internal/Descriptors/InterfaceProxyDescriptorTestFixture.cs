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
using NProxy.Core.Internal.Descriptors;
using NProxy.Core.Test.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal.Descriptors
{
    [TestFixture]
    public sealed class InterfaceProxyDescriptorTestFixture
    {
        [Test]
        public void AcceptTest()
        {
            // Arrange
            var proxyDescriptor = new InterfaceProxyDescriptor(typeof (IOne), new[] {typeof (ITwo), typeof (IOneTwo)});

            // Act
            var typeVisitor = new CollectingProxyDescriptorVisitor();

            proxyDescriptor.Accept(typeVisitor);

            // Assert
            Assert.That(typeVisitor.InterfaceTypes.Count, Is.EqualTo(4));
            Assert.That(typeVisitor.InterfaceTypes, Contains.Item(typeof (IBase)));
            Assert.That(typeVisitor.InterfaceTypes, Contains.Item(typeof (IOne)));
            Assert.That(typeVisitor.InterfaceTypes, Contains.Item(typeof (ITwo)));
            Assert.That(typeVisitor.InterfaceTypes, Contains.Item(typeof (IOneTwo)));
        }

        [Test]
        public void EqualsWithoutInterfacesTest()
        {
            // Arrange
            var firstProxyDescriptor = new InterfaceProxyDescriptor(typeof (IBase), Type.EmptyTypes);
            var secondProxyDescriptor = new InterfaceProxyDescriptor(typeof (IBase), Type.EmptyTypes);

            // Act
            var equals = firstProxyDescriptor.Equals(secondProxyDescriptor);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithSwappedInterfacesTest()
        {
            // Arrange
            var firstProxyDescriptor = new InterfaceProxyDescriptor(typeof (IBase), new[] {typeof (IOne), typeof (ITwo)});
            var secondProxyDescriptor = new InterfaceProxyDescriptor(typeof (IBase), new[] {typeof (ITwo), typeof (IOne)});

            // Act
            var equals = firstProxyDescriptor.Equals(secondProxyDescriptor);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithDeclaringInterfaceTest()
        {
            // Arrange
            var firstProxyDescriptor = new InterfaceProxyDescriptor(typeof (IOne), new[] {typeof (IBase)});
            var secondProxyDescriptor = new InterfaceProxyDescriptor(typeof (IOne), Type.EmptyTypes);

            // Act
            var equals = firstProxyDescriptor.Equals(secondProxyDescriptor);

            // Assert
            Assert.That(equals, Is.True);
        }
    }
}