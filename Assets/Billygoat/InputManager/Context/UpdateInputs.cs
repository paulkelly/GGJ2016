using UnityEngine;
using strange.extensions.signal.impl;
using System.Collections.Generic;
using Billygoat.InputManager.Model;

namespace Billygoat.InputManager
{
	public class UpdateInputs : Signal
	{
	}

    public class LateUpdateInputs : Signal
    {
    }

	public class AllInputsSignal : Signal<IMappedInputs>
	{
	}

	public interface IMappedInputs
	{
		Dictionary<int, IControl> ButtonInputs
		{
			get;
		}

		Dictionary<int, ITwoAxisControl> TwoAxisInputs
		{
			get;
		}

		void UseButtonInput (int id);
		
		void UseTwoAxisInput (int id);
	}

	public class MappedInputs : IMappedInputs
	{
		public Dictionary<int, IControl> _buttonInputs = new Dictionary<int, IControl>();
		public Dictionary<int, IControl> ButtonInputs
		{
			get
			{
				return _buttonInputs;
			}

			private set
			{
				_buttonInputs = value;
			}
		}

		public Dictionary<int, ITwoAxisControl> _twoAxisInputs = new Dictionary<int, ITwoAxisControl>();
		public Dictionary<int, ITwoAxisControl> TwoAxisInputs
		{
			get
			{
				return _twoAxisInputs;
			}

			private set
			{
				_twoAxisInputs = value;
			}
		}

		public MappedInputs(Dictionary<int, IControl> buttonInputs, Dictionary<int, ITwoAxisControl> twoAxisInputs)
		{
			ButtonInputs = buttonInputs;
			TwoAxisInputs = twoAxisInputs;
		}

		public void UseButtonInput(int id)
		{
			if(ButtonInputs.ContainsKey(id))
			{
				ButtonInputs.Remove (id);
			}
		}

		public void UseTwoAxisInput(int id)
		{
			if(TwoAxisInputs.ContainsKey(id))
			{
				TwoAxisInputs.Remove (id);
			}
		}
	}
}