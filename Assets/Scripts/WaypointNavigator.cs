using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField] private Waypoints waypoints; //Reference to which waypoint system to use

    //[SerializeField] private float moveSpeed = 5f;
    //[SerializeField] private float distanceTreshold = 0.1f;
    [SerializeField] private float waitDuration = 1f;
    private bool isWaiting = false;

    private Transform currentWaypoint; //The waypoint the object is moving

    void Start()
    {
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
    }

    public IEnumerator MovePlayer (int gridMove)
    {
        while (gridMove != 0)
        {
            transform.position = currentWaypoint.position;
            yield return new WaitForSeconds(waitDuration);
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            transform.LookAt(currentWaypoint);
            gridMove--;
        }

    }
}
