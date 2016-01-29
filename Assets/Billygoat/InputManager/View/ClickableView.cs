using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using System.Collections.Generic;

namespace Billygoat.InputManager._View
{
	public class ClickableView : View, IClickable, IClickableView
	{
		public bool blockRaycast = true;
		public virtual bool BlocksRaycasts
		{
			get {return blockRaycast;}

			protected set {blockRaycast = value;}
		}

		// General

        private Signal<PointerEventArgs> _onPointerClick = new Signal<PointerEventArgs>();
        public Signal<PointerEventArgs> OnPointerClickSignal { get { return _onPointerClick; } }

        private Signal<PointerEventArgs> _onPointerDown = new Signal<PointerEventArgs>();
        public Signal<PointerEventArgs> OnPointerDownSignal { get { return _onPointerDown; } }

        private Signal<PointerEventArgs> _onPointerUp = new Signal<PointerEventArgs>();
        public Signal<PointerEventArgs> OnPointerUpSignal { get { return _onPointerUp; } }

        private Signal<PointerEventArgs> _onPointerEnter = new Signal<PointerEventArgs>();
        public Signal<PointerEventArgs> OnPointerEnterSignal { get { return _onPointerEnter; } }

        private Signal<PointerEventArgs> _onPointerLeave = new Signal<PointerEventArgs>();
        public Signal<PointerEventArgs> OnPointerLeaveSignal { get { return _onPointerLeave; } }

        private Signal<MoveEventArgs> _onPointerMove = new Signal<MoveEventArgs>();
        public Signal<MoveEventArgs> OnPointerMoveSignal { get { return _onPointerMove; } }

        private Signal<PointerEventArgs> _onPointerOver = new Signal<PointerEventArgs>();
        public Signal<PointerEventArgs> OnPointerOverSignal { get { return _onPointerOver; } }
		
		// Mouse Only

		private Signal<MouseButton>  _onMouseDown = new Signal<MouseButton>  ();
		public Signal<MouseButton>  OnMouseDownSignal{ get{return _onMouseDown;} }

		private Signal<MouseButton>  _onMouseUp = new Signal<MouseButton>  ();
		public Signal<MouseButton>  OnMouseUpSignal{ get{return _onMouseUp;} }

		private Signal _onMouseEnter = new Signal ();
		public Signal  OnMouseEnterSignal{ get{return _onMouseEnter;} }

		private Signal _onMouseLeave = new Signal ();
		public Signal  OnMouseLeaveSignal{ get{return _onMouseLeave;} }

		private Signal<Vector3> _onMouseMove = new Signal<Vector3> ();
		public Signal<Vector3> OnMouseMoveSignal{ get{return _onMouseMove;} }

		private Signal<Vector3> _onMouseOver = new Signal<Vector3> ();
		public Signal<Vector3> OnMouseOverSignal{ get{return _onMouseOver;} }
		
		// Touch Only

		private Signal<int> _onTouchUp = new Signal<int> ();
		public Signal<int>  OnTouchUpSignal{ get{return _onTouchUp;} }

		private Signal<int> _onTouchDown = new Signal<int> ();
		public Signal<int>  OnTouchDownSignal{ get{return _onTouchDown;} }

		private Signal<int> _onTouchEnter = new Signal<int> ();
		public Signal<int>  OnTouchEnterSignal{ get{return _onTouchEnter;} }

		private Signal<int> _onTouchLeave = new Signal<int> ();
		public Signal<int>  OnTouchLeaveSignal{ get{return _onTouchLeave;} }

		private Signal<int, Vector3> _onTouchMove = new Signal<int, Vector3> ();
		public Signal<int, Vector3>  OnTouchMoveSignal{ get{return _onTouchMove;} }

        private Signal<PointerEventArgs> _onTouchOver = new Signal<PointerEventArgs>();
		public Signal<PointerEventArgs> OnTouchOverSignal{ get{return _onTouchOver;} }


