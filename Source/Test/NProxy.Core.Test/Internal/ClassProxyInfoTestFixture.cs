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
using NProxy.Core.Internal;
using NProxy.Core.Test.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal
{
    [TestFixture]
    public sealed class ClassProxyInfoTestFixture
    {
        [Test]
        public void AcceptVisitorTest()
        {
            // Arrange
            var proxyInfo = new ClassProxyInfo(typeof (Other), new[] {typeof (IOne), typeof (ITwo), typeof (IOneTwo)});

            // Act
            var proxyInfoVisitor = new CollectingProxyInfoVisitor();

            proxyInfo.AcceptVisitor(proxyInfoVisitor);

            // Assert
            Assert.That(proxyInfo.DeclaringType, Is.EqualTo(typeof (Other)));
            Assert.That(proxyInfo.ParentType, Is.EqualTo(typeof (Other)));
            Assert.That(proxyInfo.ImplementedInterfaces.Count(), Is.EqualTo(5));
            Assert.That(proxyInfo.ImplementedInterfaces, Contains.Item(typeof (IOther)));
            Assert.That(proxyInfo.ImplementedInterfaces, Contains.Item(typeof (IBase)));
            Assert.That(proxyInfo.ImplementedInterfaces, Contains.Item(typeof (IOne)));
            Assert.That(proxyInfo.ImplementedInterfaces, Contains.Item(typeof (ITwo)));
            Assert.That(proxyInfo.ImplementedInterfaces, Contains.Item(typeof (IOneTwo)));
            Assert.That(proxyInfoVisitor.InterfaceTypes.Count, Is.EqualTo(4));
            Assert.That(proxyInfoVisitor.InterfaceTypes, Contains.Item(typeof (IBase)));
            Assert.That(proxyInfoVisitor.InterfaceTypes, Contains.Item(typeof (IOne)));
            Assert.That(proxyInfoVisitor.InterfaceTypes, Contains.Item(typeof (ITwo)));
            Assert.That(proxyInfoVisitor.InterfaceTypes, Contains.Item(typeof (IOneTwo)));

            Assert.That(proxyInfoVisitor.ConstructorInfos.Count, Is.EqualTo(1));

            Assert.That(proxyInfoVisitor.EventInfos.Count, Is.EqualTo(3));

            Assert.That(proxyInfoVisitor.PropertyInfos.Count, Is.EqualTo(6));

            Assert.That(proxyInfoVisitor.MethodInfos.Count, Is.EqualTo(7));
        }

        [Test]
        public void EqualsWithoutInterfacesTest()
        {
            // Arrange
            var firstProxyInfo = new ClassProxyInfo(typeof (Other), Type.EmptyTypes);
            var secondProxyInfo = new ClassProxyInfo(typeof (Other), Type.EmptyTypes);

            // Act
            var equals = firstProxyInfo.Equals(secondProxyInfo);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithSwappedInterfacesTest()
        {
            // Arrange
            var firstProxyInfo = new ClassProxyInfo(typeof (Other), new[] {typeof (IOne), typeof (ITwo)});
            var secondProxyInfo = new ClassProxyInfo(typeof (Other), new[] {typeof (ITwo), typeof (IOne)});

            // Act
            var equals = firstProxyInfo.Equals(secondProxyInfo);

            // Assert
            Assert.That(equals, Is.True);
        }

        [Test]
        public void EqualsWithDeclaringInterfaceTest()
        {
            // Arrange
            var firstProxyInfo = new ClassProxyInfo(typeof (Other), new[] {typeof (IOther)});
            var secondProxyInfo = new ClassProxyInfo(typeof (Other), Type.EmptyTypes);

            // Act
            var equals = firstProxyInfo.Equals(secondProxyInfo);

            // Assert
            Assert.That(equals, Is.True);
        }
    }
}