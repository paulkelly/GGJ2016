using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using Billygoat.Audio;
using Billygoat.MultiplayerInput;
using System.Collections;

namespace GGJ2016
{
    public class EndAnim : View
    {

        [Inject]
        public PigeonSignals ThePigeonSignals { get; set; }

        public Animator _animator;
        public UnityEngine.UI.Image _image;
        public Color[] Colors;

        [PostConstruct]
        public void OnConstruct()
        {
            ThePigeonSignals.PlayerWins.AddListener(EndGame);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            ThePigeonSignals.PlayerWins.RemoveListener(EndGame);
        }

        private void EndGame(PlayerDevice player)
        {
            _image.color = Colors[player.id];
            StartCoroutine(PlayAnim());
        }

        private IEnumerator PlayAnim()
        {
            yield return new WaitForSeconds(3f);

            _animator.ResetTrigger("Win");
            _animator.SetTrigger("Win");
        }
    }
}