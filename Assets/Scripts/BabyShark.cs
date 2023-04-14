using UnityEngine;

public class BabyShark : MonoBehaviour
{
    [SerializeField] private PathAgent agent;
    private void Update()
    {
        Vector3 newPosition = agent.MoveAlongPath();
            transform.LookAt(newPosition);
            transform.position = newPosition;
    }
}