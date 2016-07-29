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
    internal sealed class ProxyTemplateTestFixture
    {
        private ProxyFactory _proxyFactory;

        [OneTimeSetUp]
        public void SetUp()
        {
            _proxyFactory = new ProxyFactory();
        }

        [Test]
        public void AdaptProxyInterfaceProxyTest()
        {
            // Arrange
            var proxyTemplate = _proxyFactory.GetProxyTemplate<IIntParameter>(new[] {typeof (IStringParameter)});
            var proxy = proxyTemplate.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            var value = proxyTemplate.AdaptProxy<IStringParameter>(proxy);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyAbstractClassProxyTest()
        {
            // Arrange
            var proxyTemplate = _proxyFactory.GetProxyTemplate<IntParameterBase>(new[] {typeof (IStringParameter)});
            var proxy = proxyTemplate.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            var value = proxyTemplate.AdaptProxy<IStringParameter>(proxy);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyClassProxyTest()
        {
            // Arrange
            var proxyTemplate = _proxyFactory.GetProxyTemplate<IntParameter>(new[] {typeof (IStringParameter)});
            var proxy = proxyTemplate.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            var value = proxyTemplate.AdaptProxy<IStringParameter>(proxy);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyDelegateProxyTest()
        {
            // Arrange
            var proxyTemplate = _proxyFactory.GetProxyTemplate<Action<int>>(new[] {typeof (IStringParameter)});
            var proxy = proxyTemplate.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            var value = proxyTemplate.AdaptProxy<IStringParameter>(proxy);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyInterfaceProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxyTemplate = _proxyFactory.GetProxyTemplate<IIntParameter>(new[] {typeof (IStringParameter)});
            var proxy = proxyTemplate.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyTemplate.AdaptProxy<IEnumParameter>(proxy));
        }

        [Test]
        public void AdaptProxyAbstractClassProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxyTemplate = _proxyFactory.GetProxyTemplate<IntParameterBase>(new[] {typeof (IStringParameter)});
            var proxy = proxyTemplate.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyTemplate.AdaptProxy<IEnumParameter>(proxy));
        }

        [Test]
        public void AdaptProxyClassProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxyTemplate = _proxyFactory.GetProxyTemplate<IntParameter>(new[] {typeof (IStringParameter)});
            var proxy = proxyTemplate.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyTemplate.AdaptProxy<IEnumParameter>(proxy));
        }

        [Test]
        public void AdaptProxyDelegateProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxyTemplate = _proxyFactory.GetProxyTemplate<Action<int>>(new[] {typeof (IStringParameter)});
            var proxy = proxyTemplate.CreateProxy(new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyTemplate.AdaptProxy<IEnumParameter>(proxy));
        }

        [Test]
        public void AdaptProxyNonProxyTest()
        {
            // Arrange
            var proxyTemplate = _proxyFactory.GetProxyTemplate<Action<int>>(new[] {typeof (IStringParameter)});
            var proxy = new StringParameter();

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxyTemplate.AdaptProxy<IStringParameter>(proxy));
        }
    }
}