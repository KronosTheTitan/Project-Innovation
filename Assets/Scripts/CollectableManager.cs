using System;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    #region Singleton

    public static CollectableManager Instance { get; private set; }

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

    public void PickupTreasure()
    {
        treasureCollected++;
    }
}