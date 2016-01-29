using UnityEngine;
using InControl;
using System.Collections.Generic;

namespace Billygoat.InputManager
{
    public class InControlTwoAxisControl : ITwoAxisControl
    {
        JoystickInput handle;
        TwoAxisInputControl _inControlCtrl;

        List<ITwoAxisControl> mergedControls = new List<ITwoAxisControl>();

        public InControlTwoAxisControl(JoystickInput input, TwoAxisInputControl control)
        {
            handle = input;
            _inControlCtrl = control;
        }

        #region ITwoAxisControl implementation
        public JoystickInput id
        {
            get
            {
                return handle;
            }
        }

        public override string ToString()
        {
            return id.ToString();
        }

        public bool Left
        {
            get
            {
                foreach (ITwoAxisControl control in mergedControls)
                {
                    if (control.Left)
                    {
                        return true;
                    }
                }
                return _inControlCtrl.Left;
            }
        }

        public bool Right
        {
            get
            {
                foreach (ITwoAxisControl control in mergedControls)
                {
                    if (control.Right)
                    {
                        return true;
                    }
                }
                return _inControlCtrl.Right;
            }
        }

        public bool Up
        {
            get
            {
                foreach (ITwoAxisControl control in mergedControls)
                {
                    if (control.Up)
                    {
                        return true;
                    }
                }
                return _inControlCtrl.Up;
            }
        }


        public bool Down
        {
            get
            {
                foreach (ITwoAxisControl control in mergedControls)
                {
                    if (control.Down)
                    {
                        return true;
                    }
                }
                return _inControlCtrl.Down;
            }
        }

        public Vector2 Value
        {
            get
            {
                float largestMagnitude = _inControlCtrl.Vector.magnitude;
                Vector2 largestVector = _inControlCtrl.Vector;

                foreach (ITwoAxisControl control in mergedControls)
                {
                    if (control.Value.magnitude > largestMagnitude)
                    {
                        largestMagnitude = control.Value.magnitude;
                        largestVector = control.Value;
                    }
                }
                return largestVector;
            }
        }

        public void Merge(ITwoAxisControl newControl)
        {
            mergedControls.Add(newControl);
        }
        #endregion


    }
}