using UnityEngine;
using InControl;
using System.Collections.Generic;

namespace Billygoat.InputManager
{
    public class InControlCtrl : IControl
    {
        ButtonInput handle;
        InputControl _inControlCtrl;

        List<IControl> mergedControls = new List<IControl>();

        public InControlCtrl(ButtonInput input, InputControl control)
        {
            handle = input;
            _inControlCtrl = control;
        }

        public void SetControl(InputControl control)
        {
            _inControlCtrl = control;
        }

        #region IControl implementation
        public ButtonInput id
        {
            get
            {
                return handle;
            }
        }
        public bool ButtonDown
        {
            get
            {
                foreach (IControl control in mergedControls)
                {
                    if (control.ButtonDown)
                    {
                        return true;
                    }
                }

                return _inControlCtrl.WasPressed;
            }
        }
        public bool ButtonUp
        {
            get
            {
                foreach (IControl control in mergedControls)
                {
                    if (control.ButtonUp)
                    {
                        return true;
                    }
                }

                return _inControlCtrl.WasReleased;
            }
        }
        public bool ButtonPressed
        {
            get
            {
                foreach (IControl control in mergedControls)
                {
                    if (control.ButtonPressed)
                    {
                        return true;
                    }
                }

                return _inControlCtrl.IsPressed;
            }
        }
        public float Value
        {
            get
            {
                float result = _inControlCtrl.Value;
                foreach (IControl control in mergedControls)
                {
                    if (control.Value > result)
                    {
                        result = control.Value;
                    }
                }
                return result;
            }
        }

        public void Merge(IControl newControl)
        {
            mergedControls.Add(newControl);
        }
        #endregion
    }
}