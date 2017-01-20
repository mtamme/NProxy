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
using System.Collections.Generic;
using System.Reflection;
using NProxy.Core.Test.Types;
using NUnit.Framework;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NProxy.Core.Test
{
    [TestFixture]
    public sealed class MethodInfoTestFixture
    {
        public class Target
        {
            public int Sum(int a, int b)
            {
                return a + b;
            }
        }

        [OneTimeSetUp]
        public void SetUp()
        {
        }

        [OneTimeTearDown]
        public void TearDown()
        {
        }

        [Test]
        public void TestMethodInfo()
        {
            var methodInfo = typeof(Target).GetMethod("Sum");

            var instance = new Target();
            var result = methodInfo.Invoke(instance, new object[] { 1, 2 });

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var max = 20000;
            var _params = new object[] { 1, 2 };
            for (var i = 0; i < max; i++)
            {
                result = methodInfo.Invoke(instance, _params);
            }
            watch.Stop();

            Console.WriteLine($"Time: {(decimal)watch.ElapsedTicks / (decimal)TimeSpan.TicksPerMillisecond} ms");
        }

        public delegate int SumDelegate(int a, int b);

        [Test]
        public void TestDynamicDelegate()
        {
            var methodInfo = typeof(Target).GetMethod("Sum");

            var instance = new Target();

            var d = methodInfo.CreateDelegate(typeof(SumDelegate), instance);

            var result = d.DynamicInvoke(1, 2);

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var max = 20000;
            var _params = new object[] { 1, 2 };
            for (var i = 0; i < max; i++)
            {
                result = d.DynamicInvoke(_params);
            }
            watch.Stop();

            Console.WriteLine($"Time: {(decimal)watch.ElapsedTicks / (decimal)TimeSpan.TicksPerMillisecond} ms");
        }

        //[Test]
        //public void TestDynamicCastDelegate()
        //{
        //    //var methodInfo = typeof(Target).GetMethod("Sum");

        //    //var instance = new Target();

        //    //Delegate d = methodInfo.CreateDelegate(typeof(SumDelegate), instance);

        //    //var result = d((dynamic)1, (dynamic)2);

        //    //var watch = new System.Diagnostics.Stopwatch();
        //    //watch.Start();
        //    //var max = 20000;
        //    //var _params = new object[] { 1, 2 };
        //    //for (var i = 0; i < max; i++)
        //    //{
        //    //    result = d.DynamicInvoke(_params);
        //    //}
        //    //watch.Stop();

        //    //Console.WriteLine($"Time: {(decimal)watch.ElapsedTicks / (decimal)TimeSpan.TicksPerMillisecond} ms");
        //}

        public delegate object FastInvoke(object target, params object[] arguments);

        public class FastMethodFactory
        {
            public FastInvoke Get(MethodInfo method, bool callVir = true)
            {
                return null;
            }
        }


        [Test]
        public void TestTypedDelegate()
        {
            var methodInfo = typeof(Target).GetMethod("Sum");

            var instance = new Target();

            var d = (SumDelegate)methodInfo.CreateDelegate(typeof(SumDelegate), instance);

            var result = d(1, 2);

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var max = 20000;
            for (var i = 0; i < max; i++)
            {
                result = d(1, 2);
            }
            watch.Stop();

            Console.WriteLine($"Time: {(decimal)watch.ElapsedTicks / (decimal)TimeSpan.TicksPerMillisecond} ms");
        }

        [Test]
        public void TestDirectCall()
        {
            var instance = new Target();

            var result = instance.Sum(1, 2);

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var max = 20000;
            for (var i = 0; i < max; i++)
            {
                result = instance.Sum(1, 2);
            }
            watch.Stop();

            Console.WriteLine($"Time: {(decimal)watch.ElapsedTicks / (decimal)TimeSpan.TicksPerMillisecond} ms");
        }
    }
}