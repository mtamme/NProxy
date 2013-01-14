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
using System.Threading;
using System.Threading.Tasks;
using NProxy.Core.Internal.Generators;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal.Generators
{
    [TestFixture]
    public sealed class TypeCacheTestFixture
    {
        [Test]
        public void GetTypeWithoutCacheHitTest()
        {
            // Arrange
            var typeProvider = new MockTypeProvider<string>(d =>
                {
                    if (d != "1")
                        throw new InvalidOperationException();

                    return typeof (object);
                });
            var typeCache = new TypeCache<string>(typeProvider);

            // Act
            var type = typeCache.GetType("1");

            // Assert
            Assert.That(type, Is.EqualTo(typeof (object)));
            Assert.That(typeProvider.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void GetTypeWithCacheHitTest()
        {
            // Arrange
            var typeProvider = new MockTypeProvider<string>(d =>
                {
                    if (d != "1")
                        throw new InvalidOperationException();

                    return typeof (object);
                });
            var typeCache = new TypeCache<string>(typeProvider);

            // Act
            typeCache.GetType("1");

            var type = typeCache.GetType("1");

            // Assert
            Assert.That(type, Is.EqualTo(typeof (object)));
            Assert.That(typeProvider.InvocationCount, Is.EqualTo(1));
        }

        [Test]
        public void GetTypeWithoutCacheHitAndConcurrencyTest()
        {
            // Arrange
            var monitor = new Object();
            var typeProvider = new MockTypeProvider<string>(d =>
                {
                    if (d != "1")
                        throw new InvalidOperationException();

                    lock (monitor)
                    {
                        Monitor.Wait(monitor);
                    }

                    return typeof (object);
                });

            var typeCache = new TypeCache<string>(typeProvider);

            // Act
            var firstTask = Task.Factory.StartNew(() => typeCache.GetType("1"));
            var secondTask = Task.Factory.StartNew(() => typeCache.GetType("1"));

            firstTask.Wait(500);
            secondTask.Wait(500);

            Assert.That(firstTask.IsCompleted, Is.False);
            Assert.That(secondTask.IsCompleted, Is.False);

            lock (monitor)
            {
                Monitor.Pulse(monitor);
            }

            Task.WaitAll(firstTask, secondTask);

            // Assert
            Assert.That(firstTask.Result, Is.EqualTo(typeof (object)));
            Assert.That(secondTask.Result, Is.EqualTo(typeof (object)));
            Assert.That(typeProvider.InvocationCount, Is.EqualTo(1));
        }
    }
}