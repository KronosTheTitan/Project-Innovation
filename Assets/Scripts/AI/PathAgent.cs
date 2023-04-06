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
        /*
         * calculate remaining distance to next point
         * take the vector pointing towards the next point and normalize it, then mutliply it by
         * the speed and the time since last frame.
         *
         * if the next point is reached with spare movement available the process will repeat with the remaining movement
         */

        int nextPointIndex = pathIndex + 1;

        if (nextPointIndex >= path.NumPoints())
            nextPointIndex = 0;
        
        Vector3 nextCheckpoint = path.GetPoint(nextPointIndex);
        Vector3 directionToNextCheckpoint = nextCheckpoint - currentPosition;

        float adjustedSpeed = speed * Time.deltaTime;
        Debug.Log(adjustedSpeed);
        
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