		// Mouse

		protected bool IsMouseOver = false;
		private bool wasClick = false;
	    private Vector3 lastPosition;
        public void MouseDown(PointerEventArgs args)
		{
            OnMouseDownSignal.Dispatch(args.MouseButton);
            OnPointerDownSignal.Dispatch(args);
			wasClick = true;
		}

        public void MouseUp(PointerEventArgs args)
		{
            OnMouseUpSignal.Dispatch(args.MouseButton);
            OnPointerUpSignal.Dispatch(args);
			if(wasClick)
			{
                OnPointerClickSignal.Dispatch(args);
				wasClick = false;
			}
		}

        public void MouseEnter(PointerEventArgs args)
		{
			IsMouseOver = true;

			OnMouseEnterSignal.Dispatch ();
            OnPointerEnterSignal.Dispatch(args);

            lastPosition = args.ScreenPosition;
		}

        public void MouseLeave(PointerEventArgs args)
		{
			IsMouseOver = false;
			wasClick = false;

			OnMouseLeaveSignal.Dispatch ();
            OnPointerLeaveSignal.Dispatch(args);
		}

        public void MouseMove(PointerEventArgs args)
		{
            OnMouseMoveSignal.Dispatch(args.Position);
		}

        public void MouseOver(PointerEventArgs args)
		{
            OnMouseOverSignal.Dispatch(args.Position);
            OnPointerOverSignal.Dispatch(args);


            Vector3 movement = args.ScreenPosition - lastPosition;
            OnPointerMoveSignal.Dispatch(new MoveEventArgs(args, new Vector2(movement.x, movement.y)));
            

            lastPosition = args.ScreenPosition;
		}
		
		// Touch

		protected List<int> TouchIdsOver = new List<int> ();
        protected bool IsTouchIdOver(int id)
		{
			return TouchIdsOver.Contains (id);
		}
		protected int PrimaryId = -1;
        public void TouchDown(PointerEventArgs args)
		{
			if(PrimaryId < 0)
			{
                PrimaryId = args.Id;
			}

            TouchEnter(args);

            OnTouchDownSignal.Dispatch(args.Id);

            if (args.Id == PrimaryId)
			{
				wasClick = true;
                OnPointerDownSignal.Dispatch(args);
			}
		}

        public void TouchUp(PointerEventArgs args)
		{
            OnTouchUpSignal.Dispatch(args.Id);

            if (args.Id == PrimaryId)
			{
                OnPointerUpSignal.Dispatch(args);
				PrimaryId = -1;

				if(wasClick)
				{
                    OnPointerClickSignal.Dispatch(args);
					wasClick = false;
				}

                OnPointerLeaveSignal.Dispatch(args);
			}

            TouchLeave(args);
		}

        public void TouchEnter(PointerEventArgs args)
		{
            TouchIdsOver.Add(args.Id);
            OnTouchEnterSignal.Dispatch(args.Id);

            if (args.Id == PrimaryId)
			{
                OnPointerEnterSignal.Dispatch(args);
			}
		}

        public void TouchLeave(PointerEventArgs args)
		{
            TouchIdsOver.Remove(args.Id);
            OnTouchLeaveSignal.Dispatch(args.Id);

            if (args.Id == PrimaryId)
			{
                OnPointerLeaveSignal.Dispatch(args);
				wasClick = false;
			}
		}

        public void TouchMove(PointerEventArgs args)
		{
            OnTouchMoveSignal.Dispatch(args.Id, args.Position);

            if (args.Id == PrimaryId)
			{
                //OnPointerMoveSignal.Dispatch(args);
			}
		}

        public void TouchOver(PointerEventArgs args)
		{
            OnTouchOverSignal.Dispatch(args);

            if (args.Id == PrimaryId)
			{
                OnPointerOverSignal.Dispatch(args);
			}
		}

	}
}