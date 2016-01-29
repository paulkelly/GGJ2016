using UnityEngine;
using System.Collections;
using Billygoat.InputManager;

namespace Billygoat.MultiplayerInput
{
    public class PlayerData
    {
        public int id;
        public IDevice Device;

        public bool Ready = false;
    }
}