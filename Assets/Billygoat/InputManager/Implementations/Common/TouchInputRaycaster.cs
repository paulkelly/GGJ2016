using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Billygoat.InputManager.Model;

namespace Billygoat.InputManager
{
	public class TouchInputRaycaster : AbstractInputRaycaster, IInputRaycaster
	{
		[Inject]
		public IInputTypeModel inputTypeModel { get; set; }

        private ClickCountUtil clickCount = new ClickCountUtil(1f);

		public void DoRaycast()
		{
			if(inputTypeModel.InputType != Billygoat.InputManager.Model.InputType.TouchDevice)
			{
				return;
			}

            clickCount.Update();

			nowSelectedObjects.Clear ();
			blockedTouches.Clear ();

			foreach (Camera camera in GetAllCameras())
			{				
				GetAllTouches (camera);
			}


			foreach(KeyValuePair<int, List<IClickable>> kvp in lastSelectedObjects)
			{
				if(nowSelectedObjects.ContainsKey(kvp.Key))
				{
					//Touch may no longer be over the object
					List<IClickable> touchObjs;
					nowSelectedObjects.TryGetValue(kvp.Key, out touchObjs);
					foreach(IClickable obj in kvp.Value)
					{
						if(!touchObjs.Contains(obj))
						{
							obj.TouchLeave(new PointerEventArgs(kvp.Key));
						}
					}
				}
				else
				{
					//Touch has stopped
					foreach(IClickable obj in kvp.Value)
					{
                        obj.TouchLeave(new PointerEventArgs(kvp.Key));
					}
				}
			}
			
			lastSelectedObjects.Clear ();
			foreach(KeyValuePair<int, List<IClickable>> kvp in nowSelectedObjects)
			{
				lastSelectedObjects.Add(kvp.Key, kvp.Value);
			}
		}

		private List<int> blockedTouches = new List<int>();

		public void GetAllTouches(Camera camera)
		{
			foreach(Touch touch in Input.touches)
			{
				if(!blockedTouches.Contains(touch.fingerId) && !IsTouchOverBlockingObject(touch))
				{
					if(DoTouchRaycast(camera, touch))
					{
						blockedTouches.Add(touch.fingerId);
					}
				}
			}
		}

		private Dictionary<int, List<IClickable>> lastSelectedObjects = new Dictionary<int, List<IClickable>> ();
		private Dictionary<int, List<IClickable>> nowSelectedObjects = new Dictionary<int, List<IClickable>> ();

		public bool DoTouchRaycast(Camera camera, Touch touch)
		{
			var ray = camera.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
			var rayHits = Physics.RaycastAll(ray, 1000f);
			Array.Sort(rayHits, (x, y) => { return x.distance.CompareTo(y.distance); });
			
			foreach(RaycastHit hit in rayHits)
			{
				IClickable obj = hit.transform.GetComponent<IClickable>();
				if(obj != null)
				{
					AddToNowObjects(touch.fingerId, obj);

					if(LastSelectedHasObject(touch.fingerId, obj))
					{
                        obj.TouchEnter(new RaycastHitPointerArgs(touch.fingerId, clickCount.ClickCount, hit, camera));
					}

                    obj.TouchOver(new RaycastHitPointerArgs(touch.fingerId, clickCount.ClickCount, hit, camera));

					if(touch.phase == TouchPhase.Began)
					{
                        obj.TouchDown(new RaycastHitPointerArgs(touch.fingerId, clickCount.ClickCount, hit, camera));
					}
					else if(touch.phase == TouchPhase.Ended)
					{
                        obj.TouchUp(new RaycastHitPointerArgs(touch.fingerId, clickCount.ClickCount, hit, camera));
					}
					
					if(obj.BlocksRaycasts)
					{
						return true;
					}
				}
			}

			return false;
		}

		private void AddToNowObjects(int fingerId, IClickable obj)
		{
			if(nowSelectedObjects.ContainsKey(fingerId))
			{
				List<IClickable> touchObjs;
				nowSelectedObjects.TryGetValue (fingerId, out touchObjs);

				touchObjs.Add (obj);
			}
			else
			{
				List<IClickable> touchObjs = new List<IClickable>();
				touchObjs.Add(obj);
				nowSelectedObjects.Add(fingerId, touchObjs);
			}
		}

		private bool LastSelectedHasObject(int fingerId, IClickable obj)
		{
			if(lastSelectedObjects.ContainsKey(fingerId))
			{
				List<IClickable> touchObjs;
				lastSelectedObjects.TryGetValue(fingerId, out touchObjs);

				if(touchObjs.Contains(obj))
				{
					return true;
				}
			}

			return false;
		}

	}
}