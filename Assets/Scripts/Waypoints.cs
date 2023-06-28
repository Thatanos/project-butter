using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, 1f); //Draw gizmo on child
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1). position);
        }

        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

    public Transform GetNextWaypoint (Transform currentWaypoint)
    {
        if (currentWaypoint == null)
        {
            return transform.GetChild(0);
        }

        if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1) //GetSiblingIndex() will check the same entry in index
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        else //if on last waypoint, return the first waypoint
        {
            return transform.GetChild(0);
        }
    }

    public int GetCurrentWaypoint (Transform currentWaypoint)
    {
        return (currentWaypoint.GetSiblingIndex() - 1);
    }
}
