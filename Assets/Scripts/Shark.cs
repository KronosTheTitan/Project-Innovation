using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PathAgent))]
public class Shark : MonoBehaviour
{
    [SerializeField] private PathAgent agent;
    [SerializeField] private float agroDistance;
    [SerializeField] private float attackSpeed;

    public float coolDownTimer;
    private float currentTimer;
    private bool coolDown;





    private void Update()
    {

        if (currentTimer <= 0)
        {
            currentTimer = coolDownTimer;
            coolDown = false;
            
        }
        currentTimer -= Time.deltaTime;

        //sry for the long line
        bool playerInRange = Vector3.Distance(GameManager.Instance.GetPlayer().transform.position, transform.position) < agroDistance;
        if (playerInRange && coolDown == false)
        {
            Debug.Log("player Detected");
            transform.LookAt(GameManager.Instance.GetPlayer().transform);
            transform.position = Vector3.MoveTowards(transform.position,
                GameManager.Instance.GetPlayer().transform.position, attackSpeed * Time.deltaTime);
            agent.updateCurrentPosition(this.transform.position);
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
        if (collision.gameObject.name != "Player" )
            return;

        if (coolDown == true)
            return;
       
        Submarine submarine = collision.gameObject.GetComponent<Submarine>();
        submarine.AddFuel(-damage);
        //Destroy(gameObject);
        coolDown = true;
       

    }

   
   
}
