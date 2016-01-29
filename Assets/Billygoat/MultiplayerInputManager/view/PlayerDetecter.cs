using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace Billygoat.MultiplayerInput
{
    public class PlayerDetecter : View
    {

        [Inject]
        public MenuButtonPressed MenuPressedSignal { get; set; }

        void Update()
        {
            foreach (var device in InControl.InputManager.Devices)
            {
                if (device.MenuWasPressed)
                {
                    MenuPressedSignal.Dispatch(device);
                }
            }
        }
    }
}