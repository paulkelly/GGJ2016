using UnityEngine;
using System.Collections;
using Billygoat.Audio;
using strange.extensions.mediation.impl;

public interface ISFX
{
    void SetClip(AudioClip clip);
    void ClaimAudioSource();
    void ClaimAudioSource(float fadeTime);
    void TryPlay();
    void Play();
    void Play(float fadeTime);
    void Stop();
    void Stop(float fadeTime);
    void Pause();
    void Unpause();
}

public class SFXView : View, ISFX
{
    public AudioClip Clip;

    [HideInInspector]
    public AudioSourceSettings AudioSettings;

    [Inject]
    public IAudioSystem AudioSystem { get; set; }

    private BGAudioSource _audioSource;

    protected override void OnAwake()
    {
        AudioSettings = GetComponent<AudioSourceSettings>();
    }

    private bool HasAudioSource()
    {
        return _audioSource != null && _audioSource.BelongsTo(GetHashCode());
    }

    public void SetClip(AudioClip clip)
    {
        Clip = clip;
    }

    public AudioClip GetClip()
    {
        return Clip;
    }

    public void SetPitch(float value)
    {
        if (HasAudioSource())
        {
            _audioSource.TheAudioSource.pitch = value;
        }
    }


    private float _volume = 1;
    public float Volume
    {
        get
        {
            if (AudioSettings != null)
            {
                return AudioSettings.Volume;
            }

            return _volume;
        }

        set
        {
            if (AudioSettings != null)
            {
                AudioSettings.Volume = value;
            }

            _volume = value;
        }
    }

    public void ClaimAudioSource()
    {
        ClaimAudioSource(0);
    }

    public void ClaimAudioSource(float fadeTime)
    {
        if (!HasAudioSource())
        {
            _audioSource = AudioSystem.GetAudioSource(GetHashCode());
        }

        _audioSource.SetAudioSettings(AudioSettings);
        _audioSource.Volume = Volume;
        _audioSource.FadeIn(fadeTime);
    }

    public void TryPlay()
    {
        if (HasAudioSource())
        {
            Play();
        }
    }

    public void Play()
    {
        Play(0);
    }

    public virtual void Play(float fadeTime)
    {
        if (!HasAudioSource())
        {
            ClaimAudioSource(fadeTime);
        }

        _audioSource.PlaySound(Clip, transform.position);
    }

    public void Stop()
    {
        if (HasAudioSource())
        {
            _audioSource.Return();
            _audioSource = null;
        }
    }

    public void Stop(float fadeTime)
    {
        if (HasAudioSource())
        {
            _audioSource.FadeOut(fadeTime);
            _audioSource = null;
        }
    }

    public void Pause()
    {
        if (HasAudioSource())
        {
            _audioSource.Pause();
        }
    }

    public void Unpause()
    {
        if (HasAudioSource())
        {
            _audioSource.Unpause();
        }
    }

    //public override void OnPause()
    //{
    //    base.OnPause();
    //    Pause();
    //}

    //public override void OnResume()
    //{
    //    if (paused)
    //    {
    //        Unpause();
    //    }
    //    base.OnResume();
    //}
}
