using UnityEngine;
using System;
using Billygoat.InputManager;

namespace Billygoat.InputManager
{
	public class GamepadButtonInputCommand : ButtonInputCommand
	{
		ButtonInput buttonInput;
						
		public GamepadButtonInputCommand(IInputManager inputManager, ButtonInput buttonInput) : this(inputManager, buttonInput, 0)
		{
		}
		
		public GamepadButtonInputCommand(IInputManager inputManager, ButtonInput buttonInput, float delayTime) : base(delayTime)
		{
			this.buttonInput = buttonInput;
			this.inputManager = inputManager;

			ConfigureInput ();
		}

		public void ConfigureInput()
		{
			input = inputManager.ActiveDevice.GetButtonControl (buttonInput);

			buttonDown = () => {return input.ButtonDown;};
			buttonUp = () => {return input.ButtonUp;};
			buttonPressed = () => {return input.ButtonPressed;};
		}

		private bool ReturnFalse()
		{
			return false;
		}
	}
}