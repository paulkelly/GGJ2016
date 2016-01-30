using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace Billygoat
{
    public class AudioModuleBootstrap : ContextView
    {
        private static AudioModuleBootstrap instance = null;

        public static AudioModuleBootstrap Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            Init();
        }

        private void Init()
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
            this.context = new AudioModuleContext(this);
            DontDestroyOnLoad(this.gameObject);
        }
    }
}