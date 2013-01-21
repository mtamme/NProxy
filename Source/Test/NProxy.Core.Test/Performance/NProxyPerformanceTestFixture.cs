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
    [TestFixture]
    [Category("Performance")]
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
            var invocationHandler = new NProxyInvocationHandler(new Method());
            var proxyFactory = new ProxyFactory();

            proxyFactory.CreateProxy<IMethod>(Type.EmptyTypes, invocationHandler);
        }

        [TestCase(1000000)]
        public void CreateProxyTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new Method());
            var proxyFactory = new ProxyFactory();
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            proxyFactory.CreateProxy<IMethod>(Type.EmptyTypes, invocationHandler);

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.CreateProxyFromUnknownType, 1, stopwatch.Elapsed);

            stopwatch.Reset();

            stopwatch.Start();

            proxyFactory.CreateProxy<IGenericMethod>(Type.EmptyTypes, invocationHandler);

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.CreateProxyFromUnknownTypeWithGenericMethod, 1, stopwatch.Elapsed);

            stopwatch.Reset();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy<IMethod>(Type.EmptyTypes, invocationHandler);
            }

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.CreateProxyFromKnownType, iterations, stopwatch.Elapsed);

            stopwatch.Reset();
            
            stopwatch.Start();
            
            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy<IGenericMethod>(Type.EmptyTypes, invocationHandler);
            }
            
            stopwatch.Stop();
            
            Report.Instance.WriteValues(AssemblyName, Scenario.CreateProxyFromKnownTypeWithGenericMethod, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void InvokeMethodTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new Method());
            var proxyFactory = new ProxyFactory();
            var proxy = proxyFactory.CreateProxy<IMethod>(Type.EmptyTypes, invocationHandler);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.InvokeMethod, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void InvokeGenericMethodTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new GenericMethod());
            var proxyFactory = new ProxyFactory();
            var proxy = proxyFactory.CreateProxy<IGenericMethod>(Type.EmptyTypes, invocationHandler);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.InvokeGenericMethod, iterations, stopwatch.Elapsed);
        }
    }
}