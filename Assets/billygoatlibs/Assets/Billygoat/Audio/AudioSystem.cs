using UnityEngine;
using System.Collections;

namespace Billygoat.Audio
{
    public class AudioSystem : IAudioSystem
    {
        [Inject]
        public AudioSignals TheAudioSignals { get; set; }

        [Inject(AudioTypes.MusicFade)]
        public MusicFade _MusicFade { get; set; }

        [Inject("SFXPool")]
        public GameObject SFXPoolGO { get; set; }

        private SFXPool _SFXPool;

        [PostConstruct]
        public void PostConstruct()
        {
            _SFXPool = SFXPoolGO.GetComponent<SFXPool>();
            _SFXPool.Init();
        }

        public BGAudioSource GetAudioSource(int ownerHashCode)
        {
            return _SFXPool.GetAudioSource(ownerHashCode);
        }

        public void PlayMusic(AudioPlaylistType clips)
        {
            if (clips.CrossfadeTime < 0)
            {
                clips.CrossfadeTime = _MusicFade.FadeTime;
            }
            TheAudioSignals.PlayMusicTrack.Dispatch(clips);
        }

        public void FadeOutSFX(float time)
        {
            _SFXPool.FadeOut(time);
        }

        public void FadeInSFX(float time)
        {
            throw new System.NotImplementedException();
        }

        public void FadeOutMusic(float time)
        {
            TheAudioSignals.FadeOutMusic.Dispatch(time);
        }

        public void FadeInMusic(float time)
        {
            TheAudioSignals.FadeInMusic.Dispatch(time);
        }

        public void PauseSFX()
        {
            throw new System.NotImplementedException();
        }

        public void UnpauseSFX()
        {
            throw new System.NotImplementedException();
        }

        public void PauseMusic()
        {
            throw new System.NotImplementedException();
        }

        public void UnpauseMusic()
        {
            throw new System.NotImplementedException();
        }

        public void ExitScene(float time)
        {
            FadeOutSFX(time);
            TheAudioSignals.ExitScene.Dispatch(time);
        }

        public void OnSceneActivation()
        {
            //MixerSnapshots.Normal.TransitionTo(0f);
        }

        //public void SetMixerSnapshot(MixerSnapshotType snapshot)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void SetSoundParamater<TP, TV>(TP paramaterName, TV paramaterType) where TP : struct
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}