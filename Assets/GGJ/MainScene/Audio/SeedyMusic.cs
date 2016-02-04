using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using Billygoat.Audio;
using Billygoat.MultiplayerInput;

namespace GGJ2016
{
    public class SeedyMusic : View
    {
        [Inject]
        public AudioSignals TheAudioSignals { get; set; }

        [Inject]
        public PigeonSignals ThePigeonSignals { get; set; }

        public AudioClip MainTheme;

        private AudioSource source;

        private float MaxScore = 0;
        private Dictionary<int, float> scores = new Dictionary<int, float>();

        private float MaxVolume = 1;
        private float _targetMaxVolume = 1;

        public AnimationCurve Curve;

        [PostConstruct]
        public void OnConstruct()
        {
            source = GetComponent<AudioSource>();

            AudioPlaylistType music = new AudioPlaylistType();
            music.Clips.Add(MainTheme);
            TheAudioSignals.PlayMusicTrack.Dispatch(music);

            ThePigeonSignals.TurnOffSleezeMusic.AddListener(EndGame);
            ThePigeonSignals.SetPigeonScore.AddListener(ListenScores);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            ThePigeonSignals.TurnOffSleezeMusic.RemoveListener(EndGame);
            ThePigeonSignals.SetPigeonScore.RemoveListener(ListenScores);
        }

        private void Update()
        {
            MaxScore = 0;
            foreach (float v in scores.Values)
            {
                MaxScore = Mathf.Max(MaxScore, v);
            }

            source.volume = Curve.Evaluate(Mathf.Clamp((MaxScore / 80), 0, MaxVolume));
            MaxVolume = Mathf.Lerp(MaxVolume, _targetMaxVolume, Time.deltaTime);
        }

        private void EndGame()
        {
            _targetMaxVolume = 0;
        }

        private void ListenScores(ScoreData data)
        {
            int key = data.Player.id;
            if (scores.ContainsKey(key))
            {
                scores[key] = data.Score;
            }
            else
            {
                scores.Add(key, data.Score);
            }
        }
    }
}