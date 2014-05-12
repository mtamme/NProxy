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
using LinFu.Proxy.Interfaces;

namespace NProxy.Core.Benchmark
{
    internal sealed class LinFuProxyCache : IProxyCache
    {
        #region IProxyCache Members

        public bool Contains(Type baseType, params Type[] baseInterfaces)
        {
            return false;
        }

        public Type Get(Type baseType, params Type[] baseInterfaces)
        {
            throw new KeyNotFoundException();
        }

        public void Store(Type result, Type baseType, params Type[] baseInterfaces)
        {
        }

        #endregion
    }
}