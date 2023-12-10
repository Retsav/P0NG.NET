using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    public event EventHandler OnStateChanged;
    IGameState DoState(GameStatesSystem gss);
}
