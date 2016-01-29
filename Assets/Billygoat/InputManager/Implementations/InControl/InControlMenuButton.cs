using UnityEngine;
using InControl;

namespace Billygoat.InputManager
{
	public class InControlMenuButton : IControl
	{
		ButtonInput handle;
		InputDevice inControlDevice;
		
		public InControlMenuButton(ButtonInput input, InputDevice device)
		{
			handle = input;
			inControlDevice = device;
		}
		
		#region IControl implementation
		public ButtonInput id
		{
			get
			{
				return handle;
			}
		}

		public bool ButtonDown
		{
			get
			{
				return inControlDevice.MenuWasPressed;
			}
		}

		public bool ButtonUp
		{
			get
			{
				return false;
			}
		}

		public bool ButtonPressed
		{
			get
			{
				return false;
			}
		}

		public float Value
		{
			get
			{
				float result = 0;
				if(ButtonDown)
				{
					result = 1;
				}
				return result;
			}
		}

		public void Merge(IControl newControl)
		{

		}
		#endregion
	}
}