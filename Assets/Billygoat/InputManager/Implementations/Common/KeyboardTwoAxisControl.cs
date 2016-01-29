using UnityEngine;
using System.Collections.Generic;

namespace Billygoat.InputManager
{
    public class KeyboardTwoAxisControl : ITwoAxisControl
    {
        JoystickInput handle;
        KeyCode _left;
        KeyCode _right;
        KeyCode _down;
        KeyCode _up;

        List<ITwoAxisControl> mergedControls = new List<ITwoAxisControl>();

        public KeyboardTwoAxisControl(JoystickInput input, KeyCode left, KeyCode right, KeyCode up, KeyCode down)
        {
            handle = input;
            _left = left;
            _right = right;
            _down = down;
            _up = up;
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
                return Input.GetKey(_left);
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
                return Input.GetKey(_right);
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
                return Input.GetKey(_up);
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
                return Input.GetKey(_down);
            }
        }

        public Vector2 Value
        {
            get
            {
                Vector2 _value = Vector2.zero;
                if (Input.GetKey(_left))
                {
                    _value.x -= 1;
                }
                if (Input.GetKey(_right))
                {
                    _value.x += 1;
                }
                if (Input.GetKey(_up))
                {
                    _value.y += 1;
                }
                if (Input.GetKey(_down))
                {
                    _value.y -= 1;
                }
                _value = _value.normalized;

                float largestMagnitude = _value.magnitude;
                Vector2 largestVector = _value;
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
