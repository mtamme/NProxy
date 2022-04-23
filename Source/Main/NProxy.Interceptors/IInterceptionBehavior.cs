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

using System.Collections.Generic;
using System.Reflection;

namespace NProxy.Interceptors
{
    /// <summary>
    /// Defines an interception behavior.
    /// </summary>
    public interface IInterceptionBehavior
    {
        /// <summary>
        /// Applies the interception behavior.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="interceptors">The interceptors.</param>
        void Apply(MemberInfo memberInfo, ICollection<IInterceptor> interceptors);

        /// <summary>
        /// Validates the interception behavior.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        void Validate(MemberInfo memberInfo);
    }
}