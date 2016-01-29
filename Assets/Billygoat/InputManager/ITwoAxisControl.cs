using UnityEngine;

namespace Billygoat.InputManager
{
	public interface ITwoAxisControl
	{
		bool Left
		{
			get;
		}

		bool Right
		{
			get;
		}

		bool Up
		{
			get;
		}

		bool Down
		{
			get;
		}

		Vector2 Value
		{
			get;
		}

		void Merge(ITwoAxisControl newControl);

	}
}