using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    private int waypointIndex = 0;
    private float dist;

    void Start()
    {
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        dist = Vector2.Distance(transform.position, waypoints[waypointIndex].position);

        if (dist < 0.1f)
        {
            IncreaseIndex();
        }

        PatrolBetweenWaypoints();
    }

    void PatrolBetweenWaypoints()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
