using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryState : IGameState
{
    public event EventHandler OnStateChanged;

    public IGameState DoState(GameStatesSystem gss)
    {
        if (!(gss.GetStateTimer() <= 0f))
        {
            gss.DecreaseStateTimerByDeltaTime();
            return GameStatesSystem.entryState;
        }
        SendEventOnStateChangedClientRpc();
        return GameStatesSystem.gameplayState;
    }

    public int GetStateIndex()
    {
        return 0;
    }

    private void SendEventOnStateChangedClientRpc()
    {
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
}
