using Billygoat.MultiplayerInput;
using UnityEngine;
using strange.extensions.signal.impl;

namespace GGJ2016
{
    public class PigeonSignals
    {
        public Signal<Transform> PigeonSpawned = new Signal<Transform>();
    }
}