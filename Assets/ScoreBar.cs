using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ScoreBar : NetworkBehaviour
{
    public static event EventHandler OnBarHit;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!IsServer) return;
        Debug.Log("PONG");
        OnBarHit?.Invoke(this, EventArgs.Empty);
    }
}
