using System;
using Billygoat.Extensions;
using UnityEngine;

namespace Billygoat.InputManager
{
    public class TwoAxisAction : AbstractAction
    {
        private bool _isPressed;
        public Action OnDown = () => { };
        public Action OnLeft = () => { };
        public Action OnRelease = () => { };
        public Action OnRight = () => { };
        public Action<ITwoAxisControl> OnStay = (ITwoAxisControl value) => { };
        public Action OnUp = () => { };

        public TwoAxisAction()
            : this(0)
        {
        }

        public TwoAxisAction(TwoAxisSignal twoAxisSignal)
            : this(twoAxisSignal, 0)
        {
        }

        public TwoAxisAction(float delayTime)
            : base(delayTime)
        {
            MaxTime = delayTime;
        }

        public TwoAxisAction(TwoAxisSignal twoAxisSignal, float delayTime)
            : base(delayTime)
        {
            MaxTime = delayTime;

            OnUp = twoAxisSignal.OnUp.Dispatch;
            OnDown = twoAxisSignal.OnDown.Dispatch;
            OnLeft = twoAxisSignal.OnLeft.Dispatch;
            OnRight = twoAxisSignal.OnRight.Dispatch;
            OnStay = twoAxisSignal.OnStay.Dispatch;
        }

        public void HandleInput(ITwoAxisControl rawInput)
        {
            InputHandled();

            if (_isPressed && rawInput.Value.magnitude < Mathf.Epsilon)
            {
                _isPressed = false;
                return;
            }
            else if (!_isPressed && rawInput.Value.magnitude >= Mathf.Epsilon)
            {
                isTick = true;
                _tickCountdown = MaxTime;
                _isPressed = true;
            }
            if (isTick)
            {
                OnStay(rawInput);
                float angle = rawInput.Value.GetAngleFrom(Vector2.up);

                if (IsJoypadUp(angle))
                {
                    OnUp();
                }
                else if (IsJoypadDown(angle))
                {
                    OnDown();
                }
                else if (rawInput.Left)
                {
                    OnLeft();
                }
                else if (rawInput.Right)
                {
                    OnRight();
                }

            }
        }

        private const int JOYPAD_UPPER_LIMIT = 45;
        private const int JOYPAD_LOWER_LIMIT = 135;

        private static bool IsJoypadDown(float angle)
        {
            return JOYPAD_LOWER_LIMIT < Mathf.Abs(angle);
        }

        private static bool IsJoypadUp(float angle)
        {
            return Mathf.Abs(angle) < JOYPAD_UPPER_LIMIT;
        }



        protected override void Release()
        {
            base.Release();
            OnRelease();
        }
    }
}