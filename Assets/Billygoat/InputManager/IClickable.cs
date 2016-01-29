using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.mediation.api;

namespace Billygoat.InputManager
{
	public interface IClickable
	{
		bool BlocksRaycasts
		{
			get;
		}

		// Mouse

        void MouseDown(PointerEventArgs args);

        void MouseUp(PointerEventArgs args);

        void MouseEnter(PointerEventArgs args);

        void MouseLeave(PointerEventArgs args);

        void MouseMove(PointerEventArgs args);

        void MouseOver(PointerEventArgs args);

		// Touch

        void TouchDown(PointerEventArgs args);

        void TouchUp(PointerEventArgs args);

        void TouchEnter(PointerEventArgs args);

        void TouchLeave(PointerEventArgs args);

        void TouchMove(PointerEventArgs args);

        void TouchOver(PointerEventArgs args);
	}
}