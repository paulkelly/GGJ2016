using UnityEngine;
using Billygoat.MultiplayerInput;
using strange.extensions.mediation.impl;

public class MainSceneLoader : View
{
    public int ReadyNeeded = 1;

    [Inject]
    public MultiInputSignals InputSignals { get; set; }

    [Inject]
    public IMultiInputManager InputManager { get; set; }

    [PostConstruct]
    public void OnConstruct()
    {
        InputSignals.PlayerReady.AddListener(OnPlayerReady);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        InputSignals.PlayerReady.RemoveListener(OnPlayerReady);
    }

    private void OnPlayerReady(PlayerData player)
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
