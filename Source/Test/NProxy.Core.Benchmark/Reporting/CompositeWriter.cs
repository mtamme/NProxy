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