using UnityEngine;
using System.Collections;
using System;

namespace Billygoat.InputManager
{
    public abstract class AbstractAction
    {
        protected float MaxTime;
        protected float _tickCountdown;

        protected bool isTick = false;

        public Action OnInputStopped = () => {};
        public Action OnNoInput = () => {};

        private bool _inputReceived = false;
        private bool _inputReceivedLastFrame = false;

        public AbstractAction()
        {
            MaxTime = 0;
        }

        public AbstractAction(float delayTime)
        {
            MaxTime = delayTime;
        }

        public void Update()
        {
            _tickCountdown -= Time.unscaledDeltaTime;
            if (_tickCountdown > 0)
            {
                isTick = false;
            }
            else
            {
                _tickCountdown = MaxTime;
                isTick = true;
            }

            _inputReceivedLastFrame = _inputReceived;
            _inputReceived = false;
        }

        public void LateUpdate()
        {
            if (!_inputReceived)
            {
                if (_inputReceivedLastFrame)
                {
                    OnInputStopped();
                }
                OnNoInput();
            }
        }

        protected void InputHandled()
        {
            _inputReceived = true;
        }

        protected virtual void Release()
        {
            _tickCountdown = 0;
        }

        public virtual void Reset()
        {
            Release();
        }
    }
}