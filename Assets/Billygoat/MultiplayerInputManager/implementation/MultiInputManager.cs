using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InControl;
using strange.extensions.context.api;

namespace Billygoat.MultiplayerInput
{
    public class MultiInputManager : IMultiInputManager
    {
        private const string GAMEOBJECT_NAME = "InControl";

        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }

        [Inject]
        public MultiInputSignals InputSignals { get; set; }

        public List<PlayerDevice> Players = new List<PlayerDevice>();
        public List<PlayerDevice> XInputDevices = new List<PlayerDevice>();

        private int NextId
        {
            get
            {
                for (int i = 0; i < 4; i++)
                {
                    if (!HasPlayerId(i))
                    {
                        return i;
                    }
                }
                return -1;
            }
        }

        private bool HasPlayerId(int id)
        {
            foreach (var player in Players)
            {
                if (player.id == id)
                {
                    return true;
                }
            }

            foreach (var xInput in XInputDevices)
            {
                if (xInput.id == id)
                {
                    return true;
                }
            }

            return false;
        }


        public void TryRegisterDevice(InputDevice device)
        {
            if (IsDeviceAdded(device))
            {
                return;
            }

            int playerId = -1;
            if (isXInputDevice(device))
            {
                XInputDevice xInputDevice = device as XInputDevice;
                playerId = xInputDevice.DeviceIndex;
            }
            else
            {
                playerId = NextId;
            }

            if (playerId >= 0)
            {
                PlayerDevice newPlayer = new PlayerDevice()
                {
                    id = playerId,
                    InControlDevice = device
                };
                AddPlayer(newPlayer);
            }
        }

        private void AddPlayer(PlayerDevice newPlayer)
        {
            Players.Add(newPlayer);
            InputSignals.PlayerJoined.Dispatch(newPlayer);
        }

        public void TryRemovePlayer(PlayerDevice player)
        {
            if (Players.Contains(player))
            {
                Players.Remove(player);
                InputSignals.PlayerRemoved.Dispatch(player);
            }
        }

        private bool IsDeviceAdded(InputDevice inputDevice)
        {
            foreach (var player in Players)
            {
                if (player.InControlDevice == inputDevice)
                {
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            Players.Clear();
        }

        public PlayerDevice[] GetPlayers()
        {
            return Players.ToArray();
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

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            EnableXInput();
#endif

            InControl.InputManager.OnDeviceAttached += DevicesChanged;
            InControl.InputManager.OnDeviceDetached += DevicesChanged;
        }

        private void EnableXInput()
        {
            InControl.InputManager.EnableXInput = true;
            InControl.InputManager.Reload();

            RefreshXinputDevices();
        }

        void DevicesChanged(InputDevice device)
        {
            RefreshXinputDevices();
            RemoveUnknownDevices();
        }

        void RefreshXinputDevices()
        {
            XInputDevices.Clear();
            foreach (var xInputDevice in GetXInputDevices())
            {
                PlayerDevice newPlayer = new PlayerDevice()
                {
                    id = xInputDevice.DeviceIndex,
                    InControlDevice = xInputDevice
                };
                XInputDevices.Add(newPlayer);
            }
        }

        void RemoveUnknownDevices()
        {
            List<PlayerDevice> tmp = new List<PlayerDevice>();
            tmp.AddRange(Players);
            foreach (var player in tmp)
            {
                TryConnectXInputPlayer(player);
            }
        }

        private void TryConnectXInputPlayer(PlayerDevice device)
        {
            foreach (var xinput in XInputDevices)
            {
                if (xinput.id == device.id)
                {
                    if (!xinput.InControlDevice.Equals(device.InControlDevice))
                    {
                        xinput.InControlDevice = device.InControlDevice;
                        InputSignals.PlayerDeviceChanged.Dispatch(xinput);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            //Remove player if not found in XInputDevices as that player has lost connection to its device
            TryRemovePlayer(device);
        }

        public List<XInputDevice> GetXInputDevices()
        {
            List<XInputDevice> xInputDevices = new List<XInputDevice>();

            foreach (var device in InControl.InputManager.Devices)
            {
                if (isXInputDevice(device))
                {
                    xInputDevices.Add(device as XInputDevice);
                }
            }
            return xInputDevices;
        }

        private bool isXInputDevice(InputDevice device)
        {
            return device is XInputDevice;
        }
    }
}