using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameStatesSystem : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI timerCounterText;



    
    public static EntryState entryState = new EntryState();
    public static GameplayState gameplayState = new GameplayState();
    public static IntermissionState intermissionState = new IntermissionState();


    private static IGameState currentState;
    
    private NetworkVariable<float> stateTime = new NetworkVariable<float>(ENTRY_STATE_TIMER_MAX);
    private const int ENTRY_STATE_TIMER_MAX = 3;
    private const int INTERMISSION_STATE_TIMER_MAX = 2;

    
    public override void OnNetworkSpawn()
    {
        currentState = entryState;
        stateTime.OnValueChanged += stateTime_OnValueChanged;
        entryState.OnStateChanged += States_OnStateChangedResetText;
        intermissionState.OnStateChanged += States_OnStateChangedResetText;
        ScoreBar.OnAnyBarHit += ScoreBar_OnAnyBarHit;
    }

    private void ScoreBar_OnAnyBarHit(object sender, EventArgs e)
    {
        if (!IsServer) return;
        UpdateStateMachineToIntermissionServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdateStateMachineToIntermissionServerRpc()
    {
        Debug.Log("SERVER: SEND INTERMISSION");
        UpdateStateMachineToIntermissionClientRpc();
    }
    

    [ClientRpc]
    private void UpdateStateMachineToIntermissionClientRpc()
    {
        Debug.Log("CLIENT: GET INTERMISSION!");
        currentState = intermissionState;
        if(currentState != intermissionState) Debug.LogError("Current state is not intermission state"); 
        UpdateStateTimerServerRpc(INTERMISSION_STATE_TIMER_MAX);
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdateStateTimerServerRpc(float value)
    {
        stateTime.Value = value;
    }
    
    private void States_OnStateChangedResetText(object sender, EventArgs e)
    {
        States_OnStateChangedResetTextClientRpc();
    }

    [ClientRpc]
    private void States_OnStateChangedResetTextClientRpc()
    {
        timerCounterText.text = "";
    }
    
    private void stateTime_OnValueChanged(float previousvalue, float newvalue)
    {
        timerCounterText.text = Math.Round(newvalue).ToString();
    }


    private void Update()
    {
        if (!IsServer) return; 
        currentState = currentState.DoState(this);
        UpdateStateMachineClientRpc(currentState.GetStateIndex());
    }

    [ClientRpc]
    private void UpdateStateMachineClientRpc(int stateIndex)
    {
        switch (stateIndex)
        {
            case 0:
                currentState = entryState;
                break;
            case 1:
                currentState = gameplayState;
                timerCounterText.text = "";
                break;
            case 2:
                currentState = intermissionState;
                break;
            default:
                Debug.LogError("Wrong stateIndex!");
                break;
        }
    }
    

    public static IGameState GetCurrentState()
    {
        return currentState;
    }
    
    public void DecreaseStateTimerByDeltaTime()
    {
        if (!IsServer) return;
        stateTime.Value -= Time.deltaTime;
    }
    
    public float GetStateTimer()
    {
        return stateTime.Value;
    }
    
    
}
