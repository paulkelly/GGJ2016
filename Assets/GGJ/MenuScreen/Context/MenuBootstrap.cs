using strange.extensions.context.impl;

namespace GGJ2016
{
    public class MenuBootstrap : ContextView
    {
        private void Awake()
        {
            this.context = new MenuContext(this);
        }
    }
}