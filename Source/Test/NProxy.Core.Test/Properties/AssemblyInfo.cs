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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyTitle("NProxy.Core.Test")]
[assembly: AssemblyDescription("NProxy is a library for the .NET framework to create lightweight dynamic proxies.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Martin Tamme")]
[assembly: AssemblyProduct("NProxy.Core.Test")]
[assembly: AssemblyCopyright("Copyright © Martin Tamme")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

// The Common Language Specification (CLS) defines naming restrictions,
// data types, and rules to which assemblies must conform if they are to be used 
// across programming languages. Good design dictates that all assemblies
// explicitly indicate CLS compliance with CLSCompliantAttribute. If the 
// attribute is not present on an assembly, the assembly is not compliant.

[assembly: CLSCompliant(true)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

[assembly: AssemblyVersion("0.0")]
[assembly: InternalsVisibleTo("NProxy.Dynamic, PublicKey=0024000004800000940000000602000000240000525341310004000001000100df0530d40b8735b471e42a9f183238672fd0c1fe9867a2de9427af3bc7a9af73ec73a0a5389405c7052ab90f9df38c1b293b92ad27533cd29d5611b3b8672ede99c6604e7f5b3cf84fb9e6a94c1f36fa13aad99c3c29f97fe21131540ba20591dccdff897ecd0057e6296dc4acbdd220189f781bf7363f7c0b5ff94d7ab727d3")]


// disable warning about using /keyfile instead of AssemblyKeyFile
#pragma warning disable 1699
// using '\' will break mono builds
[assembly: AssemblyKeyFile(@"../../NProxy.snk")]
#pragma warning restore 1699
