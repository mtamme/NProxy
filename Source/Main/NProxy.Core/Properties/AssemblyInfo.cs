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
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyTitle("NProxy.Core")]
[assembly: AssemblyDescription("NProxy is a library for the .NET framework to create lightweight dynamic proxies.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Martin Tamme")]
[assembly: AssemblyProduct("NProxy.Core")]
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

[assembly: AssemblyVersion("2.3.0.0")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: InternalsVisibleTo("NProxy.Core.Test, PublicKey=002400000480000094000000060200000024000052534131000400001100000055a212b390ce55e01b54badc7783e456552b9a35be0d8f41b3d296f93b21857d3f0c10ef9394307e3c3808417ed207b20cbe9592dffc267077c9e7f57779ed29359b2d7cc4810fd6e666efba6a1c48c9636d73dbb344207c07b611e98dd0305bb9484dd26eaf688920bc4c13abf1a25f263ba637ee339fc88dae254c00fbff9a")]
[assembly: InternalsVisibleTo("NProxy.Dynamic, PublicKey=002400000480000094000000060200000024000052534131000400000100010031d0e185f342141fb582a63c5c3706ee107a49b7c4c988587512e9cf2d02473280bd9d5cf129d118978bb753339b1819c5f836a0940a0c3ec153ccad71b4786a388da0b4b9531b405d57ce00ac02ee019001eb1bcfdaa0afa1d1542adec526e1165ce740dd2d31ad682c4c8d9b305bc64c3ebb029dffa773d1f9e0e9a5847885")]