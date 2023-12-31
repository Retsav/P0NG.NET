using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ScoreBar : NetworkBehaviour
{
    public static event EventHandler OnAnyBarHit;
    private NetworkVariable<int> barScore = new NetworkVariable<int>();

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!IsServer) return; ;
        OnAnyBarHitEventClientRpc();
        barScore.Value++;
    }

    [ClientRpc]
    private void OnAnyBarHitEventClientRpc()
    {
        OnAnyBarHit?.Invoke(this, EventArgs.Empty);
    }
    
    public NetworkVariable<int> GetBarScore()
    {
        return barScore;
    }
}
