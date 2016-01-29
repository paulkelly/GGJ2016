using UnityEngine;
using strange.extensions.command.impl;

namespace Billygoat.InputManager
{
    public abstract class AbstractInput
    {
        protected float MaxTime;
        private float _tickCountdown;

        public AbstractInput()
        {
            MaxTime = 0;
        }

        public AbstractInput(float delayTime)
        {
            MaxTime = delayTime;
        }

        public abstract void Update();

        public abstract void Merge(AbstractInput newInput);

        protected bool isTick()
        {
            _tickCountdown -= Time.deltaTime;
            if (_tickCountdown > 0)
            {
                return false;
            }

            _tickCountdown = MaxTime;
            return true;
        }

        protected void Release()
        {
            _tickCountdown = 0;
        }
    }
}