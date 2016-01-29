using UnityEngine;
using strange.extensions.signal.impl;
using Billygoat.InputManager;
using Billygoat.InputManager._View;

namespace Billygoat.InputManager
{
    public class SetControlLayoutSignal : Signal<ControlLayout>
    {
    }

    public class UpdateControlLayout : Signal
    {
    }
}