using JetBrains.Annotations;
using UnityEngine;

namespace Billygoat.InputManager
{
	public interface IInputManager
	{
		IDevice ActiveDevice
		{
			get;
		}

		IKeyboard Keyboard
		{
			get;
		}

	    void RedetectActiveDevice();
	}
}