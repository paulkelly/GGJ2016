using UnityEngine;

namespace Billygoat.InputManager.GUI
{
	public enum UITypes
	{
		FadeTime
	}

	public class UIFadeTime
	{
		public float FadeTime;

		public UIFadeTime(float fadeTime)
		{
			FadeTime = fadeTime;
		}
	}
}