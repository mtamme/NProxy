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
using System.Collections.Generic;

namespace NProxy.Core
{
    /// <summary>
    /// Defines a proxy type registry.
    /// </summary>
    public interface IProxyTypeRegistry
    {
        /// <summary>
        /// Returns a proxy type.
        /// </summary>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="interfaceTypes">The additional interface types.</param>
        /// <returns>The proxy type.</returns>
        IProxyType GetProxyType(Type declaringType, IEnumerable<Type> interfaceTypes);
    }
}