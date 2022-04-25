using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WaypointEditor : MonoBehaviour
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        if((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.yellow * 0.5f;
        }

        Gizmos.DrawSphere(waypoint.transform.position, 0.1f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint.width / 4f),
            waypoint.transform.position - (waypoint.transform.right * waypoint.width / 4));

        if(waypoint._prevWP != null)
        {
            Gizmos.color = Color.red;
            Vector3 offset = waypoint.transform.right * waypoint.width / 4f;
            Vector3 offsetTo = waypoint._prevWP.transform.right * waypoint._prevWP.width / 4f;
            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint._prevWP.transform.position + offsetTo);
        }
        if(waypoint._nextWP != null)
        {
            Gizmos.color = Color.green;
            Vector3 offset = waypoint.transform.right * -waypoint.width / 4f;
            Vector3 offsetTo = waypoint._nextWP.transform.right * -waypoint._nextWP.width / 4f;
            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint._nextWP.transform.position + offsetTo);
        }
        if(waypoint.branches != null)
        {
            foreach(Waypoint branch in waypoint.branches)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);
            }
        }
    }
}
