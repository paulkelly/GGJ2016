using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Billygoat.InputManager._View;
using Billygoat.InputManager.Model;

namespace Billygoat.InputManager
{
	public class MouseInputRaycaster : AbstractInputRaycaster, IInputRaycaster
	{
		[Inject]
		public IInputTypeModel inputTypeModel { get; set; }


        private ClickCountUtil clickCount = new ClickCountUtil();

	    //private Type[] ClickableTypes = new Type[]
	    //{
     //       typeof(ItemClickableView),
     //       typeof(IWheelOptionClickableView)
     //   };

		public void DoRaycast()
		{
			if(inputTypeModel.InputType != Billygoat.InputManager.Model.InputType.KeyboardAndMouse)
			{
				return;
			}

		    clickCount.Update();

			nowSelectedObjects.Clear ();

			if(!IsPointerOverBlockingObject ())
			{
				foreach (Camera camera in GetAllCameras())
				{
					if(GetAllRays (camera))
					{ 
						break;
					}
				}

				////TODO: Set curser if object selected
				//if(nowSelectedObjects.Count > 0)
				//{
    //                //SetCurserToHand
				//    if (ClickableTypes.Contains(nowSelectedObjects[0].GetType()))
				//    {
    //                    MouseCursorSignals.CursorToPointer.Dispatch();
    //                }
				//    else
				//    {
    //                    MouseCursorSignals.CursorToArrow.Dispatch();
    //                }

    //            }
				//else
				//{
    //                //SetCurserToArrow
    //                MouseCursorSignals.CursorToArrow.Dispatch();
    //            }

			}
			//else
			//{
   //             MouseCursorSignals.CursorToPointer.Dispatch();
   //         }

			foreach(IClickable obj in lastSelectedObjects)
			{
				if(!nowSelectedObjects.Contains(obj))
				{
					obj.MouseLeave(new PointerEventArgs());
				}
			}

            foreach (IClickable obj in nowSelectedObjects)
		    {
		        if (!lastSelectedObjects.Contains(obj))
		        {
		            PointerEventArgs args;
		            ObjEventArgs.TryGetValue(obj, out args);
                    obj.MouseEnter(args);
		        }
		    }

			lastSelectedObjects.Clear ();
			lastSelectedObjects.AddRange(nowSelectedObjects);
            ObjEventArgs.Clear();
		}

	    private List<IClickable> lastSelectedObjects = new List<IClickable> ();
		private List<IClickable> nowSelectedObjects = new List<IClickable> ();
        private Dictionary<IClickable, PointerEventArgs> ObjEventArgs = new Dictionary<IClickable, PointerEventArgs>();

		public bool GetAllRays(Camera camera)
		{
			var ray = camera.ScreenPointToRay(Input.mousePosition);
			var rayHits = Physics.RaycastAll(ray, 1000f);
			Array.Sort(rayHits, (x, y) => { return x.distance.CompareTo(y.distance); });

			foreach(RaycastHit hit in rayHits)
			{
				IClickable obj = hit.transform.GetComponent<IClickable>();
				if(obj != null)
				{
					nowSelectedObjects.Add(obj);
                    ObjEventArgs.Add(obj, new RaycastHitPointerArgs(MouseButton.Right, clickCount.ClickCount, hit, camera));
                    RaycastHitPointerArgs args = new RaycastHitPointerArgs(MouseButton.None, clickCount.ClickCount, hit, camera);

				    if(Input.GetMouseButtonDown(0))
					{
                        args.MouseButton = MouseButton.Left;
                        obj.MouseDown(args);
					}
					else if(Input.GetMouseButtonUp(0))
					{
                        args.MouseButton = MouseButton.Left;
                        obj.MouseUp(args);
					}
					
					if(Input.GetMouseButtonDown(1))
					{
                        args.MouseButton = MouseButton.Right;
                        obj.MouseDown(args);
					}
					else if(Input.GetMouseButtonUp(1))
					{
                        args.MouseButton = MouseButton.Right;
                        obj.MouseUp(args);
					}
					
					if(Input.GetMouseButtonDown(2))
					{
                        args.MouseButton = MouseButton.Middle;
                        obj.MouseDown(args);
					}
					else if(Input.GetMouseButtonUp(2))
					{
                        args.MouseButton = MouseButton.Middle;
                        obj.MouseUp(args);
					}


                    if (Input.GetMouseButton(0))
                    {
                        args.MouseButton |= MouseButton.Left;
                    }
                    if (Input.GetMouseButton(1))
                    {
                        args.MouseButton |= MouseButton.Right;
                    }
                    if (Input.GetMouseButton(2))
                    {
                        args.MouseButton |= MouseButton.Middle;
                    }


                    if (lastSelectedObjects.Contains(obj))
                    {
                        obj.MouseOver(args);
                    }

                    if (obj.BlocksRaycasts)
                    {
                        return true;
                    }
                    
				}
			}

			return false;
		}




	}
}