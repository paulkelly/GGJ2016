using Billygoat.InputManager._View;
using strange.extensions.mediation.impl;

namespace Billygoat.InputManager
{
    public class ControlLayoutMediator : Mediator
    {
        [Inject]
        public ControlLayout controlLayout { get; set; }

        [Inject]
        public SetControlLayoutSignal useLayout { get; set; }

        [Inject]
        public UpdateControlLayout UpdateControlLayout { get; set; }

        public override void OnRegister()
        {
            controlLayout.loadLayout.AddListener(UseThisLayout);
            UpdateControlLayout.AddListener(UseThisLayout);
        }

        public override void OnRemove()
        {
            base.OnRemove();

            controlLayout.loadLayout.RemoveListener(UseThisLayout);
            UpdateControlLayout.RemoveListener(UseThisLayout);
        }

        private void UseThisLayout()
        {
            useLayout.Dispatch(controlLayout);
        }
    }
}