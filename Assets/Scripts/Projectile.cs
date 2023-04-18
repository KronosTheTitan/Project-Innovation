using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Start()
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("EnemyHit");
            other.gameObject.GetComponent<Shark>().TakeDamage();
        }

        if(other.gameObject.CompareTag("Player"))
        {
            return;
        }

       // Debug.Log("Destroy");
       // Destroy(gameObject);
    }

    
}
