using UnityEngine;
using System;

namespace Billygoat.InputManager
{
	public class TwoAxisInputCommand : AbstractInput
	{
		ITwoAxisControl input;
		public ITwoAxisControl GetInput()
		{
			return input;
		}

		public Action<ITwoAxisControl> OnUpdate;
		public Action<ITwoAxisControl> OnStay;
		public Action<ITwoAxisControl> OnEnter;
		public Action<ITwoAxisControl> OnLeave;

		private bool _hasEntered = false;

		public TwoAxisInputCommand(IInputManager inputManager, JoystickInput joystickInput) : this(inputManager, joystickInput, 0)
		{
		}

		public TwoAxisInputCommand(IInputManager inputManager, JoystickInput joystickInput, float delayTime) : base(delayTime)
		{
			input = inputManager.ActiveDevice.GetTwoAxisControl (joystickInput);

			OnUpdate = (ITwoAxisControl value) => {};
			OnStay = (ITwoAxisControl value) => {};
			OnEnter = (ITwoAxisControl value) => {};
			OnLeave = (ITwoAxisControl value) => {};
		}

		public override void Update()
		{			
			OnUpdate(input);
			
			if (input.Value.magnitude > Mathf.Epsilon)
			{
				if (!_hasEntered)
				{
					_hasEntered = true;
					OnEnter(input);
				}
				if (isTick())
				{
					OnStay(input);
				}
			}
			else if (_hasEntered)
			{
				_hasEntered = false;
				OnLeave(input);
				Release();
			}
		}

		public override void Merge(AbstractInput newInput)
		{
			TwoAxisInputCommand newTwoAxis = (TwoAxisInputCommand)newInput;
			if(newTwoAxis != null)
			{
				input.Merge (newTwoAxis.GetInput());
			}
		}

	}
	
}