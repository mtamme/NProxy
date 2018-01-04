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
using System.Linq;
using System.Reflection;
using System.Text;

namespace NProxy.Core.Internal.Reflection
{
    /// <summary>
    /// Provides <see cref="EventInfo"/> extension methods.
    /// </summary>
    internal static class EventInfoExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the specified event is overrideable.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <returns>A value indicating whether the specified event is overrideable.</returns>
        public static bool CanOverride(this EventInfo eventInfo)
        {
            if (eventInfo == null)
                throw new ArgumentNullException("eventInfo");

            var methodInfos = eventInfo.GetMethods();

            return methodInfos.All(m => m.CanOverride());
        }

        /// <summary>
        /// Returns the methods for the specified event.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <returns>The methods for the specified event.</returns>
        public static IEnumerable<MethodInfo> GetMethods(this EventInfo eventInfo)
        {
            if (eventInfo == null)
                throw new ArgumentNullException("eventInfo");

            var methodInfos = new List<MethodInfo>
            {
                eventInfo.GetAddMethod(true),
                eventInfo.GetRemoveMethod(true)
            };
            var raiseMethodInfo = eventInfo.GetRaiseMethod(true);

            if (raiseMethodInfo != null)
                methodInfos.Add(raiseMethodInfo);

            return methodInfos;
        }

        /// <summary>
        /// Returns the full name of the specified event.
        /// </summary>
        /// <param name="eventInfo">The event information.</param>
        /// <returns>The full name.</returns>
        public static string GetFullName(this EventInfo eventInfo)
        {
            if (eventInfo == null)
                throw new ArgumentNullException("eventInfo");

            var fullName = new StringBuilder();

            fullName.Append(eventInfo.DeclaringType);
            fullName.Append(Type.Delimiter);
            fullName.Append(eventInfo.Name);

            return fullName.ToString();
        }
    }
}