using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Billygoat.MultiplayerInput;
using UnityEngine.SceneManagement;
using Billygoat.InputManager.GUI;

namespace GGJ2016
{
    public class SceneLoader : View
    {
        public const string Menu = "Menu";
        public const string Main = "MainScene";

        private CanvasGroupFader fader;

        [Inject]
        public MultiInputSignals InputSignals { get; set; }

        [PostConstruct]
        public void OnConstruct()
        {
            InputSignals.StartGame.AddListener(StartGame);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (InputSignals != null)
            {
                InputSignals.StartGame.RemoveListener(StartGame);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            TurnOffFader();
        }

        void OnLevelWasLoaded(int level)
        {
            TurnOffFader();
        }

        private void TurnOffFader()
        {
            if (fader == null)
            {
                fader = GetComponentInChildren<CanvasGroupFader>(true);
            }

            fader.Visible = false;
        }

        public void StartGame()
        {
            StartCoroutine(FadeOutAndStartGame());
        }

        private IEnumerator FadeOutAndStartGame()
        {
            fader.Visible = true;

            yield return new WaitForSeconds(1);

            SceneManager.LoadScene("MainScene");
        }


    }
}