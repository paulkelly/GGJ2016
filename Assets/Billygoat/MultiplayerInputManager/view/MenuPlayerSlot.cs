using UnityEngine;
using strange.extensions.mediation.impl;
using UnityEngine.UI;

namespace Billygoat.MultiplayerInput
{
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
            InputSignals.PlayerJoined.AddListener(OnPlayerJoined);
            InputSignals.PlayerRemoved.AddListener(OnPlayerLeft);

            _image = GetComponent<Image>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            InputSignals.PlayerJoined.RemoveListener(OnPlayerJoined);
            InputSignals.PlayerRemoved.AddListener(OnPlayerLeft);
        }

        private PlayerData _playerData;

        public void OnPlayerJoined(PlayerData playerData)
        {
            if (playerData.id == SlotNumber)
            {
                _playerData = playerData;
            }
        }

        public void OnPlayerLeft(PlayerData playerData)
        {
            if (playerData.id == SlotNumber)
            {
                _playerData = null;
            }
        }

        void Update()
        {
            if (_playerData != null)
            {
                if (_playerData.InControlDevice.Action1.WasReleased)
                {
                    _playerData.Ready = true;

                    InputSignals.PlayerReady.Dispatch(_playerData);
                }
                else if (_playerData.InControlDevice.Action2.WasReleased)
                {
                    if(_playerData.Ready)
                    {
                        _playerData.Ready = false;
                    }
                    else
                    {
                        InputManager.TryRemovePlayer(_playerData);
                    }
                    
                }
            }

            SetImage();
        }

        protected virtual void OnReady()
        {

        }

        protected virtual void SetImage()
        {
            if (_playerData != null)
            {
                if (_playerData.Ready)
                {
                    _image.color = Color.green;
                }
                else
                {
                    _image.color = Color.red;
                }
            }
            else
            {
                _image.color = Color.grey;
            }
        }


    }
}