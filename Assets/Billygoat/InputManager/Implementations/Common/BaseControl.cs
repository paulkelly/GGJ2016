using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Billygoat.InputManager
{
    public class BaseControl : IControl
    {
        List<IControl> mergedControls = new List<IControl>();

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

                return false;
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

                return false;
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

                return false;
            }
        }

        public float Value
        {
            get
            {
                float result = 0;
                if (ButtonPressed)
                {
                    result = 1;
                }
                return result;
            }
        }

        public void Merge(IControl newControl)
        {
            mergedControls.Add(newControl);
        }
    }
}