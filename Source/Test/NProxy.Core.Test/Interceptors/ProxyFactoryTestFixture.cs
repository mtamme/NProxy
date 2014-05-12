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
            employee.Name = "Foo";

            // Assert
            Assert.That(employee, Is.InstanceOf<IEmployee>());
            Assert.That(employee.Name, Is.EqualTo("Foo"));

            Assert.That(employee, Is.InstanceOf<ILazy>());
            var lazy = (ILazy) employee;

            Assert.That(lazy.Loaded, Is.True);
        }
    }
}