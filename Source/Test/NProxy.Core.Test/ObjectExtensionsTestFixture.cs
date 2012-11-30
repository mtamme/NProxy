//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © 2012 Martin Tamme
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
        public void CastInterfaceProxyTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IIntParameter>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));
            var value = proxy.Cast<IStringParameter>();

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void CastAbstractClassProxyTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IntParameterBase>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));
            var value = proxy.Cast<IStringParameter>();

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void CastClassProxyTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IntParameter>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));
            var value = proxy.Cast<IStringParameter>();

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void CastDelegateProxyTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<Action<int>>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));
            var value = proxy.Cast<IStringParameter>();

            // Assert
            Assert.That(value, Is.Not.Null);
            Assert.That(value, Is.InstanceOf<IStringParameter>());
        }

        [Test]
        public void CastInterfaceProxyWithNotImplementedInterfaceTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IIntParameter>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidCastException>(() => proxy.Cast<IEnumParameter>());
        }

        [Test]
        public void CastAbstractClassProxyWithNotImplementedInterfaceTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IntParameterBase>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidCastException>(() => proxy.Cast<IEnumParameter>());
        }

        [Test]
        public void CastClassProxyWithNotImplementedInterfaceTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<IntParameter>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidCastException>(() => proxy.Cast<IEnumParameter>());
        }

        [Test]
        public void CastDelegateProxyWithNotImplementedInterfaceTest()
        {
            // Arrange
            var proxy = _proxyFactory.CreateProxy<Action<int>>(new[] {typeof (IStringParameter)}, new TargetInvocationHandler(_ => null));

            // Act
            // Assert
            Assert.Throws<InvalidCastException>(() => proxy.Cast<IEnumParameter>());
        }

        [Test]
        public void CastNonProxyTest()
        {
            // Arrange
            var proxy = new StringParameter();

            // Act
            // Assert
            Assert.Throws<InvalidOperationException>(() => proxy.Cast<IStringParameter>());
        }
    }
}
