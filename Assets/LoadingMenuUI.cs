using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

public class LoadingMenuUI : NetworkBehaviour
{
    private int playerCount;
    
    
    
    private async Task WaitForSecondPlayer()
    {
        Debug.Log("WAITING FOR SECOND PLAYER!");

    }
}
