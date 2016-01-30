using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[System.Serializable]
public class AudioSourceSettings : MonoBehaviour
{
    public AudioMixerGroup MixerGroup;

    public bool Mute;
    public bool BypassEffects;
    public bool Loop;
    [Range (0, 256)]
    public int Priority = 128;
    [Range(0, 1)]
    public float Volume = 1;
    [Range(-1, 1)]
    public float StereoPan = 0;
    [Range(0, 1)]
    public float SpatialBlend = 1;
    [Range(0, 1.1f)]
    public float ReverbZoneMix = 1;

    [Range(0, 5)]
    public float DopplerLevel = 1;
    [Range(0, 360)]
    public float Spread = 0;

    public AudioRolloffMode VolumeRolloff;
    public float MinDistance = 1;
    public float MaxDistance = 500;
}
