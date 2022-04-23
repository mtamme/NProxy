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

namespace NProxy.Interceptors.Language
{
    /// <summary>
    /// Defines the <c>ExtendWith</c> verb.
    /// </summary>
    /// <typeparam name="T">The declaring type.</typeparam>
    /// <typeparam name="TInterceptor">The interceptor type.</typeparam>
    public interface IExtendWith<T, in TInterceptor> : IImplement<T, TInterceptor> where T : class
    {
        /// <summary>
        /// Specifies a mixin.
        /// </summary>
        /// <typeparam name="TMixin">The mixin type.</typeparam>
        /// <returns>The <c>ExtendWith</c> verb.</returns>
        IExtendWith<T, TInterceptor> ExtendWith<TMixin>() where TMixin : class, new();

        /// <summary>
        /// Specifies mixins.
        /// </summary>
        /// <param name="mixins">The mixins.</param>
        /// <returns>The <c>ExtendWith</c> verb.</returns>
        IExtendWith<T, TInterceptor> ExtendWith(params object[] mixins);
    }
}