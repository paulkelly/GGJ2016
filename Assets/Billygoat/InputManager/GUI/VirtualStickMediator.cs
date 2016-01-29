using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Billygoat.InputManager.GUI.VirtualStick;
using Billygoat.InputManager.Model;

namespace Billygoat.InputManager.GUI.VirtualStick
{
	public class VirtualStickMediator : Mediator
	{
		[Inject]
		public VirtualStickView view { get; set; }

		[Inject]
		public IInputTypeModel inputModel { get; set; }

		protected VirtualTwoAxisControl control = new VirtualTwoAxisControl();

		public override void OnRegister()
		{
			view.JoystickValue.AddListener (JoystickMove);
            view.OnDisabled.AddListener(OnStop);
        }

	    public override void OnRemove()
	    {
	        base.OnRemove();

            view.JoystickValue.RemoveListener(JoystickMove);
            view.OnDisabled.RemoveListener(OnStop);
	    }

	    private void JoystickMove(Vector2 value)
		{
			control.Value = value;
			OnJoystickMove (control);
		}

		protected virtual void OnJoystickMove(ITwoAxisControl control)
		{
		}

	    protected virtual void OnStop()
	    {
	    }
	}
}