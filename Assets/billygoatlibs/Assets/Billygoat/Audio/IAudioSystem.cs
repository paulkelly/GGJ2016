
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;

namespace Billygoat.Audio
{
    public interface IAudioSystem
    {
        BGAudioSource GetAudioSource(int ownerHashCode);

        void PlayMusic(AudioPlaylistType clips);

        void FadeOutSFX(float time);
        void FadeInSFX(float time);
        void FadeOutMusic(float time);
        void FadeInMusic(float time);

        void PauseSFX();
        void UnpauseSFX();
        void PauseMusic();
        void UnpauseMusic();

        void ExitScene(float time);
        void OnSceneActivation();

        //void SetMixerSnapshot(MixerSnapshotType snapshot);

        //void SetSoundParamater<TP, TV>(TP paramaterName, TV paramaterType) where TP : struct;
    }

    public class AudioClipType
    {
        public AudioClip Clip { get; set; }
        public Vector3 Position { get; set; }
        public bool Loop { get; set; }
        public AudioMixerGroup MixerGroup { get; set; }
        public float FadeInTime = 0;

        //public AudioClipType(AudioClip clip) : this(clip, null, false)
        //{
        //}

        //public AudioClipType(AudioClip clip, bool loop) : this(clip, null, loop)
        //{
        //}

        //public AudioClipType(AudioClip clip, AudioMixerGroup group) : this(clip, group, false)
        //{
        //}

        //public AudioClipType(AudioClip clip, AudioMixerGroup group, bool loop)
        //{
        //    Clip = clip;
        //    Loop = loop;
        //    MixerGroup = group;
        //}
    }

    public class AudioPlaylistType
    {
        public List<AudioClip> Clips = new List<AudioClip>();
        public bool Loop = true;
        public float CrossfadeTime = -1;
    }

    public class MixerSnapshotType
    {
        public AudioMixerSnapshot Snapshot { get; set; }
        public float TransitionTime { get; set; }

        public MixerSnapshotType(AudioMixerSnapshot snapshot)
        {
            Snapshot = snapshot;
        }
    }
}