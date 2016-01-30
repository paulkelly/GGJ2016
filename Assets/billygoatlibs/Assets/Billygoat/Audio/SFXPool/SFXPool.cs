using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;


namespace Billygoat.Audio
{
    public interface ISFXPool
    {
        BGAudioSource GetAudioSource(int ownerHashCode);
        void ReturnAudioSource(BGAudioSource source);

        void FadeIn(float time);
        void FadeOut(float time);

        AudioSourceSettings DefaultAudioSourceSettings { get; }

        MonoBehaviour GetMono();
    }

    public class SFXPool : MonoBehaviour, ISFXPool
    {
        public GameObject BaseAudioSettings;
        public void FadeOut(float time)
        {
            foreach (var source in _inUse)
            {
                source.FadeOut(time);
            }
        }

        public MonoBehaviour GetMono()
        {
            return this;
        }

        private const int DefaultSize = 8;
        private readonly List<BGAudioSource> _audioSourcePool = new List<BGAudioSource>();
        private readonly List<BGAudioSource> _inUse = new List<BGAudioSource>();

        private AudioSourceSettings _defaultAudioSettings;
        public AudioSourceSettings DefaultAudioSourceSettings { get { return _defaultAudioSettings; } }

        [PostConstruct]
        public void Init()
        {
            _defaultAudioSettings = gameObject.AddComponent<AudioSourceSettings>();
            for (int i = 0; i < DefaultSize; i++)
            {
                AddAudioSource();
            }
        }

        void Update()
        {
            BGAudioSource[] tmp = _inUse.ToArray();
            foreach (var source in tmp)
            {
                source.Update();
            }
        }

        private void AddAudioSource()
        {
            GameObject go = Instantiate(BaseAudioSettings);
            go.transform.parent = transform;

            BGAudioSource next = new BGAudioSource(this, go.GetComponent<AudioSource>());

            _audioSourcePool.Add(next);
        }

        public BGAudioSource GetAudioSource(int ownerHashCode)
        {
            BGAudioSource result = GetMatchingSource(ownerHashCode);
            _audioSourcePool.Remove(result);

            result.SetOwner(ownerHashCode);
            _inUse.Add(result);

            return result;
        }

        private BGAudioSource GetMatchingSource(int ownerHashCode)
        {
            if (_audioSourcePool.Count == 0)
            {
                AddAudioSource();
            }
            BGAudioSource firstEmpty = _audioSourcePool[0];
            foreach (var source in _audioSourcePool)
            {
                if (source.OwnerHashCode == ownerHashCode)
                {
                    return source;
                }
            }

            return firstEmpty;
        }

        public void ReturnAudioSource(BGAudioSource source)
        {
            _inUse.Remove(source);
            _audioSourcePool.Add(source);
        }

        public void FadeIn(float time)
        {
            throw new System.NotImplementedException();
        }
    }

    [System.Serializable]
    public class BGAudioSource
    {
        private readonly ISFXPool _pool;
        public AudioSource TheAudioSource { get; set; }
        private float _volume;

        public float Volume
        {
            get
            {
                return _volume;
            }

            set
            {
                _volume = value;
                //TheAudioSource.volume = value;
            }
        }

        public int OwnerHashCode;
        public bool InUse = false;

        public BGAudioSource(ISFXPool pool, AudioSource source)
        {
            _pool = pool;
            TheAudioSource = source;
        }

        public void SetOwner(int hashcode)
        {
            InUse = true;
            OwnerHashCode = hashcode;
        }

        public bool BelongsTo(int hashcode)
        {
            return InUse && hashcode == OwnerHashCode;
        }

        public void SetAudioSettings(AudioSourceSettings settings)
        {
            if (settings == null)
            {
                settings = _pool.DefaultAudioSourceSettings;
            }

            Volume = settings.Volume;
            TheAudioSource.rolloffMode = settings.VolumeRolloff;
            TheAudioSource.bypassEffects = settings.BypassEffects;
            TheAudioSource.loop = settings.Loop;
            TheAudioSource.outputAudioMixerGroup = settings.MixerGroup;
            TheAudioSource.dopplerLevel = settings.DopplerLevel;
            TheAudioSource.maxDistance = settings.MaxDistance;
            TheAudioSource.minDistance = settings.MinDistance;
            TheAudioSource.priority = settings.Priority;
            TheAudioSource.mute = settings.Mute;
            TheAudioSource.reverbZoneMix = settings.ReverbZoneMix;
            TheAudioSource.spatialBlend = settings.SpatialBlend;
            TheAudioSource.spread = settings.Spread;
            TheAudioSource.panStereo = settings.StereoPan;
            TheAudioSource.pitch = 1;
        }

        public void PlaySound(AudioClip clip, Vector3 position, AudioSourceSettings settings)
        {
            SetAudioSettings(settings);

            PlaySound(clip, position);
        }

        public void PlaySound(AudioClip clip, Vector3 position)
        {
            TheAudioSource.transform.position = position;
            if (TheAudioSource.clip == null || TheAudioSource.clip.GetHashCode() != clip.GetHashCode())
            {
                TheAudioSource.clip = clip;
            }
            TheAudioSource.Play();
        }

        public void Pause()
        {
            TheAudioSource.Pause();
        }

        public void Unpause()
        {
            TheAudioSource.UnPause();
        }

        public void Return()
        {
            TheAudioSource.UnPause();
            TheAudioSource.Stop();
            InUse = false;
            _pool.ReturnAudioSource(this);
        }

        public void Update()
        {
            if (co != null)
            {
                co.MoveNext();
            }
        }

        private IEnumerator co;
        private IEnumerator fadeing;
        public void FadeOut(float fadeTime)
        {
            fadeing = AudioCrossFader.FadeAudio(TheAudioSource, 0, fadeTime);
            co = FadeOutEnum();
        }

        public void FadeIn(float fadeTime)
        {
            TheAudioSource.volume = 0;
            fadeing = AudioCrossFader.FadeAudio(TheAudioSource, Volume, fadeTime);
            co = FadeInEnum();
        }

        private IEnumerator FadeOutEnum()
        {
            while (fadeing.MoveNext())
            {
                yield return null;
            }
            Return();
        }

        private IEnumerator FadeInEnum()
        {
            while (fadeing.MoveNext())
            {
                yield return null;
            }
        }
    }
}
