﻿using UnityEngine;

namespace Billygoat.MultiplayerInput
{
    public class MultiInputContext : SignalContext
    {
        public MultiInputContext(MonoBehaviour contextView) : base(contextView)
        {
        }

        public MultiInputContext(MonoBehaviour contextView, bool autoMapping) : base(contextView, autoMapping)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();
        }
    }
}