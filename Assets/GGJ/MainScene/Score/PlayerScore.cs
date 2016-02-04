using UnityEngine;
using strange.extensions.mediation.impl;
using UnityEngine.UI;
using Billygoat.MultiplayerInput;
using Billygoat.InputManager.GUI;

namespace GGJ2016
{
    public class PlayerScore : View
    {
        [Inject]
        public IMultiInputManager InputManager { get; set; }

        [Inject]
        public MultiInputSignals InputSignals { get; set; }

        [Inject]
        public PigeonSignals ThePigeonSignals { get; set; }

        public int SlotNumber;

        public RectTransform animated;

        [PostConstruct]
        public void OnConstruct()
        {
            gameObject.SetActive(false);

            InputSignals.PlayerJoined.AddListener(OnPlayerJoined);
            ThePigeonSignals.SetPigeonScore.AddListener(SetScore);

            foreach (var player in InputManager.GetPlayers())
            {
                OnPlayerJoined(player);
            }

            fader = GetComponentInChildren<CanvasGroupFader>(true);
            _scoreBar = GetComponentInChildren<ScoreBar>(true);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            InputSignals.PlayerJoined.RemoveListener(OnPlayerJoined);
            ThePigeonSignals.SetPigeonScore.RemoveListener(SetScore);
        }

        private PlayerData _playerData;
        private ScoreBar _scoreBar;
        private float _score;

        private void SetScore(ScoreData scoreData)
        {
            if(scoreData.Player.id == SlotNumber)
            {
                _score = scoreData.Score;
            }
        }

        public void OnPlayerJoined(PlayerData playerData)
        {
            if (playerData.id == SlotNumber)
            {
                _playerData = playerData;

                gameObject.SetActive(true);
            }
        }

        public void OnPlayerLeft(PlayerData playerData)
        {
            if (playerData.id == SlotNumber)
            {
                _playerData = null;

                gameObject.SetActive(false);
            }
        }

        private CanvasGroupFader fader;

        private Vector3 min = new Vector3(0.9f, 0.9f, 1);
        private Vector3 max = new Vector3(1.1f, 1.1f, 1);
        private float time = 0.5f;
        void Update()
        {
            if (_playerData != null)
            {
                fader.Visible = true;
                _scoreBar.SetScore(_score);

                if(_score > 80)
                {
                    time += Time.deltaTime * 4;
                    animated.localScale = Vector3.Lerp(min, max, Mathf.Abs(Mathf.Cos(time)));
                }
                else
                {
                    animated.localScale = Vector3.one;

                }
            }
            else
            {
                fader.Visible = false;
            }
        }



    }
}