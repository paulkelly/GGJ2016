using strange.extensions.context.impl;

namespace GGJ2016
{
    public class MainBootstrap : ContextView
    {
        private void Awake()
        {
            this.context = new MainContext(this);
        }
    }
}