using strange.extensions.signal.impl;

namespace Billygoat.MultiplayerInput
{
    public class MultiInputSignals
    {
        public Signal<PlayerData> PlayerJoined = new Signal<PlayerData>();
        public Signal<PlayerData> PlayerRemoved = new Signal<PlayerData>();

        public Signal<PlayerData> PlayerReady = new Signal<PlayerData>();

        public Signal StartGame = new Signal();
        public Signal EndGame = new Signal();
    }
}