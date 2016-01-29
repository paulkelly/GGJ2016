using UnityEngine;
using System.Collections;
using Billygoat.InputManager.Model;
using System;

namespace Billygoat.InputManager
{
	public class ButtonAction : AbstractAction
	{
		public Action OnUp = () => {};
		public Action OnDown = () => {};
		public Action OnStay = () => {};

		private bool handleOnUp;

        public bool HandleOnUp { get { return handleOnUp;} }

		public ButtonAction(ButtonSignal buttonSignal) : this (buttonSignal, 0)
		{
		}

		public ButtonAction() : this (0)
		{
		}
		
		public ButtonAction(ButtonSignal buttonSignal, float delayTime) : base(delayTime)
		{
			MaxTime = delayTime;
			OnUp = buttonSignal.OnUp.Dispatch;
			OnDown = buttonSignal.OnDown.Dispatch;
			OnStay = buttonSignal.OnStay.Dispatch;
		}

		public ButtonAction(float delayTime) : base(delayTime)
		{
			MaxTime = delayTime;
		}

		public void HandleInput(IControl rawInput)
		{
		    InputHandled();

			if(rawInput.ButtonDown)
			{
				handleOnUp = true;
				OnDown();
			}
            //Note - removed the handleOnUp flag here for now.
            // it was causing problems and solving none.
			if(rawInput.ButtonUp)
			{
				handleOnUp = false;
				OnUp();
				Release();
			}
			if(rawInput.ButtonPressed && isTick)
			{
				OnStay();
			}
		}

	    public void PropagateAction(ButtonAction action)
	    {
	        handleOnUp = action.HandleOnUp;
	    }

        public override void Reset()
        {
            base.Reset();
            handleOnUp = false;
        }
	}
}