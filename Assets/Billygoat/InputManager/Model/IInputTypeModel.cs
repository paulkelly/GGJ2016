using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;

namespace Billygoat.InputManager.Model
{
    public enum InputType
    {
        Gamepad,
        KeyboardAndMouse,
        TouchDevice
    }

    public interface IInputTypeModel
    {
        InputType InputType
        {
            get;

            set;
        }

        Signal<InputType> OnInputTypeChanged { get; }
    }
}