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
using NProxy.Core.Internal.Templates;
using NProxy.Core.Test.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal.Templates
{
    [TestFixture]
    public sealed class InterfaceProxyTemplateTestFixture
    {
        [Test]
        public void AcceptVisitorTest()
        {
            // Arrange
            var proxyTemplate = new InterfaceProxyTemplate(typeof (IOne), new[] {typeof (ITwo), typeof (IOneTwo)});

            // Act
            var proxyTemplateVisitor = new CollectingProxyTemplateVisitor();

            proxyTemplate.AcceptVisitor(proxyTemplateVisitor);

            // Assert
            Assert.That(proxyTemplateVisitor.InterfaceTypes.Count, Is.EqualTo(4));
            Assert.That(proxyTemplateVisitor.InterfaceTypes, Contains.Item(typeof (IBase)));
            Assert.That(proxyTemplateVisitor.InterfaceTypes, Contains.Item(typeof (IOne)));
            Assert.That(proxyTemplateVisitor.InterfaceTypes, Contains.Item(typeof (ITwo)));
            Assert.That(proxyTemplateVisitor.InterfaceTypes, Contains.Item(typeof (IOneTwo)));

            Assert.That(proxyTemplateVisitor.ConstructorInfos.Count, Is.EqualTo(1));

            Assert.That(proxyTemplateVisitor.EventInfos.Count, Is.EqualTo(3));

            Assert.That(proxyTemplateVisitor.PropertyInfos.Count, Is.EqualTo(6));

            Assert.That(proxyTemplateVisitor.MethodInfos.Count, Is.EqualTo(7));
        }

        [Test]
        public void EqualsWithoutInterfacesTest()
        {
            // Arrange
            var firstProxyTemplate = new InterfaceProxyTemplate(typeof (IBase), Type.EmptyTypes);
            var secondProxyTemplate = new InterfaceProxyTemplate(typeof (IBase), Type.EmptyTypes);

            // Act
            var equals = firstProxyTemplate.Equals(secondProxyTemplate);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithSwappedInterfacesTest()
        {
            // Arrange
            var firstProxyTemplate = new InterfaceProxyTemplate(typeof (IBase), new[] {typeof (IOne), typeof (ITwo)});
            var secondProxyTemplate = new InterfaceProxyTemplate(typeof (IBase), new[] {typeof (ITwo), typeof (IOne)});

            // Act
            var equals = firstProxyTemplate.Equals(secondProxyTemplate);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithDeclaringInterfaceTest()
        {
            // Arrange
            var firstProxyTemplate = new InterfaceProxyTemplate(typeof (IOne), new[] {typeof (IBase)});
            var secondProxyTemplate = new InterfaceProxyTemplate(typeof (IOne), Type.EmptyTypes);

            // Act
            var equals = firstProxyTemplate.Equals(secondProxyTemplate);

            // Assert
            Assert.That(equals, Is.True);
        }
    }
}