using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PathAgent))]
public class Shark : MonoBehaviour
{
    [SerializeField] private PathAgent agent;
    [SerializeField] private float agroDistance;
    [SerializeField] private float attackSpeed;
    





    private void Update()
    {
        
        //sry for the long line
        bool playerInRange = Vector3.Distance(GameManager.Instance.GetPlayer().transform.position, transform.position) < agroDistance;
        if (playerInRange)
        {
            Debug.Log("player Detected");
            transform.LookAt(GameManager.Instance.GetPlayer().transform);
            transform.position = Vector3.MoveTowards(transform.position,
                GameManager.Instance.GetPlayer().transform.position, attackSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 newPosition = agent.MoveAlongPath();
            transform.LookAt(newPosition);
            transform.position = newPosition;
            
        }

        
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }

    [SerializeField] private float damage;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        Submarine submarine = collision.gameObject.GetComponent<Submarine>();
        submarine.AddFuel(-damage);
        Destroy(gameObject);

    }

   
   
}
