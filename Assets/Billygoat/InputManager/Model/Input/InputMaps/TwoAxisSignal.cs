using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;

namespace Billygoat.InputManager
{
	public class TwoAxisSignal 
	{
		public Signal OnUp = new Signal();
		public Signal OnDown = new Signal();
		public Signal OnLeft = new Signal();
		public Signal OnRight = new Signal();

		public Signal<ITwoAxisControl> OnStay= new Signal<ITwoAxisControl>();
	}
}