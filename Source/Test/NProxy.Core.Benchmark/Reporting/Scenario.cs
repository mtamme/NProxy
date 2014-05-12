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
    internal sealed class Scenario
    {
        public static readonly Scenario ProxyGeneration = new Scenario(
            "ProxyGeneration",
            "Proxy generation");

        public static readonly Scenario ProxyGenerationWithGenericParameter = new Scenario(
            "ProxyGenerationWithGenericParameter",
            "Proxy generation (w/ generic param)");

        public static readonly Scenario ProxyInstantiation = new Scenario(
            "ProxyInstantiation",
            "Proxy instantiation");

        public static readonly Scenario ProxyInstantiationWithGenericParameter = new Scenario(
            "ProxyInstantiationWithGenericParameter",
            "Proxy instantiation (w/ generic param)");

        public static readonly Scenario MethodInvocation = new Scenario(
            "MethodInvocation",
            "Method invocation");

        public static readonly Scenario MethodInvocationWithGenericParameter = new Scenario(
            "MethodInvocationWithGenericParameter",
            "Method invocation (w/ generic param)");

        private readonly string _name;

        private readonly string _description;

        private Scenario(string name, string description)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (description == null)
                throw new ArgumentNullException("description");

            _name = name;
            _description = description;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }
    }
}