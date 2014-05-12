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

namespace NProxy.Core.Test.Types
{
    internal sealed class Class
    {
        public interface INested<TInner>
        {
            event Action Event;

            TInner Property { get; set; }

            TInner Method();
        }

        public sealed class Nested<TInner>
        {
#pragma warning disable 0067

            public event Action Event;

#pragma warning restore 0067

            public TInner Property { get; set; }

            public TInner Method()
            {
                return default(TInner);
            }
        }

        public sealed class Nested
        {
#pragma warning disable 0067

            public event Action Event;

#pragma warning restore 0067

            public object Property { get; set; }

            public void Method()
            {
            }
        }

#pragma warning disable 0067

        public event Action Event;

#pragma warning restore 0067

        public object Property { get; set; }

        public void Method()
        {
        }
    }

    internal sealed class Class<TOuter>
    {
        public interface INested<TInner>
        {
            event Action Event;

            TInner Property { get; set; }

            TInner Method();
        }

        public sealed class Nested<TInner>
        {
#pragma warning disable 0067

            public event Action Event;

#pragma warning restore 0067

            public TInner Property { get; set; }

            public TInner Method()
            {
                return default(TInner);
            }
        }

        public sealed class Nested
        {
#pragma warning disable 0067

            public event Action Event;

#pragma warning restore 0067

            public object Property { get; set; }

            public void Method()
            {
            }
        }

#pragma warning disable 0067

        public event Action Event;

#pragma warning restore 0067

        public TOuter Property { get; set; }

        public TOuter Method()
        {
            return default(TOuter);
        }
    }

    internal class Other : IOther
    {
        #region IOther Members

#pragma warning disable 0067

        public event Action OtherEvent;

#pragma warning restore 0067

        public void OtherMethod()
        {
        }

        public int OtherGetProperty { get; set; }

        public int OtherSetProperty { get; set; }

        #endregion
    }
}