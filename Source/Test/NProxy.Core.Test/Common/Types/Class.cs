//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © Martin Tamme
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//
using System;

namespace NProxy.Core.Test.Common.Types
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
}
