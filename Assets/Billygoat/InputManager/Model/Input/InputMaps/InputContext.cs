using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Billygoat.InputManager
{
	public abstract class InputContext : BaseInputContext, IComparable
	{
		protected InputMap inputMap = new InputMap();

        public InputMap InputMap { get { return inputMap;} }

		public override void Update()
		{
			inputMap.Update ();
		}

        public override void LateUpdate()
        {
            inputMap.LateUpdate();
        }

		public override bool HandleInput(int id, IControl button)
		{
			return inputMap.HandleButtonInput (id, button);
		}

		public override bool HandleInput(int id, ITwoAxisControl twoAxis)
		{
			return inputMap.HandleTwoAxisInput (id, twoAxis);
		}
	}
}