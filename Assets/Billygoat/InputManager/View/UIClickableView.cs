using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.EventSystems;
using Billygoat.InputManager.Model;

namespace Billygoat.InputManager._View
{
    public class UIClickableView : ClickableView, IClickableView, IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        [Inject]
        public IInputTypeModel inputTypeModel { get; set; }

//		Supported Events
//				IPointerEnterHandler - OnPointerEnter - Called when a pointer enters the object
//				IPointerExitHandler - OnPointerExit - Called when a pointer exits the object
//				IPointerDownHandler - OnPointerDown - Called when a pointer is pressed on the object
//				IPointerUpHandler - OnPointerUp - Called when a pointer is released (called on the original the pressed object)
//				IPointerClickHandler - OnPointerClick - Called when a pointer is pressed and released on the same object
//				IInitializePotentialDragHandler - OnInitializePotentialDrag - Called when a drag target is found, can be used to initialise values
//				IBeginDragHandler - OnBeginDrag - Called on the drag object when dragging is about to begin
//				IDragHandler - OnDrag - Called on the drag object when a drag is happening
//				IEndDragHandler - OnEndDrag - Called on the drag object when a drag finishes
//				IDropHandler - OnDrop - Called on the object where a drag finishes
//				IScrollHandler - OnScroll - Called when a mouse wheel scrolls
//				IUpdateSelectedHandler - OnUpdateSelected - Called on the selected object each tick
//				ISelectHandler - OnSelect - Called when the object becomes the selected object
//				IDeselectHandler - OnDeselect - Called on the selected object becomes deselected
//				IMoveHandler - OnMove - Called when a move event occurs (left, right, up, down, ect)
//				ISubmitHandler - OnSubmit - Called when the submit button is pressed
//				ICancelHandler - OnCancel - Called when the cancel button is pressed

        public void Update()
        {
            OnUpdate();

            if (inputTypeModel.InputType == InputType.Gamepad)
            {
                return;
            }

            if (inputTypeModel.InputType == InputType.TouchDevice)
            {
                foreach (int id in TouchIdsOver)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.fingerId == id)
                        {
                            TouchOver(new PointerEventArgs(id, touch.position));
                        }
                    }
                }

                foreach (Touch touch in Input.touches)
                {
                    if (touch.fingerId == PrimaryId && touch.phase == TouchPhase.Moved)
                    {
                        TouchMove(new PointerEventArgs(touch.fingerId, touch.position));
                    }
                }
            }
            else if (inputTypeModel.InputType == InputType.KeyboardAndMouse)
            {
                MouseMove(new PointerEventArgs(Input.mousePosition));

                if (IsMouseOver)
                {
                    MouseOver(new PointerEventArgs(Input.mousePosition));
                }
            }
        }

        public virtual void OnUpdate()
        {
        }

        public void OnPointerDown(PointerEventData mouseData)
        {
            if (inputTypeModel.InputType == InputType.Gamepad)
            {
                return;
            }

            if (mouseData != null)
            {
                if (mouseData.pointerId >= 0)
                {
                    TouchDown(new UnityPointerEventArgs(mouseData));
                }
                else
                {
                    MouseDown(new UnityPointerEventArgs(mouseData));
                }

                if (BlocksRaycasts)
                {
                    mouseData.Use();
                }
            }
        }

        public void OnPointerUp(PointerEventData mouseData)
        {
            if (inputTypeModel.InputType == InputType.Gamepad)
            {
                return;
            }

            if (mouseData != null)
            {
                if (mouseData.pointerId >= 0)
                {
                    TouchUp(new UnityPointerEventArgs(mouseData));
                }
                else
                {
                    MouseUp(new UnityPointerEventArgs(mouseData));
                }

                if (BlocksRaycasts)
                {
                    mouseData.Use();
                }
            }
        }

        public void OnPointerEnter(PointerEventData mouseData)
        {
            if (inputTypeModel.InputType == InputType.Gamepad)
            {
                return;
            }

            if (mouseData != null)
            {
                if (mouseData.pointerId >= 0)
                {
                    TouchEnter(new UnityPointerEventArgs(mouseData));
                }
                else
                {
                    MouseEnter(new UnityPointerEventArgs(mouseData));
                }

                if (BlocksRaycasts)
                {
                    mouseData.Use();
                }
            }
        }

        public void OnPointerExit(PointerEventData mouseData)
        {
            if (inputTypeModel.InputType == InputType.Gamepad)
            {
                return;
            }

            if (mouseData != null)
            {
                if (mouseData.pointerId >= 0)
                {
                    TouchLeave(new UnityPointerEventArgs(mouseData));
                }
                else
                {
                    MouseLeave(new UnityPointerEventArgs(mouseData));
                }

                if (BlocksRaycasts)
                {
                    mouseData.Use();
                }
            }
        }
    }
}