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
using NProxy.Core.Interceptors;
using NProxy.Core.Test.Interceptors.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Interceptors
{
    [TestFixture]
    public sealed class ProxyFactoryTestFixture
    {
        private ProxyFactory _proxyFactory;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _proxyFactory = new ProxyFactory();
        }

        [Test]
        public void NewProxyFromInterfaceWithMixinTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.NewProxy<IVoidMethod>()
                .Extends<Mixin>()
                .Targets<VoidMethod>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<IVoidMethod>());
            Assert.DoesNotThrow(() => proxy.Method());

            Assert.That(proxy, Is.InstanceOf<IMixin>());
            var mixin = proxy.Cast<IMixin>();

            Assert.DoesNotThrow(() => mixin.Method());
        }

        [Test]
        public void NewProxyFromAbstractClassWithMixinTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.NewProxy<VoidMethodBase>()
                .Extends<Mixin>()
                .Targets<VoidMethod>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<VoidMethodBase>());
            Assert.DoesNotThrow(() => proxy.Method());

            Assert.That(proxy, Is.InstanceOf<IMixin>());
            var mixin = proxy.Cast<IMixin>();

            Assert.DoesNotThrow(() => mixin.Method());
        }

        [Test]
        public void NewProxyFromClassWithMixinTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.NewProxy<VoidMethod>()
                .Extends<Mixin>()
                .Targets<VoidMethod>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<VoidMethod>());
            Assert.DoesNotThrow(() => proxy.Method());

            Assert.That(proxy, Is.InstanceOf<IMixin>());
            var mixin = proxy.Cast<IMixin>();

            Assert.DoesNotThrow(() => mixin.Method());
        }

        [Test]
        public void NewProxyFromDelegateWithMixinTest()
        {
            // Arrange
            // Act
            Action proxy = _proxyFactory.NewProxy<Action>()
                .Extends<Mixin>()
                .Targets(() => {});

            // Assert
            Assert.That(proxy, Is.InstanceOf<Action>());
            Assert.DoesNotThrow(() => proxy());

            Assert.That(proxy.Target, Is.InstanceOf<IMixin>());
            var mixin = proxy.Cast<IMixin>();

            Assert.DoesNotThrow(() => mixin.Method());
        }

        [Test]
        public void NewProxyWithLazyInterceptorTest()
        {
            // Arrange
            var employee = _proxyFactory.NewProxy<IEmployee>()
                .Extends<LazyMixin>()
                .Targets<Employee>();

            // Act
            employee.Name = "Saturnus";

            // Assert
            Assert.That(employee, Is.InstanceOf<IEmployee>());
            Assert.That(employee.Name, Is.EqualTo("Saturnus"));

            Assert.That(employee, Is.InstanceOf<ILazy>());
            var lazy = (ILazy) employee;

            Assert.That(lazy.Loaded, Is.EqualTo(true));
        }
    }
}
