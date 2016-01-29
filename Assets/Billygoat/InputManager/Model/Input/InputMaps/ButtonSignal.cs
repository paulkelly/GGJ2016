using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;

namespace Billygoat.InputManager
{
	public class ButtonSignal
	{
		public Signal OnDown = new Signal ();

		public Signal OnUp = new Signal();

		public Signal OnStay = new Signal();
	}
}