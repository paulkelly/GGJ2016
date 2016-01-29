using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Billygoat.InputManager;
using Billygoat.InputManager.Implementations.InControlImpl;
using InControl;
using strange.extensions.context.api;

namespace Billygoat.MultiplayerInput
{
    public class MultiInputManager : IMultiInputManager
    {
        private const string GAMEOBJECT_NAME = "InControl";

        private static int _nextId = 0;
        private static int NextId
        {
            get
            {
                _nextId++;
                return _nextId;
            }
        }

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }

        [Inject]
        public MultiInputSignals InputSignals { get; set; }

        private Dictionary<InputDevice, PlayerData> Devices = new Dictionary<InputDevice, PlayerData>();

        private PlayerData[] AllPlayers
        {
            get { return Devices.Values.ToArray(); }
        }

        [PostConstruct]
        public void SetupInControl()
        {
            InControlManager[] myArray = Resources.FindObjectsOfTypeAll<InControlManager>();
            if (!(myArray.Length > 0))
            {
                GameObject go = new GameObject(GAMEOBJECT_NAME);
                go.AddComponent<InControlManager>();
                go.transform.parent = contextView.transform;
            }

            //InControl.InputManager.OnDeviceDetached
        }

        public void TryRegisterDevice(InputDevice device)
        {
            if (!Devices.ContainsKey(device))
            {
                PlayerData newPlayer = new PlayerData()
                {
                    id = NextId,
                    Device = new InControlDevice(device)
                };
                Devices.Add(device, newPlayer);

                InputSignals.PlayerJoined.Dispatch(newPlayer);
            }
        }

        public PlayerData[] GetPlayers()
        {
            return AllPlayers;
        }
    }
}