using UnityEngine;
using System.Collections;
using Billygoat.InputManager._View;
using strange.extensions.signal.impl;
using strange.extensions.mediation.impl;
using Billygoat.Extensions;
using Billygoat.InputManager.Model;

namespace Billygoat.InputManager.GUI.VirtualStick
{
    [RequireComponent(typeof (RectTransform))]
    public class VirtualStickView : UIClickableView
    {
        private float stickRadiusOnScreen = 40;
        private RectTransform rectTransform;

        public RectTransform image;

        public Vector2 _value;

        public Vector2 Value
        {
            get { return _value; }
        }

        public CanvasGroupFader canvasGroup;
        public VirtualStickLookat lookAt;
        public Transform VirtualStick;
        public Transform stick;
        private bool _enabled = false;
        private bool needsReset = false;

        internal Signal OnDisabled = new Signal();
        internal Signal<Vector2> JoystickValue = new Signal<Vector2>();

        protected override void OnStart()
        {
            base.OnStart();

            rectTransform = GetComponent<RectTransform>();

            Disable(new PointerEventArgs());

            OnPointerDownSignal.AddListener(Enable);
            OnPointerUpSignal.AddListener(Disable);
            OnPointerMoveSignal.AddListener(SetIputPosition);

            Rect stickRect = image.GetScreenRect();
            stickRadiusOnScreen = stickRect.width/2f;
        }

        public void Enable(PointerEventArgs args)
        {
            CenterPositionOnInput();
            _enabled = true;
            needsReset = true;
            StartCoroutine("ShowAfterDelay");
        }

        public void Disable(PointerEventArgs args)
        {
            canvasGroup.Visible = false;
            JoystickValue.Dispatch(Vector2.zero);
            _enabled = false;

            OnDisabled.Dispatch();
            StopCoroutine("ShowAfterDelay");
            StartCoroutine(UnblockOnNextFrame());
        }

        private float time = 0;

        private IEnumerator ShowAfterDelay()
        {
            time = 0;
            while (time < 0.2f)
            {
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;
            canvasGroup.Visible = true;
            while (time < 0.1f)
            {
                time += Time.deltaTime;
                yield return null;
            }
            BlocksRaycasts = true;
        }

        private IEnumerator UnblockOnNextFrame()
        {
            yield return new WaitForEndOfFrame();
            BlocksRaycasts = false;
        }

        public void CenterPositionOnInput()
        {
            SetPosition(InputPosition);
            _value = Vector3.zero;
            lookAt.ResetPosition();
        }

        private void SetPosition(Vector3 pos)
        {
            Rect stickRect = image.GetScreenRect();

            float stickWidth = stickRect.width;
            float stickHeight = stickRect.height;
            Rect screenRect = new Rect((stickWidth/2), (stickHeight/2), Screen.width - stickWidth,
                Screen.height - stickHeight);

            Vector3 nearestScreenPos = pos;

            nearestScreenPos = new Vector3(Mathf.Min(nearestScreenPos.x, screenRect.xMax),
                Mathf.Min(nearestScreenPos.y, screenRect.yMax), nearestScreenPos.z);
            nearestScreenPos = new Vector3(Mathf.Max(nearestScreenPos.x, screenRect.xMin),
                Mathf.Max(nearestScreenPos.y, screenRect.yMin), nearestScreenPos.z);

            rectTransform.position = nearestScreenPos;
        }

        private void Move(Vector3 pos)
        {
            SetPosition(rectTransform.position + pos);
        }

        public override void OnUpdate()
        {
            VirtualStick.gameObject.SetActive(canvasGroup.enabled);

            if (_enabled)
            {
                // Find the 2axis value and move the stick if touch has moved beyond the edge of the stick
                UpdateValueAndPosition();

                // Move the lookat transform to reflect the latest 2axis value
                lookAt.UpdatePosition(Value);

                stick.rotation = Quaternion.LookRotation(lookAt.transform.position - stick.transform.position,
                    Vector3.forward);

                JoystickValue.Dispatch(Value);
            }
        }

        private void UpdateValueAndPosition()
        {
            // Avoiding a race condition, dont start updaing the sticks position until it has been centered
            if (needsReset)
            {
                _value = Vector3.zero;
                return;
            }

            // Calculate the distance vector between the touch and the stick, use the sticks radius to normalize the vector
            Vector3 pos = InputPosition - rectTransform.position;
            Vector2 result = new Vector2(pos.x, pos.y);
            result /= stickRadiusOnScreen;
            if (result.magnitude > 1)
            {
                // Move the stick to follow the touch if it has gone beyound the deadzone
                float followDeadzone = 1.3f;
                if (result.magnitude > followDeadzone)
                {
                    Move(pos - pos.normalized*(stickRadiusOnScreen*followDeadzone));
                }

                result.Normalize();
            }

            if (result.magnitude > 0.1f)
            {
                _value = result;
            }
            else
            {
                _value = Vector3.zero;
            }
        }

        // Touch/Mouse position updated by events
        private Vector3 InputPos = Vector3.zero;

        private Vector3 InputPosition
        {
            get { return InputPos; }
        }

        private void SetIputPosition(PointerEventArgs args)
        {
            InputPos = args.Position;
            if (needsReset)
            {
                CenterPositionOnInput();
                needsReset = false;
            }
        }
    }
}