using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;

namespace Billygoat.Audio
{
    public class AudioSignals
    {
        //SFX
        public Signal<AudioClipType> PlaySFX = new Signal<AudioClipType>();



        //Music

        public Signal<AudioPlaylistType> PlayMusicTrack = new Signal<AudioPlaylistType>();

        public Signal LockPlaylist = new Signal();

        public Signal UnlockPlaylist = new Signal();

        public Signal<float> FadeOutMusic = new Signal<float>();

        public Signal<float> FadeInMusic = new Signal<float>();

        public Signal<float> ExitScene = new Signal<float>();

        public Signal PauseMusic = new Signal();

        public Signal UnpauseMusic = new Signal();
    }
}