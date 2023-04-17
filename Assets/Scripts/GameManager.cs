using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion
    
    [SerializeField] public int treasureCollected;
    [SerializeField] private Submarine player;
    [SerializeField] public int treasureDeposited;
    [SerializeField] private int treasureNeededForVictory;
    
    [SerializeField] private GameObject victoryScreen;

    public void PickupTreasure()
    {
        treasureCollected++;
    }

    public void DespositTreasure()
    {
        treasureDeposited += treasureCollected;

        if (treasureDeposited >= treasureNeededForVictory)
        {
            victoryScreen.SetActive(true);
            player.LockPlayer();
            SceneManager.LoadScene("victoryScene");
        }
        player.AddRocket(treasureCollected);
        treasureCollected = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [SerializeField] private string mainMenuSceneName;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public Submarine GetPlayer()
    {
        return player;
    }
}