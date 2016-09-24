using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Qbert.Scripts.Utils {
	[ExecuteInEditMode]
	public class GuiHandle : MonoBehaviour {
		public const string ON_PRESS = "OnPress";
		public const string ON_TAP = "OnTap";
		public const string ON_DRAG = "OnDrag";
		public const string ON_FLEXIBLE_DRAG = "OnFlexibleDrag";
        public static TouchData currentTouchData;

        private HashSet<GameObject> pressedObjects = new HashSet<GameObject>();
		private Dictionary<int, TouchInfo> store = new Dictionary<int, TouchInfo>();
		public float tapTreshold = 30;
		public bool singleTouchMode = false;
		//public FlexibleCamera flexibleCamera;
		public Camera guiCamera;

		private void Awake()
        {

		}

		private void Update() {
			if (Input.touchCount > 0) {
				ProcessTouches();
			} else {
				ProcessMouse();
			}
		}

		void OnApplicationPause(bool paused) {
			if (paused) {
				TouchInfo[] touchInfos = store.Values.ToArray();
				foreach (var touchInfo in touchInfos) {
					if (touchInfo._pressed) {
						TouchEvent(touchInfo._id, touchInfo._position, false, true);
					}
				}
			}
		}

		private void ProcessTouches() {
			foreach (var touch in Input.touches) {
				bool pressed = touch.phase == TouchPhase.Began;
				bool unpressed = touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
				int id = touch.fingerId;
				TouchEvent(id, touch.position, pressed, unpressed);
				if (singleTouchMode) break;
			}
		}

		private void ProcessMouse() {
			for (int i = 0; i < 3; i++) {
				bool pressed = Input.GetMouseButtonDown(i);
				bool unpressed = Input.GetMouseButtonUp(i);
				int id = -i;
				if (pressed || unpressed || Input.GetMouseButton(i)) {
					TouchEvent(id, Input.mousePosition, pressed, unpressed);
				}
			}
		}


	    private ITouch GetITouchInGameObject(GameObject gObject)
	    {
	        return gObject.GetComponent<ITouch>();
	    }

		private void TouchEvent(int id, Vector2 position, bool pressed, bool unpressed) {
			TouchInfo touchInfo;
			if (!store.TryGetValue(id, out touchInfo)) {
				touchInfo = new TouchInfo {_id = id};
				store.Add(id, touchInfo);
			}
			currentTouchData = touchInfo;
			if (!pressed && !unpressed) {
				Vector2 delta = position - touchInfo.position;
				touchInfo.dragDelta += delta;
				touchInfo._position = position;
				if (!delta.Equals(Vector2.zero)) {
					if (!touchInfo.isDrag) {
						if (touchInfo.capturedObject) {
							if (Mathf.Abs(touchInfo.dragDelta.x) >= tapTreshold || Mathf.Abs(touchInfo.dragDelta.y) >= tapTreshold) {
								touchInfo.isDrag = true;
							}
						}
					}
					if (touchInfo.isDrag) {
                        UnityEngine.Debug.Log("OnDrag");
						OnDrag(touchInfo.capturedObject, touchInfo.dragDelta , touchInfo._capturedITouch);
						touchInfo.dragDelta = Vector2.zero;
					}
				}
			} else {
				touchInfo._position = position;
				var hitGameObject = GetHitGameObject(position);
				if (pressed && hitGameObject && !pressedObjects.Contains(hitGameObject))
                {
					touchInfo._pressed = true;
					touchInfo._capturedObject = hitGameObject;
					touchInfo.dragDelta = Vector2.zero;
					OnPress(hitGameObject, touchInfo._capturedITouch);
				}
				if (unpressed) {
					touchInfo._pressed = false;
					if (touchInfo.capturedObject) {
						OnUnpress(touchInfo.capturedObject, !touchInfo.isDrag, touchInfo._capturedITouch);
					}
                    /*
					if (hitGameObject && hitGameObject != touchInfo.capturedObject) {
						OnUnpress(hitGameObject, false);
					}
                    */
					touchInfo.Reset();
				}
			}
			
		}

		private GameObject GetHitGameObject(Vector2 position) {
			Camera[] allCameras = Camera.allCameras;
			if (allCameras.Length > 1)
            {
				Array.Sort(allCameras, (c1, c2)=>c1.depth.CompareTo(c2.depth));
			}
			for (int i = allCameras.Length-1; i >= 0; i--)
            {
				var camera = allCameras[i];
				GameObject hitGameObject = GetHitGameObject(position, camera);
				if (hitGameObject)
                {
                    if (camera == guiCamera)
                    {
                        return hitGameObject;
                    }
                }
			}
			return null;
		}

		private GameObject GetHitGameObject(Vector2 position, Camera camera) {
			GameObject hitGameObject = null;
			
			var ray = camera.ScreenPointToRay(position);
			RaycastHit2D raycastHit2D = Physics2D.Raycast(ray.origin, ray.direction);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, float.MaxValue)) {
				hitGameObject = hit.collider.gameObject;
			}
			if (raycastHit2D.collider && hit.collider) {
				float length2D = (ray.origin - raycastHit2D.transform.position).magnitude;
				float length3D = (ray.origin - hit.transform.position).magnitude;
				if (length2D < length3D) {
					hitGameObject = raycastHit2D.collider.gameObject;
				} else {
					hitGameObject = hit.collider.gameObject;
				}
			} else if (raycastHit2D.collider) {
				hitGameObject = raycastHit2D.collider.gameObject;
			} else if (hit.collider) {
				hitGameObject = hit.collider.gameObject;
			}
			return hitGameObject;
		}

		private void OnUnpress(GameObject go, bool generateTapEvent , ITouch[] touchs)
        {
			if (pressedObjects.Contains(go))
            {
				pressedObjects.Remove(go);

				if (go)
                {
                    if (touchs != null)
                    {
                        foreach (var touch in touchs)
                        {
                            touch.OnTap();
                        }

                        if (generateTapEvent)
                        {
                            foreach (var touch in touchs)
                            {
                                touch.OnPress(false);
                            }
                        }
                    }
                    else
                    {
                        go.SendMessage(ON_PRESS, false, SendMessageOptions.DontRequireReceiver);
                        if (generateTapEvent)
                        {
                            go.SendMessage(ON_TAP, SendMessageOptions.DontRequireReceiver);
                        }
                    }

					
				}
			}
		}

		private void OnPress(GameObject go , ITouch[] touchs) {
		    if (touchs != null)
		    {
                foreach (var touch in touchs)
                {
                    touch.OnPress(true);
                }
		    }
		    else
            {
                go.SendMessage(ON_PRESS, true, SendMessageOptions.DontRequireReceiver);
            }
			
			pressedObjects.Add(go);
		}

		private void OnDrag(GameObject go, Vector2 delta , ITouch[] touchs) {
            if (touchs != null)
            {
                foreach (var touch in touchs)
                {
                    touch.OnDrag(delta);
                }
            }
            else
            {
                go.SendMessage(ON_DRAG, delta, SendMessageOptions.DontRequireReceiver);
            }
			//go.SendMessage(ON_FLEXIBLE_DRAG, ToAppCoord(delta), SendMessageOptions.DontRequireReceiver);
		}

        /*
		private Vector2 ToAppCoord(Vector2 point) {
			return flexibleCamera.ToAppCoord(point);
		}
        */
		private class TouchInfo: TouchData {
			public int _id;
			public Vector2 _position;
			public Vector2 dragDelta;
            public GameObject _capturedObject;
            public ITouch[]   _capturedITouch;
            public bool isDrag;
			public bool _pressed;

			public void Drag(GameObject go)
			{
			    _capturedITouch = go.GetComponents<ITouch>();
			    if (_capturedObject)
			    {
                    _capturedObject = go;
                    isDrag = true;
                }
			}

			public void Reset() {
				_capturedObject = null;
				isDrag = false;
			}

			public override int id {
				get { return _id; }
			}

			public override Vector2 position {
				get { return _position; }
			}

			public override GameObject capturedObject {
				get { return _capturedObject; }
			}
		}
	}
}