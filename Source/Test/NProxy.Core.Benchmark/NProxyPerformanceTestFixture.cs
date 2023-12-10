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
using System.Diagnostics;
using System.Reflection;
using NProxy.Core.Benchmark.Reporting;
using NProxy.Core.Benchmark.Types;
using NUnit.Framework;

namespace NProxy.Core.Benchmark
{
    [Category("Benchmark")]
    [TestFixture]
    public sealed class NProxyPerformanceTestFixture
    {
        private static readonly AssemblyName AssemblyName;

        static NProxyPerformanceTestFixture()
        {
            var type = typeof (ProxyTypeRegistry);

            AssemblyName = type.Assembly.GetName();
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            // Ensure all classes are loaded and initialized.
            var invocationHandler = new NProxyInvocationHandler(new Standard());
            var proxyTypeRegistry = new ProxyTypeRegistry();

            proxyTypeRegistry.CreateProxy<IStandard>(Type.EmptyTypes, invocationHandler);
        }

        [TestCase(1000)]
        public void ProxyGenerationTest(int iterations)
        {
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                var proxyTypeRegistry = new ProxyTypeRegistry();

                stopwatch.Start();

                proxyTypeRegistry.GetProxyType<IStandard>(Type.EmptyTypes);

                stopwatch.Stop();
            }

            Report.Instance.Write(AssemblyName, Scenario.ProxyGeneration, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000)]
        public void ProxyGenerationWithGenericParameterTest(int iterations)
        {
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                var proxyTypeRegistry = new ProxyTypeRegistry();

                stopwatch.Start();

                proxyTypeRegistry.GetProxyType<IGeneric>(Type.EmptyTypes);

                stopwatch.Stop();
            }

            Report.Instance.Write(AssemblyName, Scenario.ProxyGenerationWithGenericParameter, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void ProxyInstantiationTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new Standard());
            var proxyFactory = new ProxyTypeRegistry();
            var stopwatch = new Stopwatch();
            var proxyTypeRegistry = proxyFactory.GetProxyType<IStandard>(Type.EmptyTypes);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyTypeRegistry.CreateProxy(invocationHandler);
            }

            stopwatch.Stop();

            Report.Instance.Write(AssemblyName, Scenario.ProxyInstantiation, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void ProxyInstantiationWithGenericParameterTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new Generic());
            var proxyTypeRegistry = new ProxyTypeRegistry();
            var stopwatch = new Stopwatch();
            var proxyType = proxyTypeRegistry.GetProxyType<IGeneric>(Type.EmptyTypes);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyType.CreateProxy(invocationHandler);
            }

            stopwatch.Stop();

            Report.Instance.Write(AssemblyName, Scenario.ProxyInstantiationWithGenericParameter, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void MethodInvocationTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new Standard());
            var proxyTypeRegistry = new ProxyTypeRegistry();
            var proxy = proxyTypeRegistry.CreateProxy<IStandard>(Type.EmptyTypes, invocationHandler);
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
            var invocationHandler = new NProxyInvocationHandler(new Generic());
            var proxyTypeRegistry = new ProxyTypeRegistry();
            var proxy = proxyTypeRegistry.CreateProxy<IGeneric>(Type.EmptyTypes, invocationHandler);
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