using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ScoreBar : NetworkBehaviour
{
    private NetworkVariable<int> barScore = new NetworkVariable<int>();

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!IsServer) return;
        Debug.Log("PONG");
        barScore.Value++;
    }

    public NetworkVariable<int> GetBarScore()
    {
        return barScore;
    }
}
