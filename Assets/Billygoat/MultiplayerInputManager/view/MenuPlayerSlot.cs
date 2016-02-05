using System.Collections.Generic;
using UnityEngine;
using strange.extensions.mediation.impl;
using UnityEngine.UI;
using Billygoat.InputManager.GUI;

namespace Billygoat.MultiplayerInput
{
    public enum PlayerStatus
    {
        Disconnected,
        Connected,
        Ready
    }

    public class MenuPlayerSlot : View
    {
        [Inject]
        public IMultiInputManager InputManager { get; set; }

        [Inject]
        public MultiInputSignals InputSignals { get; set; }

        public int SlotNumber;
        private Image _image;

        [PostConstruct]
        public void OnConstruct()
        {
            _image = GetComponent<Image>();

            InputSignals.PlayerJoined.AddListener(OnPlayerJoined);
            InputSignals.PlayerRemoved.AddListener(OnPlayerLeft);

            OnStatusUpdated(Status);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            InputSignals.PlayerJoined.RemoveListener(OnPlayerJoined);
            InputSignals.PlayerRemoved.AddListener(OnPlayerLeft);
        }

        private PlayerDevice _playerDevice;
        private bool _ready = false;
        private PlayerStatus _status = PlayerStatus.Disconnected;

        private PlayerStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnStatusUpdated(_status);
            }
        }

        public void OnPlayerJoined(PlayerDevice playerDevice)
        {
            if (playerDevice.id == SlotNumber)
            {
                _playerDevice = playerDevice;
                Status = PlayerStatus.Connected;
            }
        }

        public void OnPlayerLeft(PlayerDevice playerDevice)
        {
            if (playerDevice.id == SlotNumber)
            {
                _playerDevice = null;
                Status = PlayerStatus.Disconnected;
            }
        }

        void Update()
        {
            if (_playerDevice != null)
            {
                if (_playerDevice.InControlDevice.Action1.WasReleased)
                {
                    _ready = true;
                    Status = PlayerStatus.Ready;
                }
                else if (_playerDevice.InControlDevice.Action2.WasReleased)
                {
                    if(_ready)
                    {
                        _ready = false;
                        Status = PlayerStatus.Connected;
                    }
                    else
                    {
                        InputManager.TryRemovePlayer(_playerDevice);
                    }
                }
            }

            if(_playerDevice != null)
            {
                _playerDevice.Ready = _ready;

                bool allReady = true;
                foreach (var player in InputManager.GetPlayers())
                {
                    allReady &= player.Ready;
                }

                if (allReady)
                {
                    InputSignals.StartGame.Dispatch();
                }
            }

        }

        public Color ready;
        public Color notReady;
        public CanvasGroupFader AButton;
        protected virtual void OnStatusUpdated(PlayerStatus status)
        {
            if (_playerDevice != null)
            {
                if (_ready)
                {
                    _image.color = ready;
                    AButton.Visible = false;
                }
                else
                {
                    _image.color = notReady;
                    AButton.Visible = true;
                }
            }
            else
            {
                _image.color = Color.black;
                AButton.Visible = false;
            }

        }


    }
}