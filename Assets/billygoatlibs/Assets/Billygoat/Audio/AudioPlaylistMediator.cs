using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace Billygoat.Audio
{
	public class AudioPlaylistMediator : Mediator
	{
		[Inject]
		public AudioPlaylist view { get; set; }

		[Inject]
		public AudioSignals TheAuidoSignals { get; set; }


		public override void OnRegister()
		{
            TheAuidoSignals.PlayMusicTrack.AddListener (PlayTrack);
            TheAuidoSignals.LockPlaylist.AddListener (Lock);
            TheAuidoSignals.UnlockPlaylist.AddListener (Unlock);

            TheAuidoSignals.FadeInMusic.AddListener (FadeIn);
            TheAuidoSignals.FadeOutMusic.AddListener (FadeOut);
            TheAuidoSignals.ExitScene.AddListener (ExitScene);

            TheAuidoSignals.PauseMusic.AddListener(PauseMusic);
            TheAuidoSignals.UnpauseMusic.AddListener(UnpauseMusic);
		}

	    public override void OnRemove()
	    {
	        base.OnRemove();

            TheAuidoSignals.PlayMusicTrack.RemoveListener(PlayTrack);
            TheAuidoSignals.LockPlaylist.RemoveListener(Lock);
            TheAuidoSignals.UnlockPlaylist.RemoveListener(Unlock);

            TheAuidoSignals.FadeInMusic.RemoveListener(FadeIn);
            TheAuidoSignals.FadeOutMusic.RemoveListener(FadeOut);

            TheAuidoSignals.PauseMusic.RemoveListener(PauseMusic);
            TheAuidoSignals.UnpauseMusic.RemoveListener(UnpauseMusic);
	    }

	    void OnDestroy()
	    {
	        OnRemove();
	    }

	    private void PauseMusic()
	    {
	        view.Pause();
	    }

        private void UnpauseMusic()
        {
            view.Unpause();
        }

		private void PlayTrack(AudioPlaylistType playlist)
		{
            view.fadeTime = playlist.CrossfadeTime;
		    if (playlist.Clips.Count == 1)
		    {
		        view.Play (playlist.Clips[0], playlist.Loop);
		    }
            else if (playlist.Loop)
            {
                view.PlaySequence(playlist.Clips.ToArray(), playlist.Loop);
            }
			
		}

		private void Lock()
		{
			view.Locked = true;
		}

		private void Unlock()
		{
			view.Locked = false;
		}

		private void FadeIn(float t)
		{
			view.FadeIn (t);
		}

	    private void ExitScene(float fadeTime)
	    {
	        view.ExitScene(fadeTime);
	    }

        private void FadeOut(float t)
		{
			view.FadeOut (t);
		}
	}
}