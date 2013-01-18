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
using NProxy.Core.Test.Performance.Types;
using Castle.DynamicProxy;
using NUnit.Framework;

namespace NProxy.Core.Test.Performance
{
    [TestFixture]
    [Category("Performance")]
    public sealed class CastlePerformanceTestFixture
    {
        private static void ShowMetrics(int iterations, TimeSpan elapsedTime)
        {
            Console.WriteLine("Iterations:   {0}", iterations);
            Console.WriteLine("Elapsed Time: {0:0.000}ms", elapsedTime.TotalMilliseconds);

            var averageMicroseconds = (elapsedTime.TotalMilliseconds*1000)/iterations;

            Console.WriteLine("Average Time: {0:0.000}µs", averageMicroseconds);
        }

        [Test]
        public void CreateProxyWithCacheTest()
        {
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new IntMethod();
            var stopwatch = new Stopwatch();
            var iteration = 0;

            // Create proxy type.
            proxyGenerator.CreateInterfaceProxyWithTarget(typeof (IIntMethod), target, interceptors);

            stopwatch.Start();

            for (; iteration < 200000; iteration++)
            {
                proxyGenerator.CreateInterfaceProxyWithTarget(typeof (IIntMethod), target, interceptors);
            }

            stopwatch.Stop();

            ShowMetrics(iteration, stopwatch.Elapsed);
        }

        [Test]
        public void InvokeMethodTest()
        {
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new IntMethod();
            var proxy = (IIntMethod) proxyGenerator.CreateInterfaceProxyWithTarget(typeof (IIntMethod), target, interceptors);
            var stopwatch = new Stopwatch();
            var iteration = 0;

            stopwatch.Start();

            for (; iteration < 10000000; iteration++)
            {
                proxy.Invoke(iteration);
            }

            stopwatch.Stop();

            ShowMetrics(iteration, stopwatch.Elapsed);
        }

        [Test]
        public void InvokeGenericMethodTest()
        {
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new GenericMethod();
            var proxy = (IGenericMethod) proxyGenerator.CreateInterfaceProxyWithTarget(typeof (IGenericMethod), target, interceptors);
            var stopwatch = new Stopwatch();
            var iteration = 0;

            stopwatch.Start();

            for (; iteration < 1000000; iteration++)
            {
                proxy.Invoke(iteration);
            }

            stopwatch.Stop();

            ShowMetrics(iteration, stopwatch.Elapsed);
        }
    }
}