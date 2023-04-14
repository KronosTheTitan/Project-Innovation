using System;
using UnityEngine;

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

    public void PickupTreasure()
    {
        treasureCollected++;
    }

    public void DespositTreasure()
    {
        treasureDeposited += treasureCollected;
        player.AddRocket(treasureCollected);
        treasureCollected = 0;
    }

   

    public Submarine GetPlayer()
    {
        return player;
    }
}