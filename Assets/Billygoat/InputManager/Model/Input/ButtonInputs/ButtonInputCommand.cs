using UnityEngine;
using System;

namespace Billygoat.InputManager
{
    public abstract class ButtonInputCommand : AbstractInput
    {

        public Action OnUp;
        public Action OnDown;
        public Action OnStay;

        protected IControl input;
        public IControl GetInput()
        {
            return input;
        }

        protected delegate bool ButtonDown();
        protected delegate bool ButtonUp();
        protected delegate bool ButtonPressed();

        protected ButtonDown buttonDown;
        protected ButtonUp buttonUp;
        protected ButtonPressed buttonPressed;

        protected IInputManager inputManager;

        public ButtonInputCommand()
            : base(0)
        {
        }

        public ButtonInputCommand(float delayTime)
            : base(delayTime)
        {
            OnUp = () => { };
            OnDown = () => { };
            OnStay = () => { };

            buttonDown = ReturnFalse;
            buttonUp = ReturnFalse;
            buttonPressed = ReturnFalse;
        }

        private bool ReturnFalse()
        {
            return false;
        }

        public override void Update()
        {
            if (buttonDown())
            {
                OnDown();
            }
            if (buttonPressed() && isTick())
            {
                OnStay();
            }
            if (buttonUp())
            {
                OnUp();
                Release();
            }
        }

        public override void Merge(AbstractInput newInput)
        {
            ButtonInputCommand newButton = (ButtonInputCommand)newInput;
            if (newButton != null)
            {
                input.Merge(newButton.GetInput());
            }
        }
    }
}