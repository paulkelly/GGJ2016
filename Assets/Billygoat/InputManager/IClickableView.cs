using System;
using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;

namespace Billygoat.InputManager
{
    [Flags]
	public enum MouseButton
	{
        None = 0 << 0,
		Left = 1 << 0,
		Middle = 1 << 1,
		Right = 1 << 2
	}

	public interface IClickableView
	{
		// General

		Signal<PointerEventArgs>  OnPointerClickSignal{ get; }
        Signal<PointerEventArgs> OnPointerDownSignal { get; }
        Signal<PointerEventArgs> OnPointerUpSignal { get; }
        Signal<PointerEventArgs> OnPointerEnterSignal { get; }
        Signal<PointerEventArgs> OnPointerLeaveSignal { get; }
        Signal<MoveEventArgs> OnPointerMoveSignal { get; }
        Signal<PointerEventArgs> OnPointerOverSignal { get; }

		// Mouse Only

		Signal<MouseButton>  OnMouseDownSignal{ get; }
		Signal<MouseButton>  OnMouseUpSignal{ get; }
		Signal  OnMouseEnterSignal{ get; }
		Signal  OnMouseLeaveSignal{ get; }
		Signal<Vector3> OnMouseMoveSignal{ get; }
		Signal<Vector3> OnMouseOverSignal{ get; }

		// Touch Only

		Signal<int>  OnTouchUpSignal{ get; }
		Signal<int>  OnTouchDownSignal{ get; }
		Signal<int>  OnTouchEnterSignal{ get; }
		Signal<int>  OnTouchLeaveSignal{ get; }
		Signal<int, Vector3>  OnTouchMoveSignal{ get; }
        Signal<PointerEventArgs> OnTouchOverSignal { get; }
	}
}