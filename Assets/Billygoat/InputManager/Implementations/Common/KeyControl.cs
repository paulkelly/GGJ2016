using UnityEngine;
using System.Collections.Generic;

namespace Billygoat.InputManager
{
    public class KeyControl : IControl
    {
        KeyCode key;

        List<IControl> mergedControls = new List<IControl>();

        public KeyControl(KeyCode key)
        {
            this.key = key;
        }

        #region IControl implementation

        public ButtonInput id
        {
            get
            {
                throw new System.NotImplementedException();
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

                return Input.GetKeyDown(key);
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

                return Input.GetKeyUp(key);
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

                return Input.GetKey(key);
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

        #endregion


    }
}