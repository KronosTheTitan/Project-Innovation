using UnityEngine;

public class FuelTank : Collectable
{
      [SerializeField] private float amountOfFuel;
      protected override void OnContact(GameObject player)
      {
            Submarine submarine = player.GetComponent<Submarine>();

            submarine.AddFuel(amountOfFuel);
      }
}