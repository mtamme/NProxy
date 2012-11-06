//
// NProxy is a library for the .NET framework to create lightweight dynamic proxies.
// Copyright © 2012 Martin Tamme
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
    internal interface IEmpty
    {
    }

    internal interface IBase
    {
        event Action BaseEvent;

        int BaseGetProperty { get; }

        int BaseSetProperty { set; }

        void BaseMethod();
    }

    internal interface IHideBase : IBase
    {
        new event Action BaseEvent;

        new int BaseGetProperty { get; }

        new int BaseSetProperty { set; }

        new void BaseMethod();
    }

    internal interface IOther
    {
        event Action OtherEvent;

        int OtherGetProperty { get; }

        int OtherSetProperty { set; }

        void OtherMethod();
    }

    internal interface IOne : IBase
    {
        event Action Event;

        int GetProperty { get; }

        int SetProperty { set; }

        void Method();
    }

    internal interface ITwo : IBase
    {
        event Action Event;

        int GetProperty { get; }

        int SetProperty { set; }

        void Method();
    }

    internal interface IOneTwo : IOne, ITwo
    {
    }
}
