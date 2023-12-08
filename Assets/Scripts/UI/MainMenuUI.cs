using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button createGameButton;
    [SerializeField] private Button joinGameButton;


    private void Awake()
    {
        createGameButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            NetworkManager.Singleton.SceneManager.LoadScene("LoadingScene", LoadSceneMode.Single);
        });
        joinGameButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
