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
        stateTime.Value = INTERMISSION_STATE_TIMER_MAX;
        UpdateStateMachineToIntermissionServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdateStateMachineToIntermissionServerRpc()
    {
        UpdateStateMachineToIntermissionClientRpc();
    }
    

    [ClientRpc]
    private void UpdateStateMachineToIntermissionClientRpc()
    {
        Debug.Log("INTERMISSION!");
        currentState = intermissionState;
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
        Debug.Log("State " + currentState + " : " + stateTime);
        if(IsServer) UpdateStateMachineClientRpc();
    }

    [ClientRpc]
    private void UpdateStateMachineClientRpc()
    {
        currentState = currentState.DoState(this);
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
