using UnityEngine;
using Billygoat;

namespace GGJ2016
{
    public class MenuContext : SignalContext
    {
        public MenuContext(MonoBehaviour contextView) : base(contextView)
        {
        }

        public MenuContext(MonoBehaviour contextView, bool autoMapping) : base(contextView, autoMapping)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();

        }
    }
}
