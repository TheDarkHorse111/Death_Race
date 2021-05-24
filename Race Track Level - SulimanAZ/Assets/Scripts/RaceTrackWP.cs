using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrackWP : MonoBehaviour
{
    public GameObject[] Waypoints;

    private void OnDrawGizmos()
    {
        DrawGizmos(false);
    }
    private void OnDrawGizmosSelected()
    {
        DrawGizmos(true);
    }

    void DrawGizmos(bool Selected) 
    {
        if (Selected == false) return;
        if (Waypoints.Length > 1) 
        {
            Vector3 prev = Waypoints[0].transform.position;
            for (int i = 1; i < Waypoints.Length; i++) 
            {
                Vector3 next = Waypoints[i].transform.position;
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
            Gizmos.DrawLine(prev, Waypoints[0].transform.position);
        }
    }
}
