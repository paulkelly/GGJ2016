using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.UI;
using Billygoat.InputManager.GUI;

namespace Billygoat.InputManager.GUI
{
	[RequireComponent (typeof(Text))]
	public class TextFader : View
	{
		[Inject (UITypes.FadeTime)]
		public UIFadeTime _UIFadeTime { get; set; }
		
		public float fadeTime
		{ 
			get
			{
				return _UIFadeTime.FadeTime;
			}
			
			set
			{
				_UIFadeTime.FadeTime = value;
			}
		}
		
		private Text _text
		{
			get
			{
				return GetComponent<Text>();
			}
		}
				
//		protected override void OnAwake()
//		{
//			_text = GetComponent<Text>();
//		}
//				
		public Color color
		{
			get
			{
				return _text.color;
			}

			set
			{
				LerpColor(value);
			}
		}

		private bool transitioning = false;
		private Color initalColor;
		private Color targetColor;
		private float time = 0;
		
		private void LerpColor(Color newColor)
		{
			initalColor = _text.color;
			targetColor = newColor;
			time = 0;
			transitioning = true;
		}
		
		void Update()
		{
			if(transitioning)
			{
				time += Time.deltaTime;
				float percComplete = time / fadeTime;
				if(percComplete < 1)
				{
					_text.color = Color.Lerp(initalColor, targetColor, percComplete);
				}
				else
				{
					transitioning = false;
					_text.color = targetColor;
				}
			}
		}
	}
}