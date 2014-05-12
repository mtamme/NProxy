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
    internal sealed class MdWriter : IWriter
    {
        private readonly TextWriter _writer;

        public MdWriter(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            var pathWithExtension = Path.ChangeExtension(path, "md");

            _writer = new StreamWriter(pathWithExtension, false, Encoding.UTF8);
        }

        #region IWriter Members

        public void Close()
        {
            _writer.Close();
        }

        public void WriteHeader()
        {
            _writer.WriteLine("| Type      | Version | Scenario                             | Iterations | Total time in ms | Average time in µs |");
            _writer.WriteLine("|:----------|--------:|:-------------------------------------|-----------:|-----------------:|-------------------:|");
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
            var line = String.Format(CultureInfo.InvariantCulture, "|{0,-11}|{1,9}|{2,-38}|{3,12}|{4,18:0.000}|{5,20:0.000}|",
                typeName,
                version,
                scenario.Description,
                iterations,
                totalMilliseconds,
                averageMicroseconds);

            _writer.WriteLine(line);
        }

        #endregion
    }
}