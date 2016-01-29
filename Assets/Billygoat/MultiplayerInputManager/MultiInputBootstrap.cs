using strange.extensions.context.impl;

namespace Billygoat.MultiplayerInput
{
    public class MultiInputBootstrap : ContextView
    {
        private static MultiInputBootstrap instance = null;

        public static MultiInputBootstrap Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            this.context = new MultiInputContext(this);
            DontDestroyOnLoad(this.gameObject);
        }
    }
}