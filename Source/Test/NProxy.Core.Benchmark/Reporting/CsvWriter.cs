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
using System.Globalization;
using System.IO;
using System.Text;

namespace NProxy.Core.Benchmark.Reporting
{
    internal sealed class CsvWriter : IWriter
    {
        private readonly TextWriter _writer;

        public CsvWriter(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            var pathWithExtension = Path.ChangeExtension(path, "csv");

            _writer = new StreamWriter(pathWithExtension, false, Encoding.UTF8);
        }

        #region IWriter Members

        public void Close()
        {
            _writer.Close();
        }

        public void WriteHeader()
        {
            _writer.WriteLine("\"Type\";\"Version\";\"Scenario\";\"Description\";\"Iterations\";\"Total time (ms)\";\"Average time (ms)\";\"Average time (µs)\"");
        }

        public void WriteRow(string typeName, string version, Scenario scenario, int iterations, TimeSpan elapsedTime)
        {
            if (typeName == null)
                throw new ArgumentNullException("typeName");

            if (version == null)
                throw new ArgumentNullException("version");

            if (scenario == null)
                throw new ArgumentNullException("scenario");

            var totalMilliseconds = elapsedTime.TotalMilliseconds;
            var averageMicroseconds = (totalMilliseconds*1000)/iterations;
            var line = String.Format(CultureInfo.InvariantCulture, "\"{0}\";\"{1}\";\"{2}\";\"{3}\";{4};{5:0.000};{6:0.000};{7:0.000}",
                typeName,
                version,
                scenario.Name,
                scenario.Description,
                iterations,
                totalMilliseconds,
                averageMicroseconds/1000,
                averageMicroseconds);

            _writer.WriteLine(line);
        }

        #endregion
    }
}