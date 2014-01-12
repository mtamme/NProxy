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
using System.Reflection;
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
        public void CreateProxyFromInterfaceAndExtendWithTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IFoo>()
                                     .ExtendWith<Bar>()
                                     .Target<Foo>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<IFoo>());
            Assert.DoesNotThrow(proxy.Bar);

            Assert.That(proxy, Is.InstanceOf<IBar>());
            var bar = (IBar) proxy;

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void CreateProxyFromAbstractClassAndExtendWithTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<FooBase>()
                                     .ExtendWith<Bar>()
                                     .Target<Foo>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<FooBase>());
            Assert.DoesNotThrow(proxy.Bar);

            Assert.That(proxy, Is.InstanceOf<IBar>());
            var bar = (IBar) proxy;

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void CreateProxyFromClassAndExtendWithTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<Foo>()
                                     .ExtendWith<Bar>()
                                     .Target<Foo>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Foo>());
            Assert.DoesNotThrow(proxy.Bar);

            Assert.That(proxy, Is.InstanceOf<IBar>());
            var bar = (IBar) proxy;

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void CreateProxyFromDelegateAndExtendWithTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<Action>()
                                     .ExtendWith<Bar>()
                                     .Target(() => { });

            // Assert
            Assert.That(proxy, Is.InstanceOf<Action>());
            Assert.DoesNotThrow(() => proxy());

            Assert.That(proxy.Target, Is.InstanceOf<IBar>());
            var bar = (IBar) proxy.Target;

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void CreateProxyFromInterfaceAndImplementTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IFoo>()
                                     .Implement<IBar>()
                                     .Target<FooBar>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<IFoo>());
            Assert.DoesNotThrow(proxy.Bar);

            Assert.That(proxy, Is.InstanceOf<IBar>());
            var bar = (IBar) proxy;

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void CreateProxyFromAbstractClassAndImplementTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<FooBase>()
                                     .Implement<IBar>()
                                     .Target<FooBar>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<FooBase>());
            Assert.DoesNotThrow(proxy.Bar);

            Assert.That(proxy, Is.InstanceOf<IBar>());
            var bar = (IBar) proxy;

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void CreateProxyFromClassAndImplementTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<Foo>()
                                     .Implement<IBar>()
                                     .Target<FooBar>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Foo>());
            Assert.DoesNotThrow(proxy.Bar);

            Assert.That(proxy, Is.InstanceOf<IBar>());
            var bar = (IBar) proxy;

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void CreateProxyFromDelegateAndImplementTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<Action>()
                                     .Implement<IBar>()
                                     .Target(() => { });

            // Assert
            Assert.That(proxy, Is.InstanceOf<Action>());
            Assert.DoesNotThrow(() => proxy());

            Assert.That(proxy.Target, Is.InstanceOf<IBar>());
            var bar = (IBar) proxy.Target;

            Assert.Throws<TargetException>(bar.Foo);
        }

        [Test]
        public void CreateProxyFromInterfaceAndInterceptByTest()
        {
            // Arrange
            var interceptor = new CountingInterceptor();

            // Act
            var proxy = _proxyFactory.CreateProxy<IFoo>()
                                     .InterceptBy(interceptor)
                                     .Target<Foo>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<IFoo>());
            Assert.DoesNotThrow(proxy.Bar);
            Assert.That(interceptor.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void CreateProxyFromAbstractClassAndInterceptByTest()
        {
            // Arrange
            var interceptor = new CountingInterceptor();

            // Act
            var proxy = _proxyFactory.CreateProxy<FooBase>()
                                     .InterceptBy(interceptor)
                                     .Target<Foo>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<FooBase>());
            Assert.DoesNotThrow(proxy.Bar);
            Assert.That(interceptor.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void CreateProxyFromClassAndInterceptByTest()
        {
            // Arrange
            var interceptor = new CountingInterceptor();

            // Act
            var proxy = _proxyFactory.CreateProxy<Foo>()
                                     .InterceptBy(interceptor)
                                     .Target<Foo>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Foo>());
            Assert.DoesNotThrow(proxy.Bar);
            Assert.That(interceptor.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void CreateProxyFromDelegateAndInterceptByTest()
        {
            // Arrange
            var interceptor = new CountingInterceptor();

            // Act
            var proxy = _proxyFactory.CreateProxy<Action>()
                                     .InterceptBy(interceptor)
                                     .Target(() => { });

            // Assert
            Assert.That(proxy, Is.InstanceOf<Action>());
            Assert.DoesNotThrow(() => proxy());
            Assert.That(interceptor.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void CreateProxyFromInterfaceAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IFoo>()
                                     .Target<Foo>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<IFoo>());
            Assert.DoesNotThrow(proxy.Bar);
        }

        [Test]
        public void CreateProxyFromAbstractClassAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<FooBase>()
                                     .Target<Foo>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<FooBase>());
            Assert.DoesNotThrow(proxy.Bar);
        }

        [Test]
        public void CreateProxyFromClassAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<Foo>()
                                     .Target<Foo>();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Foo>());
            Assert.DoesNotThrow(proxy.Bar);
        }

        [Test]
        public void CreateProxyFromDelegateAndTargetTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<Action>()
                                     .Target(() => { });

            // Assert
            Assert.That(proxy, Is.InstanceOf<Action>());
            Assert.DoesNotThrow(() => proxy());
        }

        [Test]
        public void CreateProxyFromInterfaceAndTargetBaseTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<IFoo>()
                                     .TargetBase();

            // Assert
            Assert.That(proxy, Is.InstanceOf<IFoo>());
            Assert.Throws<TargetException>(proxy.Bar);
        }

        [Test]
        public void CreateProxyFromAbstractClassAndTargetBaseTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<FooBase>()
                                     .TargetBase();

            // Assert
            Assert.That(proxy, Is.InstanceOf<FooBase>());
            Assert.Throws<TargetException>(proxy.Bar);
        }

        [Test]
        public void CreateProxyFromClassAndTargetBaseTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<Foo>()
                                     .TargetBase();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Foo>());
            Assert.DoesNotThrow(proxy.Bar);
        }

        [Test]
        public void CreateProxyFromDelegateAndTargetBaseTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.CreateProxy<Action>()
                                     .TargetBase();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Action>());
            Assert.Throws<TargetException>(() => proxy());
        }

        [Test]
        public void CreateProxyWithLazyInterceptorTest()
        {
            // Arrange
            var employee = _proxyFactory.CreateProxy<IEmployee>()
                                        .ExtendWith<LazyMixin>()
                                        .Target<Employee>();

            // Act
            employee.Name = "Saturnus";

            // Assert
            Assert.That(employee, Is.InstanceOf<IEmployee>());
            Assert.That(employee.Name, Is.EqualTo("Saturnus"));

            Assert.That(employee, Is.InstanceOf<ILazy>());
            var lazy = (ILazy) employee;

            Assert.That(lazy.Loaded, Is.True);
        }
    }
}