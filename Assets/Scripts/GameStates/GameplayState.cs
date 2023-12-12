using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : IGameState
{
    public event EventHandler OnStateChanged;
    
    public IGameState DoState(GameStatesSystem gss)
    {
        return GameStatesSystem.gameplayState;
    }

    public int GetStateIndex()
    {
        return 1;
    }
}
