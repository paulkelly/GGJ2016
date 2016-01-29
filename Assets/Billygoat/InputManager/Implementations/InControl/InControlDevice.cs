using UnityEngine;
using InControl;
using System;

namespace Billygoat.InputManager.Implementations.InControlImpl
{
    public class InControlDevice : IDevice
    {
        InputDevice inControlDevice;

        IControl[] ButtonControls;
        ITwoAxisControl[] JoystickControls;

        public InControlDevice(InputDevice device)
        {
            inControlDevice = device;

            ButtonControls = new IControl[Enum.GetValues(typeof(ButtonInput)).Length];

            ButtonControls[(int)ButtonInput.Action1] = new InControlCtrl(ButtonInput.Action1, inControlDevice.Action1);
            ButtonControls[(int)ButtonInput.Action2] = new InControlCtrl(ButtonInput.Action2, inControlDevice.Action2);
            ButtonControls[(int)ButtonInput.Action3] = new InControlCtrl(ButtonInput.Action3, inControlDevice.Action3);
            ButtonControls[(int)ButtonInput.Action4] = new InControlCtrl(ButtonInput.Action4, inControlDevice.Action4);

            ButtonControls[(int)ButtonInput.LeftStickHorizontal] = new InControlOneAxisControl(ButtonInput.LeftStickHorizontal, inControlDevice.LeftStickX);
            ButtonControls[(int)ButtonInput.LeftStickVertical] = new InControlOneAxisControl(ButtonInput.LeftStickVertical, inControlDevice.LeftStickY);
            ButtonControls[(int)ButtonInput.RightStickHorizontal] = new InControlOneAxisControl(ButtonInput.RightStickHorizontal, inControlDevice.RightStickX);
            ButtonControls[(int)ButtonInput.RightStickVertical] = new InControlOneAxisControl(ButtonInput.RightStickVertical, inControlDevice.RightStickY);

            ButtonControls[(int)ButtonInput.DPadUp] = new InControlCtrl(ButtonInput.DPadUp, inControlDevice.DPadUp);
            ButtonControls[(int)ButtonInput.DPadDown] = new InControlCtrl(ButtonInput.DPadDown, inControlDevice.DPadDown);
            ButtonControls[(int)ButtonInput.DPadLeft] = new InControlCtrl(ButtonInput.DPadLeft, inControlDevice.DPadLeft);
            ButtonControls[(int)ButtonInput.DPadRight] = new InControlCtrl(ButtonInput.DPadRight, inControlDevice.DPadRight);

            ButtonControls[(int)ButtonInput.LeftBumper] = new InControlCtrl(ButtonInput.LeftBumper, inControlDevice.LeftBumper);
            ButtonControls[(int)ButtonInput.RightBumper] = new InControlCtrl(ButtonInput.RightBumper, inControlDevice.RightBumper);
            ButtonControls[(int)ButtonInput.LeftTrigger] = new InControlCtrl(ButtonInput.LeftTrigger, inControlDevice.LeftTrigger);
            ButtonControls[(int)ButtonInput.RightTrigger] = new InControlCtrl(ButtonInput.RightTrigger, inControlDevice.RightTrigger);

            ButtonControls[(int)ButtonInput.Menu] = new InControlMenuButton(ButtonInput.Menu, inControlDevice);

            JoystickControls = new ITwoAxisControl[Enum.GetValues(typeof(JoystickInput)).Length];

            JoystickControls[(int)JoystickInput.LeftStick] = new InControlTwoAxisControl(JoystickInput.LeftStick, inControlDevice.LeftStick);
            JoystickControls[(int)JoystickInput.RightStick] = new InControlTwoAxisControl(JoystickInput.RightStick, inControlDevice.RightStick);
            JoystickControls[(int)JoystickInput.DPad] = new InControlTwoAxisControl(JoystickInput.DPad, inControlDevice.DPad);
            JoystickControls[(int)JoystickInput.WASD] = new KeyboardTwoAxisControl(JoystickInput.WASD, KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S);
            JoystickControls[(int)JoystickInput.ArrowKeys] = new KeyboardTwoAxisControl(JoystickInput.ArrowKeys, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow);
        }

        #region IDevice implementation
        public IControl GetButtonControl(ButtonInput id)
        {
            return ButtonControls[(int)id];
        }

        public ITwoAxisControl GetTwoAxisControl(JoystickInput id)
        {
            return JoystickControls[(int)id];
        }

        public IControl Action1
        {
            get
            {
                return GetButtonControl(ButtonInput.Action1);
            }
        }
        public IControl Action2
        {
            get
            {
                return GetButtonControl(ButtonInput.Action2);
            }
        }
        public IControl Action3
        {
            get
            {
                return GetButtonControl(ButtonInput.Action3);
            }
        }
        public IControl Action4
        {
            get
            {
                return GetButtonControl(ButtonInput.Action4);
            }
        }
        public IControl LeftStickHorizontal
        {
            get
            {
                return GetButtonControl(ButtonInput.LeftStickHorizontal);
            }
        }
        public IControl LeftStickVertical
        {
            get
            {
                return GetButtonControl(ButtonInput.LeftStickVertical);
            }
        }
        public IControl RightStickHorizontal
        {
            get
            {
                return GetButtonControl(ButtonInput.RightStickHorizontal);
            }
        }
        public IControl RightStickVertical
        {
            get
            {
                return GetButtonControl(ButtonInput.RightStickVertical);
            }
        }
        public IControl DPadUp
        {
            get
            {
                return GetButtonControl(ButtonInput.DPadUp);
            }
        }
        public IControl DPadDown
        {
            get
            {
                return GetButtonControl(ButtonInput.DPadDown);
            }
        }
        public IControl DPadLeft
        {
            get
            {
                return GetButtonControl(ButtonInput.DPadLeft);
            }
        }
        public IControl DPadRight
        {
            get
            {
                return GetButtonControl(ButtonInput.DPadRight);
            }
        }
        public IControl LeftBumper
        {
            get
            {
                return GetButtonControl(ButtonInput.LeftBumper);
            }
        }
        public IControl RightBumper
        {
            get
            {
                return GetButtonControl(ButtonInput.RightBumper);
            }
        }
        public IControl LeftTrigger
        {
            get
            {
                return GetButtonControl(ButtonInput.LeftTrigger);
            }
        }
        public IControl RightTrigger
        {
            get
            {
                return GetButtonControl(ButtonInput.RightTrigger);
            }
        }


        public IControl Menu
        {
            get
            {
                return GetButtonControl(ButtonInput.Menu);
            }
        }


        public ITwoAxisControl LeftStick
        {
            get
            {
                return GetTwoAxisControl(JoystickInput.LeftStick);
            }
        }
        public ITwoAxisControl RightStick
        {
            get
            {
                return GetTwoAxisControl(JoystickInput.RightStick);
            }
        }
        public ITwoAxisControl DPad
        {
            get
            {
                return GetTwoAxisControl(JoystickInput.DPad);
            }
        }
        #endregion
    }
}