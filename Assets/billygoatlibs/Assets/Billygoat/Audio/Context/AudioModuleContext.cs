using Billygoat.Audio;
using strange.extensions.context.impl;
using UnityEngine;

namespace Billygoat
{
    public class AudioModuleContext : SignalContext
    {
        public AudioModuleContext(MonoBehaviour contextView) : base(contextView)
        {
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            injectionBinder.Bind<AudioSignals>().ToSingleton().CrossContext();
            injectionBinder.Bind<IAudioSystem>().To<AudioSystem>().ToSingleton().CrossContext();
            injectionBinder.Bind<MusicFade>().ToValue(new MusicFade(1.5f)).ToName(AudioTypes.MusicFade).CrossContext();
            injectionBinder.Bind<GameObject>().ToValue(Find("SFXPool")).ToName("SFXPool").CrossContext();

            mediationBinder.BindView<AudioPlaylist>().ToMediator<AudioPlaylistMediator>();
        }

        private GameObject Find(string name)
        {
            GameObject result = ((GameObject)contextView).transform.FindChild(name).gameObject;
            return result;
        }
    }
}
