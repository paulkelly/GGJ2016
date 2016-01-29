using UnityEngine;
using strange.extensions.mediation.impl;
using Billygoat.InputManager;
using Billygoat.InputManager.Model;

namespace Billygoat.InputManager._View
{
	public class InputsUpdaterMediator : Mediator
	{
		[Inject]
		public InputsUpdaterView view { get; set; }

		[Inject]
		public UpdateInputs updateInputs { get; set;}

        [Inject]
        public LateUpdateInputs lateUpdateInputs { get; set; }

		[Inject]
		public IInputTypeModel inputType { get; set; }

		[Inject]
		public SetInputType inputTypeChanged { get; set; }

		public override void OnRegister ()
		{
			view.updateInputs.AddListener (OnUpdate);
			inputTypeChanged.AddListener (InputTypeChanged);
			InputTypeChanged (inputType.InputType);
		}

		void OnUpdate()
		{
			updateInputs.Dispatch ();
		}

	    void LateUpdate()
	    {
	        lateUpdateInputs.Dispatch();
	    }

		void InputTypeChanged(InputType type)
		{
			view.SetControls (type);
		}
	}
}