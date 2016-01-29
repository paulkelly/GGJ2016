using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Billygoat.InputManager
{
    public class VirtualTwoAxisControl : ITwoAxisControl
    {
        private Vector2 direction;

        List<ITwoAxisControl> mergedControls = new List<ITwoAxisControl>();

        public VirtualTwoAxisControl()
        {
            direction = Vector2.zero;
        }

        public VirtualTwoAxisControl(Vector2 value)
        {
            direction = value;
        }

        #region ITwoAxisControl implementation

        public void Merge(ITwoAxisControl newControl)
        {
            mergedControls.Add(newControl);
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
                return direction.x < 0;
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
                return direction.x > 0;
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
                return direction.y > 0;
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
                return direction.x < 0;
            }
        }

        public Vector2 Value
        {
            get
            {
                return direction;
            }

            set
            {
                direction = value;
            }
        }

        #endregion
    }
}
