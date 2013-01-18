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
using System.Diagnostics;
using NProxy.Core.Internal.Generators;
using NProxy.Core.Internal.Definitions;
using NProxy.Core.Test.Types;
using Castle.DynamicProxy;
using NUnit.Framework;

namespace NProxy.Core.Test
{
    [TestFixture]
    [Category("Performance")]
    public sealed class ProxyFactoryPerformanceTestFixture
    {
        private static IProxyFactory CreateProxyFactory(bool withCache)
        {
            var typeBuilderFactory = new ProxyTypeBuilderFactory(false);
            var typeProvider = new ProxyTypeGenerator(typeBuilderFactory, new DefaultInterceptionFilter());

            return withCache ? new ProxyFactory(new TypeCache<ITypeDefinition>(typeProvider)) : new ProxyFactory(typeProvider);
        }

        private static void ShowMetrics(int iterations, TimeSpan elapsedTime)
        {
            Console.WriteLine("Iterations:   {0}", iterations);
            Console.WriteLine("Elapsed Time: {0:0.000}ms", elapsedTime.TotalMilliseconds);

            var averageMicroseconds = (elapsedTime.TotalMilliseconds*1000)/iterations;

            Console.WriteLine("Average Time: {0:0.000}µs", averageMicroseconds);
        }

        #region NProxy.Core Tests

        [Test]
        public void CreateProxyWithoutCacheTest()
        {
            var invocationHandler = new TargetInvocationHandler(_ => new IntParameter());
            var proxyFactory = CreateProxyFactory(false);
            var stopwatch = new Stopwatch();
            var iteration = 0;

            stopwatch.Start();

            for (; iteration < 10000; iteration++)
            {
                proxyFactory.CreateProxy<IIntParameter>(Type.EmptyTypes, invocationHandler);
            }

            stopwatch.Stop();

            ShowMetrics(iteration, stopwatch.Elapsed);
        }

        [Test]
        public void CreateProxyWithCacheTest()
        {
            var invocationHandler = new TargetInvocationHandler(_ => new IntParameter());
            var proxyFactory = CreateProxyFactory(true);
            var stopwatch = new Stopwatch();
            var iteration = 0;

            // Create proxy type.
            proxyFactory.CreateProxy<IIntParameter>(Type.EmptyTypes, invocationHandler);

            stopwatch.Start();

            for (; iteration < 200000; iteration++)
            {
                proxyFactory.CreateProxy<IIntParameter>(Type.EmptyTypes, invocationHandler);
            }

            stopwatch.Stop();

            ShowMetrics(iteration, stopwatch.Elapsed);
        }

        [Test]
        public void InvokeMethodTest()
        {
            var invocationHandler = new TargetInvocationHandler(_ => new IntParameter());
            var proxyFactory = CreateProxyFactory(true);
            var proxy = proxyFactory.CreateProxy<IIntParameter>(Type.EmptyTypes, invocationHandler);
            var stopwatch = new Stopwatch();
            var iteration = 0;

            stopwatch.Start();

            for (; iteration < 10000000; iteration++)
            {
                proxy.Method(iteration);
            }

            stopwatch.Stop();

            ShowMetrics(iteration, stopwatch.Elapsed);
        }

        [Test]
        public void InvokeGenericMethodTest()
        {
            var invocationHandler = new TargetInvocationHandler(_ => new GenericParameter());
            var proxyFactory = CreateProxyFactory(true);
            var proxy = proxyFactory.CreateProxy<IGenericParameter>(Type.EmptyTypes, invocationHandler);
            var stopwatch = new Stopwatch();
            var iteration = 0;

            stopwatch.Start();

            for (; iteration < 10000000; iteration++)
            {
                proxy.Method(iteration);
            }

            stopwatch.Stop();

            ShowMetrics(iteration, stopwatch.Elapsed);
        }

        #endregion

        #region Castle.Core Tests

        [Test]
        public void CastleCreateProxyWithCacheTest()
        {
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleTargetInterceptor()};
            var target = new IntParameter();
            var stopwatch = new Stopwatch();
            var iteration = 0;

            // Create proxy type.
            proxyGenerator.CreateInterfaceProxyWithTarget(typeof (IIntParameter), target, interceptors);

            stopwatch.Start();

            for (; iteration < 200000; iteration++)
            {
                proxyGenerator.CreateInterfaceProxyWithTarget(typeof (IIntParameter), target, interceptors);
            }

            stopwatch.Stop();

            ShowMetrics(iteration, stopwatch.Elapsed);
        }

        [Test]
        public void CastleInvokeMethodTest()
        {
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleTargetInterceptor()};
            var target = new IntParameter();
            var proxy = (IIntParameter) proxyGenerator.CreateInterfaceProxyWithTarget(typeof (IIntParameter), target, interceptors);
            var stopwatch = new Stopwatch();
            var iteration = 0;

            stopwatch.Start();

            for (; iteration < 10000000; iteration++)
            {
                proxy.Method(iteration);
            }

            stopwatch.Stop();

            ShowMetrics(iteration, stopwatch.Elapsed);
        }

        [Test]
        public void CastleInvokeGenericMethodTest()
        {
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleTargetInterceptor()};
            var target = new GenericParameter();
            var proxy = (IGenericParameter) proxyGenerator.CreateInterfaceProxyWithTarget(typeof (IGenericParameter), target, interceptors);
            var stopwatch = new Stopwatch();
            var iteration = 0;

            stopwatch.Start();

            for (; iteration < 1000000; iteration++)
            {
                proxy.Method(iteration);
            }

            stopwatch.Stop();

            ShowMetrics(iteration, stopwatch.Elapsed);
        }

        #endregion
    }
}