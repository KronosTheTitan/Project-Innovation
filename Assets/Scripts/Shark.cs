using System;
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
}
