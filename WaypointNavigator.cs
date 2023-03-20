using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField] private Waypoints waypoints; //Reference to which waypoint system to use

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float distanceTreshold = 0.1f;
    [SerializeField] private float waitDuration = 5f;
    private Animator _Anim;
    private bool isWaiting = false;

    private Transform currentWaypoint; //The waypoint the object is moving

    void Start()
    {
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;
        _Anim = gameObject.GetComponent<Animator>();

        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
        _Anim.SetBool("Walking", true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if ((Vector3.Distance(transform.position, currentWaypoint.position) < distanceTreshold) && !isWaiting)
        {
            _Anim.SetBool("Walking", false);
            isWaiting = true;
            StartCoroutine(WaitForSeconds());
        }
    }

    IEnumerator WaitForSeconds ()
    {
        yield return new WaitForSeconds(waitDuration);
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
        isWaiting = false;
        _Anim.SetBool("Walking", true);
    }
}
