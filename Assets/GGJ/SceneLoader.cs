﻿using UnityEngine;
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
            InputSignals.EndGame.AddListener(EndGame);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (InputSignals != null)
            {
                InputSignals.StartGame.RemoveListener(StartGame);
                InputSignals.EndGame.RemoveListener(EndGame);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            TurnOffFader();
        }

        private bool loading = false;
        void OnLevelWasLoaded(int level)
        {
            loading = false;
            TurnOffFader();
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if(SceneManager.GetActiveScene().name.Equals(Main))
                {
                    if (!loading)
                    {
                        loading = true;
                        StartCoroutine(FadeOutAndLoad("Menu"));
                    }
                }
            }
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
            if (!loading)
            {
                loading = true;
                StartCoroutine(FadeOutAndLoad("MainScene"));
            }
        }

        public void EndGame()
        {
            if (!loading)
            {
                loading = true;
                StartCoroutine(FadeOutAndLoadEnd("Menu"));
            }
        }


    private IEnumerator FadeOutAndLoad(string level, float delay = 1)
        {
            fader.Visible = true;

            yield return new WaitForSeconds(delay);

            SceneManager.LoadScene(level);
        }

        private IEnumerator FadeOutAndLoadEnd(string level)
        {
            yield return new WaitForSeconds(6);

            fader.Visible = true;

            yield return new WaitForSeconds(2);

            SceneManager.LoadScene(level);
        }


    }
}