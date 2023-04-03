using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Collectable : MonoBehaviour
{
    private const String PLAYER = "Player";

    protected abstract void OnContact(GameObject player);
    
    private void OnTriggerEnter(Collider other)
    {
        //check if the other gameObject is the player, if its not exit the method.
        if (other.gameObject.name != PLAYER) return;

        OnContact(other.gameObject);
        
        //destroy the gameObject, the trash spawner it belongs to will spawn a new one in due time
        Destroy(gameObject);
    }
}