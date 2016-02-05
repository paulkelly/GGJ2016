using Billygoat.MultiplayerInput;
using UnityEngine;
using strange.extensions.signal.impl;

namespace GGJ2016
{
    public class PigeonSignals
    {
        public Signal<Transform> PigeonSpawned = new Signal<Transform>();
        public Signal<ScoreData> SetPigeonScore = new Signal<ScoreData>();

        public Signal TurnOffSleezeMusic = new Signal();
        public Signal<PlayerDevice> PlayerWins = new Signal<PlayerDevice>();
    }

    public class ScoreData
    {
        public PlayerDevice Player;
        public float Score;
    }
}