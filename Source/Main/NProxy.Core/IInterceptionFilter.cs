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

using System.Reflection;

namespace NProxy.Core
{
    /// <summary>
    /// Defines an interception filter.
    /// </summary>
    public interface IInterceptionFilter
    {
        /// <summary>
        /// Returns a value indicating whether the specified event should be intercepted.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <returns>A value indicating whether the specified event should be intercepted.</returns>
        bool AcceptEvent(EventInfo eventInfo);

        /// <summary>
        /// Returns a value indicating whether the specified property should be intercepted.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <returns>A value indicating whether the specified property should be intercepted.</returns>
        bool AcceptProperty(PropertyInfo propertyInfo);

        /// <summary>
        /// Returns a value indicating whether the specified method should be intercepted.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns>A value indicating whether the specified method should be intercepted.</returns>
        bool AcceptMethod(MethodInfo methodInfo);
    }
}