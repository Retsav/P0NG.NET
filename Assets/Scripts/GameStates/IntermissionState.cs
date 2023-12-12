using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class IntermissionState : IGameState
{
    public event EventHandler OnStateChanged;
    public IGameState DoState(GameStatesSystem gss)
    {
        if (!(gss.GetStateTimer() <= 0f))
        {
            gss.DecreaseStateTimerByDeltaTime();
            return GameStatesSystem.intermissionState;
        }

        SendEventOnStateChangedClientRpc();
        return GameStatesSystem.gameplayState;
    }

    public int GetStateIndex()
    {
        return 2;
    }

    [ClientRpc]
    private void SendEventOnStateChangedClientRpc()
    {
        Debug.Log("INTERMISSION END EVENT SEND");
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
}
