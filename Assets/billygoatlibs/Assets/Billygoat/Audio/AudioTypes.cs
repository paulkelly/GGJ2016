using UnityEngine;
using System.Collections;

namespace Billygoat.Audio
{
	public enum AudioTypes
	{
		MusicFade
	}

	public class MusicFade
	{
		public float FadeTime;

		public MusicFade(float fadeTime)
		{
			FadeTime = fadeTime;
		}
	}
}