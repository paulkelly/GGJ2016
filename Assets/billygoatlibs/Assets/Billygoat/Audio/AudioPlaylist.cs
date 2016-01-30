using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace Billygoat.Audio
{
	public class AudioPlaylist : View
	{
		[Inject (AudioTypes.MusicFade)]
		public MusicFade _MusicFade { get; set; }
		public float fadeTime 
		{ 
			get
			{
				return _MusicFade.FadeTime;
			}

		    set { _MusicFade.FadeTime = value; }
		}

		public AudioSource source1;
		public AudioSource source2;

		private AudioSource next;
		private AudioSource last;

		private AudioClip currentlyPlaying;

	    public void Pause()
	    {
	        source1.Pause();
            source2.Pause();
	    }

        public void Unpause()
        {
            source1.UnPause();
            source2.UnPause();
        }

		public float volume
		{
			get
			{
				return source1.volume;
			}

			set
			{
				source1.volume = value;
				source2.volume = value;
			}
		}

		private bool _locked = false;
		public bool Locked
		{
			get
			{
				return _locked;
			}

			set
			{
				_locked = value;
			}
		}

		private void SetNext()
		{
			if(next == null || next.GetHashCode() == source2.GetHashCode())
			{
				next = source1;
				last = source2;
			}
			else
			{
				next = source2;
				last = source1;
			}
		}

		public void Play(AudioClip clip, bool loop)
		{
            if (Locked)
			{
				return;
			}

			if(clip == null)
			{
				StartCoroutine (AudioCrossFader.FadeOut (next, fadeTime));
				currentlyPlaying = null;
				return;
			}

			if(currentlyPlaying != null && currentlyPlaying.GetHashCode() == clip.GetHashCode())
			{
				return;
			}

			currentlyPlaying = clip;

			if(next == null)
			{
				SetNext();
				next.volume = 0;
				next.clip = clip;
				next.Play();
				next.loop = loop;
				StartCoroutine (AudioCrossFader.FadeIn (next, fadeTime));
			}
			else
			{
				SetNext();
				next.volume = 0;
				next.clip = clip;
				next.Play();
				next.loop = loop;
				StartCoroutine (AudioCrossFader.FadeIn (next, fadeTime));
				StartCoroutine (AudioCrossFader.FadeOut (last, fadeTime));
			}
		}

		public void PlaySequence(AudioClip[] clips, bool loopLast)
		{
			source1.loop = false;
			source2.loop = false;

			StartCoroutine(PlaySequenceCo (clips, loopLast));
		}

	    private IEnumerator fadeingS1;
	    private IEnumerator fadeingS2;

	    private void Update()
	    {
	        if (fadeingS1 != null)
	        {
                fadeingS1.MoveNext();
	        }
            if (fadeingS2 != null)
            {
                fadeingS2.MoveNext();
            }
        }

		public void FadeIn(float fadeTime)
		{
            fadeingS1 = AudioCrossFader.FadeIn (source1, fadeTime);
            fadeingS2 = AudioCrossFader.FadeIn (source2, fadeTime);
		}

	    public void ExitScene(float fadeTime)
	    {
            fadeingS1 = AudioCrossFader.FadeAudio(source1, 0.3f, fadeTime);
            fadeingS2 = AudioCrossFader.FadeAudio(source2, 0.3f, fadeTime);
        }

		public void FadeOut(float fadeTime)
		{
            fadeingS1 = AudioCrossFader.FadeOut (source1, fadeTime);
            fadeingS2 = AudioCrossFader.FadeOut (source2, fadeTime);
		}

		IEnumerator PlaySequenceCo(AudioClip[] clips, bool loopLast)
		{
			SetNext();
			yield return StartCoroutine(LoadFirstClip (next, clips[0]));

			next.Play ();
			next.volume = 0;

			double scheduledTime = AudioSettings.dspTime;
			scheduledTime += System.Convert.ToDouble(clips[0].length);

			StartCoroutine (AudioCrossFader.FadeIn (next, fadeTime));
			yield return StartCoroutine(AudioCrossFader.FadeOut (last, fadeTime));

		    //if (clips.Length > 2)
		    //{
		        
		    //}
			if(clips.Length > 1)
			{
				next.SetScheduledEndTime(scheduledTime);

				SetNext();
				next.volume = 1;
				next.clip = clips[1];
				next.PlayScheduled(scheduledTime);

				StartCoroutine(MakeSourceLoopClip (next, clips[clips.Length-1], loopLast));
			}
		}

		IEnumerator LoadFirstClip(AudioSource source, AudioClip clip)
		{
			source.clip = clip;
			while(source.clip.loadState != AudioDataLoadState.Loaded)
			{
				yield return null;
			}
		}

		IEnumerator MakeSourceLoopClip(AudioSource source, AudioClip clip, bool loop)
		{
			while(source.clip.GetHashCode() != clip.GetHashCode())
			{
				yield return null;
			}

			source.loop = loop;
			currentlyPlaying = clip;
		}
	}
}