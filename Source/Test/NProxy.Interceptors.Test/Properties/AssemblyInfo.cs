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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

#if NETFRAMEWORK
[assembly: AssemblyKeyFile("../../NProxy.snk")]
[assembly: InternalsVisibleTo("NProxy.Dynamic, PublicKey=002400000480000094000000060200000024000052534131000400000100010031d0e185f342141fb582a63c5c3706ee107a49b7c4c988587512e9cf2d02473280bd9d5cf129d118978bb753339b1819c5f836a0940a0c3ec153ccad71b4786a388da0b4b9531b405d57ce00ac02ee019001eb1bcfdaa0afa1d1542adec526e1165ce740dd2d31ad682c4c8d9b305bc64c3ebb029dffa773d1f9e0e9a5847885")]
#else
[assembly: InternalsVisibleTo("NProxy.Dynamic")]
#endif
