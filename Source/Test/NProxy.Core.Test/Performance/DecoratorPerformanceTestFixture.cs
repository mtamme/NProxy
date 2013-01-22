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
using NUnit.Framework;

namespace NProxy.Core.Test.Performance
{
    [TestFixture]
    [Category("Performance")]
    public class DecoratorPerformanceTestFixture
    {
        [TestCase(10000000)]
        public void ProxyGenerationTest(int iterations)
        {
            var target = new Trivial();
            var stopwatch = new Stopwatch();
            
            for (var i = 0; i < iterations; i++)
            {
                stopwatch.Start();

                new TrivialDecorator(target);
                
                stopwatch.Stop();
            }
            
            Report.Instance.WriteValues("Decorator", "n/a", Scenario.ProxyGeneration, iterations, stopwatch.Elapsed);
        }
        
        [TestCase(10000000)]
        public void ProxyGenerationWithGenericParameterTest(int iterations)
        {
            var target = new Generic();
            var stopwatch = new Stopwatch();
            
            for (var i = 0; i < iterations; i++)
            {
                stopwatch.Start();
                
                new GenericDecorator(target);
                
                stopwatch.Stop();
            }
            
            Report.Instance.WriteValues("Decorator", "n/a", Scenario.ProxyGenerationWithGenericParameter, iterations, stopwatch.Elapsed);
        }
        
        [TestCase(10000000)]
        public void ProxyInstantiationTest(int iterations)
        {
            var target = new Trivial();
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();
            
            for (var i = 0; i < iterations; i++)
            {
                new TrivialDecorator(target);
            }
            
            stopwatch.Stop();
            
            Report.Instance.WriteValues("Decorator", "n/a", Scenario.ProxyInstantiation, iterations, stopwatch.Elapsed);
        }
        
        [TestCase(10000000)]
        public void ProxyInstantiationWithGenericParameterTest(int iterations)
        {
            var target = new Generic();
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();
            
            for (var i = 0; i < iterations; i++)
            {
                new GenericDecorator(target);
            }
            
            stopwatch.Stop();
            
            Report.Instance.WriteValues("Decorator", "n/a", Scenario.ProxyInstantiationWithGenericParameter, iterations, stopwatch.Elapsed);
        }
        
        [TestCase(10000000)]
        public void MethodInvocationTest(int iterations)
        {
            var proxy = new TrivialDecorator(new Trivial());
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();
            
            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }
            
            stopwatch.Stop();
            
            Report.Instance.WriteValues("Decorator", "n/a", Scenario.MethodInvocation, iterations, stopwatch.Elapsed);
        }
        
        [TestCase(10000000)]
        public void MethodInvocationWithGenericParameterTest(int iterations)
        {
            var proxy = new GenericDecorator(new Generic());
            var stopwatch = new Stopwatch();
            
            stopwatch.Start();
            
            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }
            
            stopwatch.Stop();
            
            Report.Instance.WriteValues("Decorator", "n/a", Scenario.MethodInvocationWithGenericParameter, iterations, stopwatch.Elapsed);
        }
    }
}