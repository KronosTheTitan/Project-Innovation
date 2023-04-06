﻿using UnityEngine;

public class Treasure : Collectable
{
    protected override void OnContact(GameObject player)
    {
        GameManager.Instance.PickupTreasure();
    }
}