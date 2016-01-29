using UnityEngine;

namespace Billygoat.InputManager
{
	public interface IControl
	{
		bool ButtonDown
		{
			get;
		}
		
		bool ButtonUp
		{
			get;
		}
		
		bool ButtonPressed
		{
			get;
		}
		
		float Value
		{
			get;
		}

		void Merge(IControl newControl);
	}
}