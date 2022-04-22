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

using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace NProxy.Core.Benchmark.Reporting
{
    internal sealed class Report
    {
        public static readonly Report Instance = new Report();

        private readonly Lazy<IWriter> _writer;

        private Report()
        {
            _writer = new Lazy<IWriter>(CreateWriter);
        }

        private static IWriter CreateWriter()
        {
            var file = String.Format("Benchmark_{0:yyyyMMdd_HHmmss}", DateTime.Now);
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, file);
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

            var version = String.Format("v{0}.{1}", assemblyName.Version.Major, assemblyName.Version.Minor);

            Write(assemblyName.Name, version, scenario, iterations, elapsedTime);
        }

        public void Write(string typeName, string version, Scenario scenario, int iterations, TimeSpan elapsedTime)
        {
            _writer.Value.WriteRow(typeName, version, scenario, iterations, elapsedTime);
        }
    }
}