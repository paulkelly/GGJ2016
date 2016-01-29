using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace Billygoat.MultiplayerInput
{
    public class PlayerDetecter : View
    {

        [Inject]
        public IMultiInputManager InputManager { get; set; }

        void Update()
        {
            if (InControl.InputManager.IsSetup)
            {
                foreach (var device in InControl.InputManager.Devices)
                {
                    if (device.MenuWasPressed)
                    {
                        InputManager.TryRegisterDevice(device);
                    }
                }
            }
            else
            {
                Debug.Log("IM not setup");
            }
        }
    }
}