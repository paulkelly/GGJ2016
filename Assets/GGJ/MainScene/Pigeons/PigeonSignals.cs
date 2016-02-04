﻿using Billygoat.MultiplayerInput;
using UnityEngine;
using strange.extensions.signal.impl;

namespace GGJ2016
{
    public class PigeonSignals
    {
        public Signal<Transform> PigeonSpawned = new Signal<Transform>();
        public Signal<ScoreData> SetPigeonScore = new Signal<ScoreData>();

        public Signal TurnOffSleezeMusic = new Signal();
        public Signal<PlayerData> PlayerWins = new Signal<PlayerData>();
    }

    public class ScoreData
    {
        public PlayerData Player;
        public float Score;
    }
}