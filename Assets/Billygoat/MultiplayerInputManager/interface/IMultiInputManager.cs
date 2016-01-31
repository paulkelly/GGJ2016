using UnityEngine;
using System.Collections;
using InControl;

namespace Billygoat.MultiplayerInput
{
    public interface IMultiInputManager
    {
        void TryRegisterDevice(InputDevice device);
        void TryRemovePlayer(PlayerData player);

        void Reset();

        PlayerData[] GetPlayers();
    }
}