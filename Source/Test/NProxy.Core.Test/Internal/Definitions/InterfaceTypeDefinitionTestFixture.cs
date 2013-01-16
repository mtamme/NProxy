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
using NProxy.Core.Internal.Common;
using NProxy.Core.Internal.Definitions;
using NProxy.Core.Test.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal.Definitions
{
    [TestFixture]
    public sealed class InterfaceTypeDefinitionTestFixture
    {
        [Test]
        public void AddInterfaceTest()
        {
            // Arrange
            var typeDefinition = new InterfaceTypeDefinition(typeof (IOne));

            // Act
            typeDefinition.AddInterface(typeof (ITwo));
            typeDefinition.AddInterface(typeof (IOneTwo));

            // Assert
            var interfaceTypes = new List<Type>();
            var visitor = Visitor.Create<Type>(interfaceTypes.Add);

            typeDefinition.VisitInterfaces(visitor);

            Assert.That(interfaceTypes.Count, Is.EqualTo(4));
            Assert.That(interfaceTypes, Contains.Item(typeof (IBase)));
            Assert.That(interfaceTypes, Contains.Item(typeof (IOne)));
            Assert.That(interfaceTypes, Contains.Item(typeof (ITwo)));
            Assert.That(interfaceTypes, Contains.Item(typeof (IOneTwo)));
        }

        [Test]
        public void EqualsWithoutInterfacesTest()
        {
            // Arrange
            var firstTypeDefinition = new InterfaceTypeDefinition(typeof (IBase));
            var secondTypeDefinition = new InterfaceTypeDefinition(typeof (IBase));

            // Act
            var isEqual = firstTypeDefinition.Equals(secondTypeDefinition);

            // Assert
            Assert.That(isEqual, Is.True);
        }

        [Test]
        public void EqualsWithInterfacesTest()
        {
            // Arrange
            var firstTypeDefinition = new InterfaceTypeDefinition(typeof (IBase));

            firstTypeDefinition.AddInterface(typeof (IOne));
            firstTypeDefinition.AddInterface(typeof (ITwo));

            var secondTypeDefinition = new InterfaceTypeDefinition(typeof (IBase));

            secondTypeDefinition.AddInterface(typeof (ITwo));
            secondTypeDefinition.AddInterface(typeof (IOne));

            // Act
            var isEqual = firstTypeDefinition.Equals(secondTypeDefinition);

            // Assert
            Assert.That(isEqual, Is.True);
        }
    }
}