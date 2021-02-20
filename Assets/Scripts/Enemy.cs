using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    [Header("Movement")]
    [Range(0, 1f)]
    private float moveSpeed;

    // Internal
    [SerializeField]
    private Transform[] waypoints;
    private int waypointIndex;

    private Vector3 targetPosition;


    private float defaultMoveSpeed = .5f;
    private float minPathPositionDistance = .25f;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = waypoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused)
            return;

        WalkToPath();
    }

    /**
     * Check if has reached the path necessary
     * @return bool
     */
    bool HasReachedPath()
    {
        return Vector3.Distance(transform.position, targetPosition) < minPathPositionDistance;
    }

    /**
     * Move to the path
     * @return void
     */
    private void WalkToPath()
    {
        // We move ++ x speed to our path
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, defaultMoveSpeed * moveSpeed);

        if (HasReachedPath())
        {
            // Set next waypoint index
            waypointIndex = waypointIndex >= (waypoints.Length - 1) ? 0 : (waypointIndex + 1);
            // Update our target position
            targetPosition = waypoints[waypointIndex].position;
        }
    }
}
