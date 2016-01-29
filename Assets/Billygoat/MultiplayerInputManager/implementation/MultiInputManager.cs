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

        private int NextId
        {
            get
            {
                for(int i=1; i<=10; i++)
                {
                    if(!HasPlayerId(i))
                    {
                        return i;
                    }
                }
                return 0;
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

        private PlayerData GetPlayer(int id)
        {
            foreach (var player in AllPlayers)
            {
                if(player.id == id)
                {
                    return player;
                }
            }

            return null;
        }

        private bool HasPlayerId(int id)
        {
            foreach (var player in AllPlayers)
            {
                if (player.id == id)
                {
                    return true;
                }
            }

            return false;
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

            InControl.InputManager.OnDeviceDetached += TryRemovePlayer;
        }

        public void TryRegisterDevice(InputDevice device)
        {
            if (!Devices.ContainsKey(device))
            {
                PlayerData newPlayer = new PlayerData()
                {
                    id = NextId,
                    InControlDevice = device
                };
                Devices.Add(device, newPlayer);

                InputSignals.PlayerJoined.Dispatch(newPlayer);
            }
        }

        public void TryRemovePlayer(InputDevice device)
        {
            if (Devices.ContainsKey(device))
            {
                PlayerData toRemove;
                Devices.TryGetValue(device, out toRemove);
                Devices.Remove(device);

                InputSignals.PlayerRemoved.Dispatch(toRemove);
            }
        }

        public void TryRemovePlayer(PlayerData player)
        {
            TryRemovePlayer(player.InControlDevice);
        }

        public PlayerData[] GetPlayers()
        {
            return AllPlayers;
        }
    }
}