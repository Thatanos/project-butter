using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField][Tooltip("Assign to waypoint.")] 
    private Waypoints waypoints; //Reference to which waypoint system to use

    //[SerializeField] private float moveSpeed = 5f; //deprecated
    //[SerializeField] private float distanceTreshold = 0.1f; //deprecated
    [SerializeField][Tooltip("Sets the interval between hops.")] 
    private float waitDuration = 1f;
    //private bool isWaiting = false; //deprecated
    [SerializeField][Tooltip("Exposed parameter for debugging, shows current waypoint index")]
    public int currentWaypointIndex;

    private Transform currentWaypoint; //The waypoint the object is moving

    void Start()
    {
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint); //calls function to get current waypoint
        transform.position = currentWaypoint.position; //transform to the current waypoint

        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint); //get the next waypoint
        transform.LookAt(currentWaypoint); //turns the player to the next waypoint
    }

    private void Update()
    {
        currentWaypointIndex = waypoints.GetCurrentWaypoint(currentWaypoint); //get the index of current waypoint
    }

    public IEnumerator MovePlayer (int gridMove) //moves the player
    {
        while (gridMove != 0) //if move not equal to zero, move the player
        {
            transform.position = currentWaypoint.position; //move the player to the next waypoint
            yield return new WaitForSeconds(waitDuration); //wait function
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint); //get the next waypoint
            transform.LookAt(currentWaypoint); //turns the player to the next waypoint
            gridMove--; //subtracts the total of gridmove
        }

    }
}
