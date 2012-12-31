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
using NProxy.Core.Test.Common;
using NProxy.Core.Test.Common.Types;
using NUnit.Framework;

namespace NProxy.Core.Test
{
    [TestFixture]
    internal sealed class ObjectExtensionsTestFixture
    {
        private ProxyFactory _proxyFactory;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _proxyFactory = new ProxyFactory();
        }

        [Test]
        public void AdaptProxyInterfaceProxyTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IIntParameter>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));

            // Act
            var value = proxy.AdaptProxy<IStringParameter>();

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyAbstractClassProxyTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IntParameterBase>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));

            // Act
            var value = proxy.AdaptProxy<IStringParameter>();

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyClassProxyTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IntParameter>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));

            // Act
            var value = proxy.AdaptProxy<IStringParameter>();

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyDelegateProxyTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<Action<int>>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));

            // Act
            var value = proxy.AdaptProxy<IStringParameter>();

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void AdaptProxyInterfaceProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IIntParameter>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidCastException>(() => proxy.AdaptProxy<IEnumParameter>());
        }

        [Test]
        public void AdaptProxyAbstractClassProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IntParameterBase>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidCastException>(() => proxy.AdaptProxy<IEnumParameter>());
        }

        [Test]
        public void AdaptProxyClassProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IntParameter>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidCastException>(() => proxy.AdaptProxy<IEnumParameter>());
        }

        [Test]
        public void AdaptProxyDelegateProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<Action<int>>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidCastException>(() => proxy.AdaptProxy<IEnumParameter>());
        }

        [Test]
        public void AdaptProxyNonProxyTest()
        {
            // Arrange
            var proxy = new StringParameter();

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxy.AdaptProxy<IStringParameter>());
        }
    }
}
