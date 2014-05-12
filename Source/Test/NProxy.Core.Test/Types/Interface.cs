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