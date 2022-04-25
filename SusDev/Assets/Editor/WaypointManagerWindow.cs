using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaypointManagerWindow : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<WaypointManagerWindow>();
    }

    public Transform waypointRoot;
    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));
        if(waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }
        obj.ApplyModifiedProperties();

    }

    void DrawButtons()
    {
        if(GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }
        if(Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
        {
            if (GUILayout.Button("Add Branch Waypoint"))
            {
                CreateBranch();
            }
            if (GUILayout.Button("Create Waypoint before"))
            {
                CreateWaypointBefore();
            }
            if(GUILayout.Button("Create Waypoint after"))
            {
                CreateWaypointAfter();
            }
            if(GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint();
            }
        }
    }

    void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint" + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        if(waypointRoot.childCount > 1)
        {
            waypoint._prevWP = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint._prevWP._nextWP = waypoint;
            //place wp at the last position
            waypoint.transform.position = waypoint._prevWP.transform.position;
            waypoint.transform.forward = waypoint._prevWP.transform.forward;
        }
        Selection.activeObject = waypoint.gameObject;

    }
    void CreateWaypointBefore()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();

        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;
        
        if(selectedWaypoint._prevWP != null)
        {
            newWaypoint._prevWP = selectedWaypoint._prevWP;
            selectedWaypoint._prevWP._nextWP = newWaypoint;
        }

        newWaypoint._nextWP = selectedWaypoint;

        selectedWaypoint._prevWP = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }
    void CreateWaypointAfter()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();

        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        newWaypoint._prevWP = selectedWaypoint;

        if(selectedWaypoint._nextWP != null)
        {
            selectedWaypoint._nextWP._prevWP = newWaypoint;
            newWaypoint._nextWP = selectedWaypoint._nextWP;
        }

        selectedWaypoint._nextWP = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

        Selection.activeGameObject = newWaypoint.gameObject;
    }
    void RemoveWaypoint()
    {
        Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

        if(selectedWaypoint._nextWP != null)
        {
            selectedWaypoint._nextWP._prevWP = selectedWaypoint._prevWP;
        }
        if(selectedWaypoint._prevWP != null)
        {
            selectedWaypoint._prevWP._nextWP = selectedWaypoint._nextWP;
            Selection.activeGameObject = selectedWaypoint._prevWP.gameObject;
        }
        DestroyImmediate(selectedWaypoint.gameObject);
    }
    void CreateBranch()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

        Waypoint branchedFrom = Selection.activeGameObject.GetComponent<Waypoint>();
        branchedFrom.branches.Add(waypoint);

        waypoint.transform.position = branchedFrom.transform.position;
        waypoint.transform.forward = branchedFrom.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;
    }
}
