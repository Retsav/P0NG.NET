using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private LayerMask barsLayerMask;
    [SerializeField] private List<Vector3> spawnPoints;

    public override void OnNetworkSpawn()
    {
        ScoreBar.OnAnyBarHit += ScoreBar_OnAnyBarHit;
        ResetPlayersTransformClientRpc();
    }

    private void ScoreBar_OnAnyBarHit(object sender, EventArgs e)
    {
        ResetPlayersTransformClientRpc();
    }

    [ClientRpc]
    private void ResetPlayersTransformClientRpc()
    {
        transform.position = spawnPoints[(int)OwnerClientId];
    }
    
    private void Update()
    {
        if (!IsOwner) return;
        Vector3 moveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) moveDir.y = +1f;
        if (Input.GetKey(KeyCode.S)) moveDir.y = -1f;
        float moveSpeed = 3f;
        float moveDistance = .51f;
        bool canMove = !Physics2D.Raycast(transform.position, moveDir, moveDistance, barsLayerMask) &&
                       GameStatesSystem.GetCurrentState() == GameStatesSystem.gameplayState;
        if (canMove)
        {
            transform.position += moveDir * (moveSpeed * Time.deltaTime);   
        }
    }
}
