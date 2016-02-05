using UnityEngine;
using Billygoat.MultiplayerInput;
using strange.extensions.mediation.impl;
using Billygoat.Audio;

public class MainSceneLoader : View
{
    public int ReadyNeeded = 1;

    [Inject]
    public MultiInputSignals InputSignals { get; set; }

    [Inject]
    public IMultiInputManager InputManager { get; set; }

    [Inject]
    public AudioSignals TheAudioSignals { get; set; }

    public AudioClip[] IntroAudio;

    [PostConstruct]
    public void OnConstruct()
    {
        InputManager.Reset();

        InputSignals.PlayerReady.AddListener(OnPlayerReady);
        AudioPlaylistType music = new AudioPlaylistType();
        music.Clips.AddRange(IntroAudio);
        TheAudioSignals.PlayMusicTrack.Dispatch(music);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        InputSignals.PlayerReady.RemoveListener(OnPlayerReady);
    }

    private void OnPlayerReady(PlayerDevice player)
    {
        //Debug.Log("Player " + player.id + " ready");
        if(AllPlayersReady())
        {
            InputSignals.StartGame.Dispatch();
        }
    }

    private bool AllPlayersReady()
    {
        int numberReady = 0;

        foreach (var nextPlayer in InputManager.GetPlayers())
        {
            if (nextPlayer.Ready)
            {
                numberReady++;
            }
            else
            {
                return false;
            }
        }

        return numberReady >= ReadyNeeded;
    }
}
