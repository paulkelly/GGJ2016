using InControl;
using strange.extensions.signal.impl;

namespace Billygoat.MultiplayerInput
{
    public class MultiInputSignals
    {
        public Signal<PlayerData> PlayerJoined = new Signal<PlayerData>();
        public Signal<PlayerData> PlayerRemoved = new Signal<PlayerData>();
    }
}