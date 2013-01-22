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
    [TestFixture(Ignore = true, IgnoreReason = "Ignore performance test cases by default")]
    public sealed class LinFuPerformanceTestFixture
    {
        private static readonly AssemblyName AssemblyName;

        static LinFuPerformanceTestFixture()
        {
            var type = typeof (LinFu.Proxy.ProxyFactory);

            AssemblyName = type.Assembly.GetName();
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Ensure all classes are loaded and initialized.
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new Standard());

            proxyFactory.CreateProxy<IStandard>(interceptor);
        }

        [TestCase(1000)]
        public void ProxyGenerationTest(int iterations)
        {
            var interceptor = new LinFuInterceptor(new Standard());
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                var proxyFactory = new LinFu.Proxy.ProxyFactory {Cache = new LinFuProxyCache()};

                stopwatch.Start();

                proxyFactory.CreateProxy<IStandard>(interceptor);

                stopwatch.Stop();
            }

            Report.Instance.Write(AssemblyName, Scenario.ProxyGeneration, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000)]
        public void ProxyGenerationWithGenericParameterTest(int iterations)
        {
            var interceptor = new LinFuInterceptor(new Generic());
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                var proxyFactory = new LinFu.Proxy.ProxyFactory {Cache = new LinFuProxyCache()};

                stopwatch.Start();

                proxyFactory.CreateProxy<IGeneric>(interceptor);

                stopwatch.Stop();
            }

            Report.Instance.Write(AssemblyName, Scenario.ProxyGenerationWithGenericParameter, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void ProxyInstantiationTest(int iterations)
        {
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new Standard());
            var stopwatch = new Stopwatch();

            proxyFactory.CreateProxy<IStandard>(interceptor);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy<IStandard>(interceptor);
            }

            stopwatch.Stop();

            Report.Instance.Write(AssemblyName, Scenario.ProxyInstantiation, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void ProxyInstantiationWithGenericParameterTest(int iterations)
        {
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new Generic());
            var stopwatch = new Stopwatch();

            proxyFactory.CreateProxy<IGeneric>(interceptor);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy<IGeneric>(interceptor);
            }

            stopwatch.Stop();

            Report.Instance.Write(AssemblyName, Scenario.ProxyInstantiationWithGenericParameter, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void MethodInvocationTest(int iterations)
        {
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new Standard());
            var proxy = proxyFactory.CreateProxy<IStandard>(interceptor);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            Report.Instance.Write(AssemblyName, Scenario.MethodInvocation, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void MethodInvocationWithGenericParameterTest(int iterations)
        {
            var proxyFactory = new LinFu.Proxy.ProxyFactory();
            var interceptor = new LinFuInterceptor(new Generic());
            var proxy = proxyFactory.CreateProxy<IGeneric>(interceptor);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            Report.Instance.Write(AssemblyName, Scenario.MethodInvocationWithGenericParameter, iterations, stopwatch.Elapsed);
        }
    }
}