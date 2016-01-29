using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using System;
using UnityEngine.UI;
using Billygoat.InputManager.Model;

namespace Billygoat.InputManager._View
{
	public class InputsUpdaterView : View
	{
		[Inject]
		public IInputRaycaster inputRaycaster { get; set; }

        [Inject]
        public IInputManager InputManager { get; set; }

        protected InputType inputType;

		internal Signal updateInputs = new Signal ();

		public void SetControls(InputType type)
		{
			inputType = type;
		}

		void Update()
		{
			OnUpdate ();

            InputManager.RedetectActiveDevice();
        }

		protected virtual void OnUpdate()
		{
			inputRaycaster.DoRaycast ();
			updateInputs.Dispatch ();
		}
	}
}