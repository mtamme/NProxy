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
using NProxy.Core.Test.Types;
using NUnit.Framework;

namespace NProxy.Core.Test
{
    [TestFixture]
    internal sealed class ProxyTestFixture
    {
        private ProxyFactory _proxyFactory;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _proxyFactory = new ProxyFactory();
        }

        [Test]
        public void CastInterfaceProxyTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IIntParameter>(new[] {typeof (IStringParameter)});
            var instance = proxy.CreateInstance(new TargetInvocationHandler(_ => null));

            // Act
            var value = proxy.Cast<IStringParameter>(instance);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void CastAbstractClassProxyTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IntParameterBase>(new[] {typeof (IStringParameter)});
            var instance = proxy.CreateInstance(new TargetInvocationHandler(_ => null));

            // Act
            var value = proxy.Cast<IStringParameter>(instance);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void CastClassProxyTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IntParameter>(new[] {typeof (IStringParameter)});
            var instance = proxy.CreateInstance(new TargetInvocationHandler(_ => null));

            // Act
            var value = proxy.Cast<IStringParameter>(instance);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void CastDelegateProxyTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<Action<int>>(new[] {typeof (IStringParameter)});
            var instance = proxy.CreateInstance(new TargetInvocationHandler(_ => null));

            // Act
            var value = proxy.Cast<IStringParameter>(instance);

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void CastInterfaceProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IIntParameter>(new[] {typeof (IStringParameter)});
            var instance = proxy.CreateInstance(new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidCastException>(() => proxy.Cast<IEnumParameter>(instance));
        }

        [Test]
        public void CastAbstractClassProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IntParameterBase>(new[] {typeof (IStringParameter)});
            var instance = proxy.CreateInstance(new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidCastException>(() => proxy.Cast<IEnumParameter>(instance));
        }

        [Test]
        public void CastClassProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IntParameter>(new[] {typeof (IStringParameter)});
            var instance = proxy.CreateInstance(new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidCastException>(() => proxy.Cast<IEnumParameter>(instance));
        }

        [Test]
        public void CastDelegateProxyToInvalidInterfaceTypeTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<Action<int>>(new[] {typeof (IStringParameter)});
            var instance = proxy.CreateInstance(new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidCastException>(() => proxy.Cast<IEnumParameter>(instance));
        }

        [Test]
        public void CastNonProxyTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<Action<int>>(new[] {typeof (IStringParameter)});
            var instance = new StringParameter();

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxy.Cast<IStringParameter>(instance));
        }
    }
}