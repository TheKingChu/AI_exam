using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public List<Waypoints> waypoints;

    public Waypoints lastWaypoint
    {
        get; set;
    }

    public float distance
    {
        get; set;
    }

    private void OnDrawGizmos()
    {
        if(waypoints == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;

        foreach(Waypoints waypoint in waypoints)
        {
            if(waypoints != null) 
            {
                Gizmos.DrawLine(transform.position, waypoint.transform.position);
            }
        }
    }
}
