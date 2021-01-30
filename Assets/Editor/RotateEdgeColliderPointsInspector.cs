using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RotateEdgeColliderPoints))]
public class RotateEdgeColliderPointsInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Rotate"))
        {
            ((RotateEdgeColliderPoints)target).Rotate();
        }

        if (GUILayout.Button("Done"))
        {
            DestroyImmediate(target);
        }
    }
}
