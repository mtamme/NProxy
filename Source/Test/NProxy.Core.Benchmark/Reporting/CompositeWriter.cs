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

namespace NProxy.Core.Benchmark.Reporting
{
    internal sealed class CompositeWriter : IWriter
    {
        private readonly IWriter[] _writers;

        public CompositeWriter(params IWriter[] writers)
        {
            if (writers == null)
                throw new ArgumentNullException("writers");

            _writers = writers;
        }

        private void ForEachWriter(Action<IWriter> action)
        {
            foreach (var writer in _writers)
            {
                action(writer);
            }
        }

        #region IWriter Members

        public void Close()
        {
            ForEachWriter(w => w.Close());
        }

        public void WriteHeader()
        {
            ForEachWriter(w => w.WriteHeader());
        }

        public void WriteRow(string typeName, string version, Scenario scenario, int iterations, TimeSpan elapsedTime)
        {
            ForEachWriter(w => w.WriteRow(typeName, version, scenario, iterations, elapsedTime));
        }

        #endregion
    }
}