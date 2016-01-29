using UnityEngine;
using strange.extensions.mediation.impl;
using UnityEngine.UI;

namespace Billygoat.InputManager.GUI
{
	[RequireComponent (typeof(CanvasGroup))]
	public class CanvasGroupFader : View
	{
		[Inject (UITypes.FadeTime)]
		public UIFadeTime _UIFadeTime { get; set; }

		public float fadeTime
		{ 
			get
			{
			    if (_fadeTime >= 0)
			    {
			        return _fadeTime;
			    }
				return _UIFadeTime.FadeTime;
			}
			
			set
			{
                _fadeTime = value;
			}
		}

		public bool InitallyVisible = false;

		public float _fadeTime;

		private CanvasGroup _canvasGroup;

	    protected override void OnAwake()
	    {
            _canvasGroup = GetComponent<CanvasGroup>();
            _visible = InitallyVisible;
            if (_visible)
            {
                _canvasGroup.alpha = 1;
            }
            else
            {
                _canvasGroup.alpha = 0;
            }
	    }

	    protected override void OnStart()
	    {
	        bool visible = (_canvasGroup.alpha > 0) | _visible;
            _canvasGroup.gameObject.SetActive(visible);
            _canvasGroup.blocksRaycasts = visible;
		}

		private bool _visible = false;

		public bool Visible
		{
			get
			{
				return _visible;
			}

			set
			{
			    if(value)
				{
					Enable();
				}
				else
				{
					Disable();
				}
			}
		}

	    public bool AlphaGreaterThanZero()
	    {
	        return _canvasGroup.alpha > 0;
	    }

		private float targetAlpha = 0;
		private float initalAlpha = 0;

		private bool _fadeing = false;
		private float time = 0;

		private void Enable()
		{
			if(!_visible)
			{
				_visible = true;
				targetAlpha = 1;
				time = 0;
				_fadeing = true;
			    if(_canvasGroup != null)
				{
					if(gameObject.activeSelf)
					{
						initalAlpha = _canvasGroup.alpha;
					}
					else
					{
						initalAlpha = 0;
						gameObject.SetActive(true);
						_canvasGroup.alpha = initalAlpha;
					}
				}
			}
		}

		public void InstandOn()
		{
			_visible = true;

			_fadeing = false;
			_canvasGroup.alpha = 1;
			gameObject.SetActive(_visible);
			_canvasGroup.blocksRaycasts = _visible;
		}

        public void InstandOff()
        {
            _visible = false;

            _fadeing = false;
            if (_canvasGroup == null)
            {
                _canvasGroup = GetComponent<CanvasGroup>();
            }
            _canvasGroup.alpha = 0;
            gameObject.SetActive(_visible);
            _canvasGroup.blocksRaycasts = _visible;
        }

		private void Disable()
		{
			if(_visible)
			{
				_visible = false;
				targetAlpha = 0;
				initalAlpha = _canvasGroup.alpha;
				time = 0;
				_fadeing = true;
			}
		}

		void Update()
		{
			if(_fadeing)
			{
			    float percComplete = 1;
                if (fadeTime > 0)
			    {
                    percComplete = time / fadeTime;
                    time += Mathf.Clamp(Time.unscaledDeltaTime, 0, fadeTime / 15f);
                }
				if(percComplete < 1)
				{
					_canvasGroup.alpha = initalAlpha + ((targetAlpha - initalAlpha) * percComplete);
				}
				else
				{
					_fadeing = false;
					_canvasGroup.alpha = targetAlpha;
					gameObject.SetActive(_visible);
					_canvasGroup.blocksRaycasts = _visible;
				}
			}
            
		}
	}
}