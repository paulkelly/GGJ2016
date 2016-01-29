using System;
using System.Collections.Generic;
using Billygoat.InputManager;

namespace Billygoat.InputManager
{
    [Serializable]
    public class ControlLayoutPersistance
    {
        public List<GamepadButtonInputMapping> ButtonMap = new List<GamepadButtonInputMapping>();
        public List<JoystickInputMapping> JoystickMap = new List<JoystickInputMapping>();
        public List<KeyboardInputMapping> KeyboardMap = new List<KeyboardInputMapping>();
    }
}