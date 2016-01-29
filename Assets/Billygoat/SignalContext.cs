using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

namespace Billygoat
{
    public class SignalContext : MVCSContext
    {
        public SignalContext(MonoBehaviour contextView)
            : base(contextView)
        {
        }

        public SignalContext(MonoBehaviour contextView, bool autoMapping)
            : base(contextView, autoMapping)
        {
        }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();

            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>();
        }

        public override void Launch()
        {
            base.Launch();
        }
    }
}