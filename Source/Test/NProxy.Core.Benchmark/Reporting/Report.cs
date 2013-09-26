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
using System.Reflection;
using NProxy.Core.Benchmark.Reporting;

namespace NProxy.Core.Benchmark.Reporting
{
    internal sealed class Report
    {
        public static Report Instance = new Report();

        private Lazy<IWriter> _writer;

        public Report()
        {
            _writer = new Lazy<IWriter>(CreateWriter);
        }

        private static IWriter CreateWriter()
        {
            var path = String.Format("Benchmark_{0:yyyyMMdd_HHmmss}", DateTime.Now);
            var writer = new CompositeWriter(new CsvWriter(path), new MdWriter(path));

            writer.WriteHeader();

            return writer;
        }

        public void Close()
        {
            if (_writer.IsValueCreated)
                _writer.Value.Close();
        }

        public void Write(AssemblyName assemblyName, Scenario scenario, int iterations, TimeSpan elapsedTime)
        {
            if (assemblyName == null)
                throw new ArgumentNullException("assemblyName");

            var version = String.Format("v {0}.{1}.{2}",
                                        assemblyName.Version.Major,
                                        assemblyName.Version.Minor,
                                        assemblyName.Version.Build);

            Write(assemblyName.Name, version, scenario, iterations, elapsedTime);
        }

        public void Write(string typeName, string version, Scenario scenario, int iterations, TimeSpan elapsedTime)
        {
            _writer.Value.WriteRow(typeName, version, scenario, iterations, elapsedTime);
        }
    }
}