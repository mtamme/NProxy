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
using Castle.DynamicProxy;
using NUnit.Framework;

namespace NProxy.Core.Test.Performance
{
    [TestFixture]
    [Category("Performance")]
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
            var target = new Method();

            proxyGenerator.CreateInterfaceProxyWithTarget<IMethod>(target, interceptors);
        }

        [TestCase(1000000)]
        public void CreateProxyTest(int iterations)
        {
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new Method();
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            proxyGenerator.CreateInterfaceProxyWithTarget<IMethod>(target, interceptors);

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.CreateProxyFromUnknownType, 1, stopwatch.Elapsed);

            stopwatch.Reset();

            var newTarget = new GenericMethod();

            stopwatch.Start();

            proxyGenerator.CreateInterfaceProxyWithTarget<IGenericMethod>(newTarget, interceptors);

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.CreateProxyFromUnknownTypeWithGenericMethod, 1, stopwatch.Elapsed);

            stopwatch.Reset();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyGenerator.CreateInterfaceProxyWithTarget<IMethod>(target, interceptors);
            }

            stopwatch.Stop();

            Report.Instance.WriteValues(AssemblyName, Scenario.CreateProxyFromKnownType, iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void InvokeMethodTest(int iterations)
        {
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new Method();
            var proxy = proxyGenerator.CreateInterfaceProxyWithTarget<IMethod>(target, interceptors);
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
            var proxyGenerator = new ProxyGenerator();
            var interceptors = new IInterceptor[] {new CastleInterceptor()};
            var target = new GenericMethod();
            var proxy = proxyGenerator.CreateInterfaceProxyWithTarget<IGenericMethod>(target, interceptors);
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