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

namespace NProxy.Core.Benchmark.Types
{
    internal sealed class StandardProxy : IStandard
    {
        private readonly IStandard _target;

        public StandardProxy(IStandard target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            _target = target;
        }

        #region IStandard Members

        public int Invoke(int value)
        {
            return _target.Invoke(value);
        }

        #endregion
    }
}