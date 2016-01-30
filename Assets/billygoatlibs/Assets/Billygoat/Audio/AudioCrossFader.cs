using UnityEngine;
using System.Collections;

namespace Billygoat.Audio
{
    public class AudioCrossFader
    {
        public static IEnumerator FadeOut(AudioSource source, float fadeTime)
        {
            return FadeAudio(source, 0, fadeTime);
        }

        public static IEnumerator FadeIn(AudioSource source, float fadeTime)
        {
            return FadeAudio(source, 1, fadeTime);
        }

        public static IEnumerator FadeAudio(AudioSource source, float targetVolume, float fadeTime)
        {
            if (source != null)
            {
                float time = 0;
                float startVolume = source.volume;
                float percComplete = 0;

                while (time < fadeTime)
                {
                    time += Mathf.Min(1/30f, Time.unscaledDeltaTime);

                    percComplete = time / fadeTime;
                    source.volume = Mathf.Lerp(startVolume, targetVolume, percComplete);

                    yield return null;
                }

                source.volume = targetVolume;

                if (targetVolume == 0)
                {
                    source.Stop();
                }
            }
        }
    }
}