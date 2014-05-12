﻿//
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
using System.Reflection.Emit;

namespace NProxy.Core.Internal.Reflection.Emit
{
    /// <summary>
    /// Defines a type repository.
    /// </summary>
    internal interface ITypeRepository
    {
        /// <summary>
        /// Constructs a type builder.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="parentType">The parent type.</param>
        /// <returns>The type builder.</returns>
        TypeBuilder DefineType(string typeName, Type parentType);

        /// <summary>
        /// Returns a type for the specified method.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>The type.</returns>
        Type GetType(MethodInfo methodInfo);
    }
}