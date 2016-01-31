using strange.extensions.context.impl;
using Billygoat.MultiplayerInput;

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