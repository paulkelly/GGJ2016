using UnityEngine;

namespace Billygoat.InputManager
{
	public interface IDevice
	{
		IControl GetButtonControl(ButtonInput id);

		ITwoAxisControl GetTwoAxisControl(JoystickInput id);

		IControl Action1
		{
			get;
		}
		IControl Action2
		{
			get;
		}
		IControl Action3
		{
			get;
		}
		IControl Action4
		{
			get;
		}

		IControl LeftStickHorizontal
		{
			get;
		}
		IControl LeftStickVertical
		{
			get;
		}

		IControl RightStickHorizontal
		{
			get;
		}
		IControl RightStickVertical
		{
			get;
		}

		IControl DPadUp
		{
			get;
		}
		IControl DPadDown
		{
			get;
		}
		IControl DPadLeft
		{
			get;
		}
		IControl DPadRight
		{
			get;
		}

		IControl LeftBumper
		{
			get;
		}
		IControl RightBumper
		{
			get;
		}
		IControl LeftTrigger
		{
			get;
		}
		IControl RightTrigger
		{
			get;
		}

		IControl Menu
		{
			get;
		}

		ITwoAxisControl LeftStick
		{
			get;
		}

		ITwoAxisControl RightStick
		{
			get;
		}
		ITwoAxisControl DPad
		{
			get;
		}
	}
}