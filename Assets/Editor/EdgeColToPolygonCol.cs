using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(EdgeCollider2D))]
public class EdgeColToPolygonCol : Editor
{
    public bool convertToTriggers;

    private void ConvertEdgeToPoly(EdgeCollider2D col)
    {
        List<Vector2> pointsList = new List<Vector2>();
        pointsList.AddRange(col.points);

        PolygonCollider2D poly = col.gameObject.AddComponent<PolygonCollider2D>();

        poly.pathCount = 1;
        poly.SetPath(0, pointsList.ToArray());

        if (convertToTriggers)
            poly.isTrigger = true;

        DestroyImmediate(col);
    }

    public override void OnInspectorGUI()
    {
        EdgeCollider2D col = (EdgeCollider2D)target;

        DrawDefaultInspector();

        //myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        //EditorGUILayout.LabelField("Level", myTarget.Level.ToString());

        if (GUILayout.Button("Convert To Poly Collider (Destructive)"))
        {
            ConvertEdgeToPoly(col);
        }
    }
}