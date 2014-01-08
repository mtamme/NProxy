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
        public void NewProxyFromInterfaceAndExtendsTest()
        {
            // Arrange
            // Act
            var activator = _proxyFactory.NewProxy<IFoo>()
                                         .Extends<Bar>()
                                         .Targets<Foo>();
            var proxy = activator.CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<IFoo>());
            Assert.DoesNotThrow(proxy.Bar);

            Assert.That(proxy, Is.InstanceOf<IBar>());
            var bar = activator.AdaptInstance<IBar>(proxy);

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void NewProxyFromAbstractClassAndExtendsTest()
        {
            // Arrange
            // Act
            var activator = _proxyFactory.NewProxy<FooBase>()
                                         .Extends<Bar>()
                                         .Targets<Foo>();
            var proxy = activator.CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<FooBase>());
            Assert.DoesNotThrow(proxy.Bar);

            Assert.That(proxy, Is.InstanceOf<IBar>());
            var bar = activator.AdaptInstance<IBar>(proxy);

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void NewProxyFromClassAndExtendsTest()
        {
            // Arrange
            // Act
            var activator = _proxyFactory.NewProxy<Foo>()
                                         .Extends<Bar>()
                                         .Targets<Foo>();
            var proxy = activator.CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Foo>());
            Assert.DoesNotThrow(proxy.Bar);

            Assert.That(proxy, Is.InstanceOf<IBar>());
            var bar = activator.AdaptInstance<IBar>(proxy);

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void NewProxyFromDelegateAndExtendsTest()
        {
            // Arrange
            // Act
            var activator = _proxyFactory.NewProxy<Action>()
                                         .Extends<Bar>()
                                         .Targets(() => { });
            var proxy = activator.CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Action>());
            Assert.DoesNotThrow(() => proxy());

            Assert.That(proxy.Target, Is.InstanceOf<IBar>());
            var bar = activator.AdaptInstance<IBar>(proxy);

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void NewProxyFromInterfaceAndImplementsTest()
        {
            // Arrange
            // Act
            var activator = _proxyFactory.NewProxy<IFoo>()
                                         .Implements<IBar>()
                                         .Targets<FooBar>();
            var proxy = activator.CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<IFoo>());
            Assert.DoesNotThrow(proxy.Bar);

            Assert.That(proxy, Is.InstanceOf<IBar>());
            var bar = activator.AdaptInstance<IBar>(proxy);

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void NewProxyFromAbstractClassAndImplementsTest()
        {
            // Arrange
            // Act
            var activator = _proxyFactory.NewProxy<FooBase>()
                                         .Implements<IBar>()
                                         .Targets<FooBar>();
            var proxy = activator.CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<FooBase>());
            Assert.DoesNotThrow(proxy.Bar);

            Assert.That(proxy, Is.InstanceOf<IBar>());
            var bar = activator.AdaptInstance<IBar>(proxy);

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void NewProxyFromClassAndImplementsTest()
        {
            // Arrange
            // Act
            var activator = _proxyFactory.NewProxy<Foo>()
                                         .Implements<IBar>()
                                         .Targets<FooBar>();
            var proxy = activator.CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Foo>());
            Assert.DoesNotThrow(proxy.Bar);

            Assert.That(proxy, Is.InstanceOf<IBar>());
            var bar = activator.AdaptInstance<IBar>(proxy);

            Assert.DoesNotThrow(bar.Foo);
        }

        [Test]
        public void NewProxyFromDelegateAndImplementsTest()
        {
            // Arrange
            // Act
            var activator = _proxyFactory.NewProxy<Action>()
                                         .Implements<IBar>()
                                         .Targets(() => { });
            var proxy = activator.CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Action>());
            Assert.DoesNotThrow(() => proxy());

            Assert.That(proxy.Target, Is.InstanceOf<IBar>());
            var bar = activator.AdaptInstance<IBar>(proxy);

            Assert.Throws<TargetException>(bar.Foo);
        }

        [Test]
        public void NewProxyFromInterfaceAndInvokesTest()
        {
            // Arrange
            var interceptor = new CountingInterceptor();

            // Act
            var proxy = _proxyFactory.NewProxy<IFoo>()
                                     .Invokes(interceptor)
                                     .Targets<Foo>()
                                     .CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<IFoo>());
            Assert.DoesNotThrow(proxy.Bar);
            Assert.That(interceptor.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void NewProxyFromAbstractClassAndInvokesTest()
        {
            // Arrange
            var interceptor = new CountingInterceptor();

            // Act
            var proxy = _proxyFactory.NewProxy<FooBase>()
                                     .Invokes(interceptor)
                                     .Targets<Foo>()
                                     .CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<FooBase>());
            Assert.DoesNotThrow(proxy.Bar);
            Assert.That(interceptor.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void NewProxyFromClassAndInvokesTest()
        {
            // Arrange
            var interceptor = new CountingInterceptor();

            // Act
            var proxy = _proxyFactory.NewProxy<Foo>()
                                     .Invokes(interceptor)
                                     .Targets<Foo>()
                                     .CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Foo>());
            Assert.DoesNotThrow(proxy.Bar);
            Assert.That(interceptor.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void NewProxyFromDelegateAndInvokesTest()
        {
            // Arrange
            var interceptor = new CountingInterceptor();

            // Act
            var proxy = _proxyFactory.NewProxy<Action>()
                                     .Invokes(interceptor)
                                     .Targets(() => { })
                                     .CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Action>());
            Assert.DoesNotThrow(() => proxy());
            Assert.That(interceptor.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void NewProxyFromInterfaceAndTargetsTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.NewProxy<IFoo>()
                                     .Targets<Foo>()
                                     .CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<IFoo>());
            Assert.DoesNotThrow(proxy.Bar);
        }

        [Test]
        public void NewProxyFromAbstractClassAndTargetsTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.NewProxy<FooBase>()
                                     .Targets<Foo>()
                                     .CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<FooBase>());
            Assert.DoesNotThrow(proxy.Bar);
        }

        [Test]
        public void NewProxyFromClassAndTargetsTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.NewProxy<Foo>()
                                     .Targets<Foo>()
                                     .CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Foo>());
            Assert.DoesNotThrow(proxy.Bar);
        }

        [Test]
        public void NewProxyFromDelegateAndTargetsTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.NewProxy<Action>()
                                     .Targets(() => { })
                                     .CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Action>());
            Assert.DoesNotThrow(() => proxy());
        }

        [Test]
        public void NewProxyFromInterfaceAndTargetsSelfTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.NewProxy<IFoo>()
                                     .TargetsSelf()
                                     .CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<IFoo>());
            Assert.Throws<TargetException>(proxy.Bar);
        }

        [Test]
        public void NewProxyFromAbstractClassAndTargetsSelfTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.NewProxy<FooBase>()
                                     .TargetsSelf()
                                     .CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<FooBase>());
            Assert.Throws<TargetException>(proxy.Bar);
        }

        [Test]
        public void NewProxyFromClassAndTargetsSelfTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.NewProxy<Foo>()
                                     .TargetsSelf()
                                     .CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Foo>());
            Assert.DoesNotThrow(proxy.Bar);
        }

        [Test]
        public void NewProxyFromDelegateAndTargetsSelfTest()
        {
            // Arrange
            // Act
            var proxy = _proxyFactory.NewProxy<Action>()
                                     .TargetsSelf()
                                     .CreateInstance();

            // Assert
            Assert.That(proxy, Is.InstanceOf<Action>());
            Assert.Throws<TargetException>(() => proxy());
        }

        [Test]
        public void NewProxyWithLazyInterceptorTest()
        {
            // Arrange
            var employee = _proxyFactory.NewProxy<IEmployee>()
                                        .Extends<LazyMixin>()
                                        .Targets<Employee>()
                                        .CreateInstance();

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