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
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace NProxy.Core.Test.Performance
{
    [SetUpFixture]
    internal sealed class Report
    {
        public static Report Instance { get; private set; }

        private Lazy<TextWriter> _writer;

        [SetUp]
        public void SetUp()
        {
            var path = String.Format("PerformanceReport_{0:yyyyMMdd_HHmmss}.csv", DateTime.Now);

            _writer = new Lazy<TextWriter>(() => CreateWriter(path));

            Instance = this;
        }

        [TearDown]
        public void TearDown()
        {
            if (_writer.IsValueCreated)
                _writer.Value.Close();
        }

        private static TextWriter CreateWriter(string path)
        {
            var writer = new StreamWriter(path, false);

            writer.WriteLine("\"Library\";\"Version\";\"Scenario ID\";\"Scenario name\";\"Scenario description\";\"Iterations\";\"Total time in ms\";\"Average time in µs\"");

            return writer;
        }

        private TextWriter Writer
        {
            get { return _writer.Value; }
        }

        public void WriteValues(AssemblyName assemblyName, Scenario scenario, int iterations, TimeSpan elapsedTime)
        {
            if (assemblyName == null)
                throw new ArgumentNullException("assemblyName");

            if (scenario == null)
                throw new ArgumentNullException("scenario");

            var totalMilliseconds = elapsedTime.TotalMilliseconds;
            var averageMicroseconds = (totalMilliseconds*1000)/iterations;

            Writer.WriteLine("\"{0}\";\"{1}.{2}.{3}\";{4};\"{5}\";\"{6}\";{7};{8:0.000};{9:0.000}",
                             assemblyName.Name,
                             assemblyName.Version.Major,
                             assemblyName.Version.Minor,
                             assemblyName.Version.Build,
                             scenario.Id,
                             scenario.Name,
                             scenario.Description,
                             iterations,
                             totalMilliseconds,
                             averageMicroseconds);
        }
    }
}