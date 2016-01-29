using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Billygoat.InputManager
{
	public interface IInputMap
	{
        Dictionary<int, ButtonAction> ButtonActions { get; }

		void Update ();
		
		bool HandleButtonInput (int id, IControl rawInput);
		
		bool HandleTwoAxisInput (int id, ITwoAxisControl rawInput);
		
		void AddButtonAction (int id, ButtonAction action);
		
		void AddTwoAxisAction (int id, TwoAxisAction action);

	    void PropagateButtonAction(int id, ButtonAction action);

	    void Reset();
	}
}
