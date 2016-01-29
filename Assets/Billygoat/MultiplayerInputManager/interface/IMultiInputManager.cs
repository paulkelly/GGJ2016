using UnityEngine;
using System.Collections;
using InControl;

namespace Billygoat.MultiplayerInput
{
    public interface IMultiInputManager
    {
        void TryRegisterDevice(InputDevice device);

        PlayerData[] GetPlayers();
    }
}