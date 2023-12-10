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
using System.Collections.Generic;

namespace NProxy.Core.Internal
{
    /// <summary>
    /// Defines a proxy information.
    /// </summary>
    internal interface IProxyInfo
    {
        /// <summary>
        /// Returns the declaring type.
        /// </summary>
        Type DeclaringType { get; }

        /// <summary>
        /// Returns the parent type.
        /// </summary>
        Type ParentType { get; }

        /// <summary>
        /// Returns all implemented interfaces.
        /// </summary>
        IEnumerable<Type> ImplementedInterfaces { get; }

        /// <summary>
        /// Dispatches to the specific visit method for each member.
        /// </summary>
        /// <param name="proxyInfoVisitor">The proxy information visitor.</param>
        void AcceptVisitor(IProxyInfoVisitor proxyInfoVisitor);

        /// <summary>
        /// Unwraps the specified proxy.
        /// </summary>
        /// <param name="proxy">The proxy object.</param>
        /// <returns>The wrapped object.</returns>
        object UnwrapProxy(object proxy);

        /// <summary>
        /// Creates a new proxy of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <returns>The new proxy object.</returns>
        object CreateProxy(Type type, object[] arguments);
    }
}