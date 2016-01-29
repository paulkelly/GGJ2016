using UnityEngine;
using System.Collections.Generic;

namespace Billygoat.InputManager
{
    public class Keyboard : IKeyboard
    {
        Dictionary<KeyCode, IControl> keyboardControls = new Dictionary<KeyCode, IControl>();

        public Keyboard()
        {
            KeyCode[] keys = (KeyCode[])System.Enum.GetValues(typeof(KeyCode));

            foreach (KeyCode key in keys)
            {
                if (!keyboardControls.ContainsKey(key))
                {
                    keyboardControls.Add(key, new KeyControl(key));
                }
            }
        }

        #region IKeyboard implementation

        public IControl GetKey(KeyCode key)
        {
            return keyboardControls[key];
        }

        #endregion
    }
}