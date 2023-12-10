using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallServer : NetworkBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private static NetworkVariable<float> ballTransformX = new NetworkVariable<float>(0f);
    private static NetworkVariable<float> ballTransformY = new NetworkVariable<float>(0f);

    private float ballSpeed = 200f;

    
    public override void OnNetworkSpawn()
    {
        GameStatesSystem.entryState.OnStateChanged += GameStatesSystem_EntryOrIntermissionState_OnStateChanged;
        GameStatesSystem.intermissionState.OnStateChanged += GameStatesSystem_EntryOrIntermissionState_OnStateChanged;
        ScoreBar.OnAnyBarHit += ScoreBar_OnAnyBarHit;
    }

    private void ScoreBar_OnAnyBarHit(object sender, EventArgs e)
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;
    }

    private void GameStatesSystem_EntryOrIntermissionState_OnStateChanged(object sender, EventArgs e)
    {
        if (IsServer)
        {
            AddStartingForce();   
        }
    }

    private void AddStartingForce()
    {
        float x = Random.value < .5f ? -1f : 1f;
        float y = Random.value < .5f ? Random.Range(-1f, -.5f) : Random.Range(.5f, 1f);
        Vector2 direction = new Vector2(x, y);
        rb.AddForce(direction * ballSpeed);
    }

    private void FixedUpdate()
    {
        if (!IsServer) return;
        ballTransformX.Value = transform.position.x;
        ballTransformY.Value = transform.position.y;
    }

    public static Vector2 GetServerBallPos()
    {
        return new Vector2(ballTransformX.Value, ballTransformY.Value);
    }
}
