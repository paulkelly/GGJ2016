using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Billygoat.InputManager._View;

namespace Billygoat.InputManager
{
	public abstract class AbstractInputRaycaster
	{
		protected Camera[] GetAllCameras()
		{
			Camera[] allCams = Camera.allCameras;
			Array.Sort(allCams, (x, y) => { return y.depth.CompareTo(x.depth); });

			return allCams;
		}

		protected bool IsPointerOverBlockingObject()
		{
			if(EventSystem.current.IsPointerOverGameObject())
			{
				List<RaycastResult> raycastResults = new List<RaycastResult>(); 
				PointerEventData pe = new PointerEventData(EventSystem.current);
				pe.position =  Input.mousePosition;
				EventSystem.current.RaycastAll(pe, raycastResults);
				
				foreach(RaycastResult result in raycastResults)
				{
				    if (HasBlockingRaycast(result))
				    {
				        return true;
				    }
				}
			}

			return false;
		}

		protected bool IsTouchOverBlockingObject(Touch touch)
		{
			// Unity bug [http://issuetracker.unity3d.com/issues/eventsystem-dot-current-dot-ispointerovergameobject-never-returns-true-on-android] 
			// Can't use IsPointerOverGameObject on android
			//if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
			//{
				List<RaycastResult> raycastResults = new List<RaycastResult>(); 
				PointerEventData pe = new PointerEventData(EventSystem.current);
				pe.position =  touch.position;
				EventSystem.current.RaycastAll(pe, raycastResults);
				
				foreach(RaycastResult result in raycastResults)
				{
                    if (HasBlockingRaycast(result))
                    {
                        return true;
                    }
				}
			//}
			
			return false;
		}

        private bool HasBlockingRaycast(RaycastResult result)
	    {
            UIClickableView obj = result.gameObject.GetComponent<UIClickableView>();
            if (obj != null)
            {
                if (obj.BlocksRaycasts)
                {
                    return true;
                }
            }
            else
            {
                // Slightly hacky - used for the icons in the inventory items
                // the icon is the thing that the gui raycast hits, but the
                // UIClickableView belongs to its parent
                Transform parent = result.gameObject.transform.parent;
                if (parent != null)
                {
                    obj = parent.GetComponent<UIClickableView>();
                    if (obj != null)
                    {
                        if (obj.BlocksRaycasts)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
	    }
	}
}