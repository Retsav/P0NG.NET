using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetworkCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI waitingText;
    
    private int playerCounter;
    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
    }

    private async void OnClientConnectedCallback(ulong obj)
    {
        playerCounter++;
        await CheckPlayerNumber();
    }


    private async Task CheckPlayerNumber()
    {
        if (playerCounter >= 1)
        {
            Debug.Log("Second player connected...");
            Task loadTask = FakeLoadTimeAsync();
            waitingText.text = "PLAYER FOUND! STARTING...";
            await loadTask;
            LoadGameScene();
        }
    }

    private async Task FakeLoadTimeAsync()
    {
        await Task.Delay(5000);
        Debug.Log("STARTED!");
    }

    private void LoadGameScene()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
