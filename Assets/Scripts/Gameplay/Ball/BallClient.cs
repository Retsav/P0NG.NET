using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BallClient : NetworkBehaviour
{
    private void FixedUpdate()
    {
        transform.position = BallServer.GetServerBallPos();
    }
}
