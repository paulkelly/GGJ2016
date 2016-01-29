using UnityEngine;

namespace Billygoat.InputManager
{
	public interface IKeyboard
	{
		IControl GetKey (KeyCode key);
	}
}