using UnityEngine;
using Billygoat;

namespace GGJ2016
{
    public class MainContext : SignalContext
    {
        public MainContext(MonoBehaviour contextView) : base(contextView)
        {
        }

        public MainContext(MonoBehaviour contextView, bool autoMapping) : base(contextView, autoMapping)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            injectionBinder.Bind<PigeonSignals>().ToSingleton();

        }
    }
}
