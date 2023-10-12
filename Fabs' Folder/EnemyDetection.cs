using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

// Currenttly the speed is really bugged and will zoom to the player

public class EnemyDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public CircleCollider2D detectionRadius;
    public Rigidbody2D rb;
    private Seeker seeker;
    private AIDestinationSetter setter;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        setter = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsPlayerWithinRadius())
        {
            StartPathfinding(setter.target.position);
        }
    }


    private bool IsPlayerWithinRadius()
    {
        Vector3 playerPosition = setter.target.position;
        return detectionRadius.OverlapPoint(playerPosition);
    }

    private void OnPathComplete(Path path)
    {
        if (path.error)
        {
            // Handle pathfinding error
            return;
        }

        Vector3[] waypoints = path.vectorPath.ToArray();
        StartCoroutine(FollowPath(waypoints));
    }

    private IEnumerator FollowPath(Vector3[] waypoints)
    {
        int currentWaypointIndex = 0;

        while (currentWaypointIndex < waypoints.Length)
        {
            Vector3 currentWaypoint = waypoints[currentWaypointIndex];
            while (Vector3.Distance(transform.position, currentWaypoint) > 0.1f)
            {
                float step = (1) * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, Mathf.Min(step, 1));
                if (Vector3.Distance(setter.target.position, currentWaypoint) > detectionRadius.radius)
                {
                    StartPathfinding(setter.target.position);
                    yield break;
                }

                yield return null;
            }
            currentWaypointIndex++;
        }
    }

    public void StartPathfinding(Vector3 targetPosition)
    {
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
    }
}
