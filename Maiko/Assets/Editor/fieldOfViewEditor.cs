using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (fieldOfView))]
public class fieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        fieldOfView fov = (fieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRad);
        
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRad);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRad);

        Handles.color = Color.red;
        foreach(Transform visibleTargets in fov.visibleTargets)
        {
            Handles.DrawLine(fov.transform.position, visibleTargets.position);
        }
    }
}
