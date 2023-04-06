using System;
using UnityEngine;

[RequireComponent(typeof(PathAgent))]
public class Shark : MonoBehaviour
{
    [SerializeField] private PathAgent agent;

    private void Update()
    {
        transform.position = agent.MoveAlongPath();
    }
}
