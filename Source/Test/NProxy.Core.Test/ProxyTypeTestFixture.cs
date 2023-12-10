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
using NProxy.Core.Test.Types;
using NUnit.Framework;

namespace NProxy.Core.Test
{
    [TestFixture]
    internal sealed class ProxyTypeTestFixture
    {
        private ProxyTypeRegistry _proxyTypeRegistry;

        [OneTimeSetUp]
        public void SetUp()
        {
            _proxyTypeRegistry = new ProxyTypeRegistry();
        }

        [Test]
        public void AdaptProxyInterfaceProxyTest()
        {
            // Arrange
            var proxyType = _proxyTypeRegistry.GetProxyType<IIntParameter>(new[] {typeof (IStringParameter)});
            var proxy = proxyType.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            var value = proxyType.AdaptProxy<IStringParameter>(proxy);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyAbstractClassProxyTest()
        {
            // Arrange
            var proxyType = _proxyTypeRegistry.GetProxyType<IntParameterBase>(new[] {typeof (IStringParameter)});
            var proxy = proxyType.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            var value = proxyType.AdaptProxy<IStringParameter>(proxy);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyClassProxyTest()
        {
            // Arrange
            var proxyType = _proxyTypeRegistry.GetProxyType<IntParameter>(new[] {typeof (IStringParameter)});
            var proxy = proxyType.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            var value = proxyType.AdaptProxy<IStringParameter>(proxy);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyDelegateProxyTest()
        {
            // Arrange
            var proxyType = _proxyTypeRegistry.GetProxyType<Action<int>>(new[] {typeof (IStringParameter)});
            var proxy = proxyType.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            var value = proxyType.AdaptProxy<IStringParameter>(proxy);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyInterfaceProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxyType = _proxyTypeRegistry.GetProxyType<IIntParameter>(new[] {typeof (IStringParameter)});
            var proxy = proxyType.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyType.AdaptProxy<IEnumParameter>(proxy));
        }

        [Test]
        public void AdaptProxyAbstractClassProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxyType = _proxyTypeRegistry.GetProxyType<IntParameterBase>(new[] {typeof (IStringParameter)});
            var proxy = proxyType.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyType.AdaptProxy<IEnumParameter>(proxy));
        }

        [Test]
        public void AdaptProxyClassProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxyType = _proxyTypeRegistry.GetProxyType<IntParameter>(new[] {typeof (IStringParameter)});
            var proxy = proxyType.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyType.AdaptProxy<IEnumParameter>(proxy));
        }

        [Test]
        public void AdaptProxyDelegateProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxyType = _proxyTypeRegistry.GetProxyType<Action<int>>(new[] {typeof (IStringParameter)});
            var proxy = proxyType.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyType.AdaptProxy<IEnumParameter>(proxy));
        }

        [Test]
        public void AdaptProxyNonProxyTest()
        {
            // Arrange
            var proxyType = _proxyTypeRegistry.GetProxyType<Action<int>>(new[] {typeof (IStringParameter)});
            var proxy = new StringParameter();

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyType.AdaptProxy<IStringParameter>(proxy));
        }
    }
}