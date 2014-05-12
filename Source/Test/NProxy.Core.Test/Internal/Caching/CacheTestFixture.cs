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
using NProxy.Core.Internal.Caching;
using NUnit.Framework;

namespace NProxy.Core.Test.Internal.Caching
{
    [TestFixture]
    public sealed class CacheTestFixture
    {
        [Test]
        public void GetOrAddWithoutCacheHitTest()
        {
            // Arrange
            var invocationCount = 0;
            Func<int, string> valueFactory = k =>
            {
                invocationCount++;
                return Convert.ToString(k);
            };
            var cache = new Cache<int, string>();

            // Act
            var value = cache.GetOrAdd(1, valueFactory);

            // Assert
            Assert.That(value, Is.EqualTo("1"));
            Assert.That(invocationCount, Is.EqualTo(1));
        }

        [Test]
        public void GetOrAddWithCacheHitTest()
        {
            // Arrange
            // Arrange
            var invocationCount = 0;
            Func<int, string> valueFactory = k =>
            {
                invocationCount++;
                return Convert.ToString(k);
            };
            var cache = new Cache<int, string>();

            // Act
            cache.GetOrAdd(1, valueFactory);

            var value = cache.GetOrAdd(1, valueFactory);

            // Assert
            Assert.That(value, Is.EqualTo("1"));
            Assert.That(invocationCount, Is.EqualTo(1));
        }
    }
}