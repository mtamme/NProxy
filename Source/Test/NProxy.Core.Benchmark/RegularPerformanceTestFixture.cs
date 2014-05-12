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

using System.Diagnostics;
using NProxy.Core.Benchmark.Reporting;
using NProxy.Core.Benchmark.Types;
using NUnit.Framework;

namespace NProxy.Core.Benchmark
{
    [TestFixture]
    public sealed class RegularPerformanceTestFixture
    {
        [TestCase(100000000)]
        public void MethodInvocationTest(int iterations)
        {
            var proxy = new StandardProxy(new Standard());
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            Report.Instance.Write("Regular", "n/a", Scenario.MethodInvocation, iterations, stopwatch.Elapsed);
        }

        [TestCase(100000000)]
        public void MethodInvocationWithGenericParameterTest(int iterations)
        {
            var proxy = new GenericProxy(new Generic());
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (var i = 0; i < iterations; i++)
            {
                proxy.Invoke(i);
            }

            stopwatch.Stop();

            Report.Instance.Write("Regular", "n/a", Scenario.MethodInvocationWithGenericParameter, iterations, stopwatch.Elapsed);
        }
    }
}