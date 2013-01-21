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

namespace NProxy.Core.Test.Performance
{
    internal sealed class Scenario
    {
        public static readonly Scenario ProxyGeneration = new Scenario(
            1,
            "ProxyGeneration",
            "Proxy generation");

        public static readonly Scenario ProxyGenerationWithGenericParameter = new Scenario(
            2,
            "ProxyGenerationWithGenericParameter",
            "Proxy generation (with generic parameter)");

        public static readonly Scenario ProxyInstantiation = new Scenario(
            3,
            "ProxyInstantiation",
            "Proxy instantiation");

        public static readonly Scenario ProxyInstantiationWithGenericParameter = new Scenario(
            4,
            "ProxyInstantiationWithGenericParameter",
            "Proxy instantiation (with generic parameter)");

        public static readonly Scenario MethodInvocation = new Scenario(
            5,
            "MethodInvocation",
            "Method invocation");

        public static readonly Scenario MethodInvocationWithGenericParameter = new Scenario(
            6,
            "MethodInvocationWithGenericParameter",
            "Method invocation (with generic parameter)");

        private readonly int _id;

        private readonly string _name;

        private readonly string _description;

        private Scenario(int id, string name, string description)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (description == null)
                throw new ArgumentNullException("description");

            _id = id;
            _name = name;
            _description = description;
        }

        public int Id
        {
            get { return _id; }
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