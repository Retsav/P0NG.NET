using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ScoreNetworkManager : NetworkBehaviour
{
    [SerializeField] private NetworkVariable<int> leftPlayerScore;
    [SerializeField] private NetworkVariable<int>  rightPlayerScore;


    [SerializeField] private TextMeshProUGUI leftPlayerScoreText;
    [SerializeField] private TextMeshProUGUI rightPlayerScoreText;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }
}
