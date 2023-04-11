using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickup : MonoBehaviour
{
  
    public Submarine submarine;
    public UIManager uiPoopi;



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Treasure")
        {
            GameManager.Instance.PickupTreasure();
            Destroy(other.gameObject);
            uiPoopi.updateStats();
        }

        if(other.tag == "Fuel")
        {
            submarine.AddFuel(25);
            Destroy(other.gameObject);
            uiPoopi.updateStats();

        }
        if(other.tag == "Deposit")
        {
            GameManager.Instance.DespositTreasure();
            uiPoopi.updateStats();
        }

    }


}
