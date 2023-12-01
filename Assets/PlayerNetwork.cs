using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [SerializeField] private LayerMask barsLayerMask;
    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (int previousValue, int newValue) =>
        {
            Debug.Log(OwnerClientId + "; " + randomNumber.Value);
        };
        base.OnNetworkSpawn();
    }

    private void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.T))
        {
            randomNumber.Value = Random.Range(0, 100);
        }
        Vector3 moveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) moveDir.y = +1f;
        if (Input.GetKey(KeyCode.S)) moveDir.y = -1f;
        float moveSpeed = 3f;
        float moveDistance = .51f;
        bool canMove = !Physics2D.Raycast(transform.position, moveDir, moveDistance, barsLayerMask);
        Debug.DrawLine(transform.position, moveDir * moveDistance, Color.red);
        if (canMove)
        {
            transform.position += moveDir * (moveSpeed * Time.deltaTime);   
        }
    }

    [ServerRpc]
    private void TestServerRpc()
    {
        
    }
}
