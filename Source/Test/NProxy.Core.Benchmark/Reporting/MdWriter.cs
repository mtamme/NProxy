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