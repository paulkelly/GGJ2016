using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace Billygoat.MultiplayerInput
{
    public class MenuPlayerSlot : View
    {
        [Inject]
        public MultiInputSignals InputSignals { get; set; }
    }
}