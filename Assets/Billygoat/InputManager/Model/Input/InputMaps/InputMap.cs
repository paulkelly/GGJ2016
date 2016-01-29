using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Billygoat.InputManager
{
	public class InputMap : IInputMap
	{
		private Dictionary<int, ButtonAction> ButtonInputs = new Dictionary<int, ButtonAction>();
		private Dictionary<int, TwoAxisAction> TwoAxisInputs = new Dictionary<int, TwoAxisAction>();

        public Dictionary<int, ButtonAction> ButtonActions { get { return ButtonInputs; } }

		public void Update()
		{
			foreach(ButtonAction action in ButtonInputs.Values)
			{
				action.Update();
			}
			foreach(TwoAxisAction action in TwoAxisInputs.Values)
			{
				action.Update();
			}
		}

        public void LateUpdate()
        {
            foreach (ButtonAction action in ButtonInputs.Values)
            {
                action.LateUpdate();
            }
            foreach (TwoAxisAction action in TwoAxisInputs.Values)
            {
                action.LateUpdate();
            }
        }

		public bool HandleButtonInput(int id, IControl rawInput)
		{
			if(ButtonInputs.ContainsKey(id))
			{
				ButtonAction action = null;
				ButtonInputs.TryGetValue(id, out action);
				action.HandleInput(rawInput);
				return true;
			}

			return false;
		}

		public bool HandleTwoAxisInput(int id, ITwoAxisControl rawInput)
		{
			if(TwoAxisInputs.ContainsKey(id))
			{
				TwoAxisAction action = null;
				TwoAxisInputs.TryGetValue(id, out action);
				action.HandleInput(rawInput);
				return true;
			}

			return false;
		}

		public void AddButtonAction(int id, ButtonAction action)
		{
			ButtonInputs.Add (id, action);
		}

		public void AddTwoAxisAction(int id, TwoAxisAction action)
		{
			TwoAxisInputs.Add (id, action);
		}

	    public void PropagateButtonAction(int id, ButtonAction action)
	    {
            foreach (ButtonAction buttonActionction in ButtonInputs.Values)
            {
                buttonActionction.PropagateAction(action);
            }
	    }

	    public void Reset()
	    {
            foreach (ButtonAction action in ButtonInputs.Values)
            {
                action.Reset();
            }
            foreach (TwoAxisAction action in TwoAxisInputs.Values)
            {
                action.Reset();
            }
	    }
	}
}