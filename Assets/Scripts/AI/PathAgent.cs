using AI;
using UnityEngine;

public class PathAgent : MonoBehaviour
{
    [SerializeField] private Path path;

    [SerializeField] private float speed;
    [SerializeField] private int pathIndex;
    [SerializeField] private Vector3 currentPosition;

    public Vector3 MoveAlongPath()
    {
        int nextPointIndex = pathIndex + 1;

        if (nextPointIndex >= path.NumPoints())
            nextPointIndex = 0;
        
        Vector3 nextCheckpoint = path.GetPoint(nextPointIndex);
        Vector3 directionToNextCheckpoint = nextCheckpoint - currentPosition;

        float adjustedSpeed = speed * Time.deltaTime;
        
        if (directionToNextCheckpoint.magnitude <= adjustedSpeed)
        {
            adjustedSpeed -= directionToNextCheckpoint.magnitude;
            pathIndex = nextPointIndex;
            
            currentPosition = nextCheckpoint;
            nextCheckpoint = path.GetPoint(pathIndex);
            directionToNextCheckpoint = nextCheckpoint - currentPosition;
        }

        Vector3 outputVector = currentPosition;

        outputVector += directionToNextCheckpoint.normalized * adjustedSpeed;

        currentPosition = outputVector;
        return outputVector;
    }
}
