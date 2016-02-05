using UnityEngine;
using System.Collections;
using InControl;
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
                    if (JoinButtonWasPressedOnDevice(device))
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


        bool JoinButtonWasPressedOnDevice(InputDevice inputDevice)
        {
            return inputDevice.MenuWasPressed || inputDevice.Action1.WasReleased;
        }
    }
}