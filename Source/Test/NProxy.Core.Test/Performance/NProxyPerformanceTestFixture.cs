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
using System.Reflection;
using NProxy.Core.Test.Performance.Types;
using NUnit.Framework;

namespace NProxy.Core.Test.Performance
{
    [TestFixture(Ignore = true, IgnoreReason = "Ignore performance test cases by default")]
    public sealed class NProxyPerformanceTestFixture
    {
        private static readonly AssemblyName AssemblyName;

        static NProxyPerformanceTestFixture()
        {
            var type = typeof (ProxyFactory);

            AssemblyName = type.Assembly.GetName();
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Ensure all classes are loaded and initialized.
            var invocationHandler = new NProxyInvocationHandler(new Trivial());
            var proxyFactory = new ProxyFactory();

            proxyFactory.CreateProxy<ITrivial>(Type.EmptyTypes, invocationHandler);
        }

        [TestCase(1000)]
        public void ProxyGenerationTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new Trivial());
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                var proxyFactory = new ProxyFactory();

                stopwatch.Start();

                proxyFactory.CreateProxy<ITrivial>(Type.EmptyTypes, invocationHandler);

                stopwatch.Stop();
            }

            Report.Instance.WriteValues(AssemblyName, Scenario.ProxyGeneration, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000)]
        public void ProxyGenerationWithGenericParameterTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new Generic());
            var stopwatch = new Stopwatch();

            for (var i = 0; i < iterations; i++)
            {
                var proxyFactory = new ProxyFactory();

                stopwatch.Start();

                proxyFactory.CreateProxy<IGeneric>(Type.EmptyTypes, invocationHandler);

                stopwatch.Stop();
            }

            Report.Instance.WriteValues(AssemblyName, Scenario.ProxyGenerationWithGenericParameter, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void ProxyInstantiationTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new Trivial());
            var proxyFactory = new ProxyFactory();
            var stopwatch = new Stopwatch();

            proxyFactory.CreateProxy<ITrivial>(Type.EmptyTypes, invocationHandler);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy<ITrivial>(Type.EmptyTypes, invocationHandler);
            }

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.ProxyInstantiation, iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void ProxyInstantiationWithGenericParameterTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new Generic());
            var proxyFactory = new ProxyFactory();
            var stopwatch = new Stopwatch();

            proxyFactory.CreateProxy<IGeneric>(Type.EmptyTypes, invocationHandler);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy<IGeneric>(Type.EmptyTypes, invocationHandler);
            }

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.ProxyInstantiationWithGenericParameter, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void MethodInvocationTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new Trivial());
            var proxyFactory = new ProxyFactory();
            var proxy = proxyFactory.CreateProxy<ITrivial>(Type.EmptyTypes, invocationHandler);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.MethodInvocation, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void MethodInvocationWithGenericParameterTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new Generic());
            var proxyFactory = new ProxyFactory();
            var proxy = proxyFactory.CreateProxy<IGeneric>(Type.EmptyTypes, invocationHandler);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.MethodInvocationWithGenericParameter, iterations, stopwatch.Elapsed);
        }
    }
}