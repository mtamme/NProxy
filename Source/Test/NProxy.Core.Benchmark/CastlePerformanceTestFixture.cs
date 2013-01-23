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
using Castle.DynamicProxy;
using NProxy.Core.Benchmark.Types;
using NUnit.Framework;

namespace NProxy.Core.Benchmark
{
    [TestFixture]
    public sealed class CastlePerformanceTestFixture
    {
        private static readonly AssemblyName AssemblyName;

        static CastlePerformanceTestFixture()
        {
            var type = typeof (ProxyGenerator);

            AssemblyName = type.Assembly.GetName();
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Ensure all classes are loaded and initialized.
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new Standard();

            proxyGenerator.CreateInterfaceProxyWithTarget<IStandard>(target, interceptors);
        }

        [TestCase(1000)]
        public void ProxyGenerationTest(int iterations)
        {
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new Standard();
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                var proxyGenerator = new ProxyGenerator();

                stopwatch.Start();

                proxyGenerator.CreateInterfaceProxyWithTarget<IStandard>(target, interceptors);

                stopwatch.Stop();
            }

            Report.Instance.Write(AssemblyName, Scenario.ProxyGeneration, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000)]
        public void ProxyGenerationWithGenericParameterTest(int iterations)
        {
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new Generic();
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                var proxyGenerator = new ProxyGenerator();

                stopwatch.Start();

                proxyGenerator.CreateInterfaceProxyWithTarget<IGeneric>(target, interceptors);

                stopwatch.Stop();
            }

            Report.Instance.Write(AssemblyName, Scenario.ProxyGenerationWithGenericParameter, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void ProxyInstantiationTest(int iterations)
        {
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new Standard();
            var stopwatch = new Stopwatch();

            proxyGenerator.CreateInterfaceProxyWithTarget<IStandard>(target, interceptors);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyGenerator.CreateInterfaceProxyWithTarget<IStandard>(target, interceptors);
            }

            stopwatch.Stop();

            Report.Instance.Write(AssemblyName, Scenario.ProxyInstantiation, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void ProxyInstantiationWithGenericParameterTest(int iterations)
        {
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new Generic();
            var stopwatch = new Stopwatch();

            proxyGenerator.CreateInterfaceProxyWithTarget<IGeneric>(target, interceptors);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyGenerator.CreateInterfaceProxyWithTarget<IGeneric>(target, interceptors);
            }

            stopwatch.Stop();

            Report.Instance.Write(AssemblyName, Scenario.ProxyInstantiationWithGenericParameter, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void MethodInvocationTest(int iterations)
        {
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new Standard();
            var proxy = proxyGenerator.CreateInterfaceProxyWithTarget<IStandard>(target, interceptors);
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
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new Generic();
            var proxy = proxyGenerator.CreateInterfaceProxyWithTarget<IGeneric>(target, interceptors);
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