using UnityEngine;
using System;

namespace Billygoat.InputManager
{
    public class KeyboardInputCommand : ButtonInputCommand
    {
        private KeyCode keycode;

        public KeyboardInputCommand(IInputManager inputManager, KeyCode keycode)
            : this(inputManager, keycode, 0)
        {
        }

        public KeyboardInputCommand(IInputManager inputManager, KeyCode keycode, float delayTime)
            : base(delayTime)
        {
            this.inputManager = inputManager;
            this.keycode = keycode;

            ConfigureInput();
        }

        public void ConfigureInput()
        {
            input = inputManager.Keyboard.GetKey(keycode);

            buttonDown = () => { return input.ButtonDown; };
            buttonUp = () => { return input.ButtonUp; };
            buttonPressed = () => { return input.ButtonPressed; };
        }
    }
}