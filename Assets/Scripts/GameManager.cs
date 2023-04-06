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
    
    [SerializeField] private int treasureCollected;
    [SerializeField] private Submarine player;

    public void PickupTreasure()
    {
        treasureCollected++;
    }

    public Submarine GetPlayer()
    {
        return player;
    }
}