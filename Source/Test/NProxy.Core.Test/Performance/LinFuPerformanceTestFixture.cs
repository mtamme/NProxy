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

using System.Diagnostics;
using System.Reflection;
using NProxy.Core.Test.Performance.Types;
using LinFu.Proxy.Interfaces;
using NUnit.Framework;

namespace NProxy.Core.Test.Performance
{
    [TestFixture]
    [Category("Performance")]
    public sealed class LinFuPerformanceTestFixture
    {
        private static readonly AssemblyName AssemblyName;
        
        static LinFuPerformanceTestFixture()
        {
            var type = typeof (LinFu.Proxy.ProxyFactory);
            
            AssemblyName = type.Assembly.GetName();
        }

        [TestCase(1000000)]
        public void CreateProxyTest(int iterations)
        {
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new IntMethod());
            var stopwatch = new Stopwatch();

            // Create proxy type.
            proxyFactory.CreateProxy<IIntMethod>(interceptor);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy<IIntMethod>(interceptor);
            }

            stopwatch.Stop();

            PerformanceSetUpFixture.Instance.WriteMetrics(AssemblyName, "CreateProxy", iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void InvokeIntMethodTest(int iterations)
        {
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new IntMethod());
            var proxy = proxyFactory.CreateProxy<IIntMethod>(interceptor);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            PerformanceSetUpFixture.Instance.WriteMetrics(AssemblyName, "InvokeIntMethod", iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void InvokeGenericMethodTest(int iterations)
        {
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new GenericMethod());
            var proxy = proxyFactory.CreateProxy<IGenericMethod>(interceptor);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            PerformanceSetUpFixture.Instance.WriteMetrics(AssemblyName, "InvokeGenericMethod", iterations, stopwatch.Elapsed);
        }
    }
}