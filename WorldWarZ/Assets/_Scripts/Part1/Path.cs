/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public sealed class Path
{
    private static Path instance;
    private List<GameObject> waypoints = new List<GameObject>();

    public List<GameObject> Waypoints { get { return waypoints; } }
    public static Path Singleton
    {
        get
        {
            if(instance == null)
            {
                instance = new Path();
                instance.Waypoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
            }
            return instance;
        }
    }
}
