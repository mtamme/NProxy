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
using NProxy.Core.Internal.Generators;
using NProxy.Core.Internal.Definitions;
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

        private static IProxyFactory CreateProxyFactory(bool withCache)
        {
            var typeBuilderFactory = new ProxyTypeBuilderFactory(false);
            var typeProvider = new ProxyTypeGenerator(typeBuilderFactory, new DefaultInterceptionFilter());

            return withCache ? new ProxyFactory(new TypeCache<ITypeDefinition>(typeProvider)) : new ProxyFactory(typeProvider);
        }

        [Ignore]
        [TestCase(1000)]
        public void CreateProxyWithoutCacheTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new IntMethod());
            var proxyFactory = CreateProxyFactory(false);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy<IIntMethod>(Type.EmptyTypes, invocationHandler);
            }

            stopwatch.Stop();

            PerformanceSetUpFixture.Instance.WriteValues(AssemblyName, "CreateProxyWithoutCache", iterations, stopwatch.Elapsed);
        }

        [TestCase(1000000)]
        public void CreateProxyTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new IntMethod());
            var proxyFactory = CreateProxyFactory(true);
            var stopwatch = new Stopwatch();

            // Create proxy type.
            proxyFactory.CreateProxy<IIntMethod>(Type.EmptyTypes, invocationHandler);

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxyFactory.CreateProxy<IIntMethod>(Type.EmptyTypes, invocationHandler);
            }

            stopwatch.Stop();

            PerformanceSetUpFixture.Instance.WriteValues(AssemblyName, "CreateProxy", iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void InvokeIntMethodTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new IntMethod());
            var proxyFactory = CreateProxyFactory(true);
            var proxy = proxyFactory.CreateProxy<IIntMethod>(Type.EmptyTypes, invocationHandler);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            PerformanceSetUpFixture.Instance.WriteValues(AssemblyName, "InvokeIntMethod", iterations, stopwatch.Elapsed);
        }

        [TestCase(10000000)]
        public void InvokeGenericMethodTest(int iterations)
        {
            var invocationHandler = new NProxyInvocationHandler(new GenericMethod());
            var proxyFactory = CreateProxyFactory(true);
            var proxy = proxyFactory.CreateProxy<IGenericMethod>(Type.EmptyTypes, invocationHandler);
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            PerformanceSetUpFixture.Instance.WriteValues(AssemblyName, "InvokeGenericMethod", iterations, stopwatch.Elapsed);
        }
    }
}