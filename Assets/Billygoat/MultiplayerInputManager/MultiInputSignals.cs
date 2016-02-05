using strange.extensions.signal.impl;

namespace Billygoat.MultiplayerInput
{
    public class MultiInputSignals
    {
        public Signal<PlayerDevice> PlayerJoined = new Signal<PlayerDevice>();
        public Signal<PlayerDevice> PlayerRemoved = new Signal<PlayerDevice>();
        public Signal<PlayerDevice> PlayerDeviceChanged = new Signal<PlayerDevice>();

        public Signal StartGame = new Signal();
        public Signal EndGame = new Signal();
        public Signal<PlayerDevice> PlayerReady = new Signal<PlayerDevice>();
    }
}