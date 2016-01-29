using UnityEngine;
using System.Collections;

namespace Billygoat.InputManager
{
	public interface IInputContext
	{
		int Priority
		{
			get;
		}

		void Update();

        void LateUpdate();

		bool HandleInput(int id, IControl buttonInput);
		
		bool HandleInput(int id, ITwoAxisControl twoAxisInput);
	}
}