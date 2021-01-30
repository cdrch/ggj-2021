using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(PolygonCollider2D))]
public class PolygonColToEdgeCol : Editor
{
    public bool convertToTriggers;
    
    private void ConvertPolyToEdge(PolygonCollider2D col)
    {
        List<Vector2> pointsList = new List<Vector2>();
        pointsList.AddRange(col.points);
        pointsList.Add(col.points[0]); // connect the start and end
        
        EdgeCollider2D edge = col.gameObject.AddComponent<EdgeCollider2D>();       

        edge.points = pointsList.ToArray();

        if (convertToTriggers)
            edge.isTrigger = true;

        DestroyImmediate(col);
    }

    public override void OnInspectorGUI()
    {
        PolygonCollider2D col = (PolygonCollider2D)target;

        DrawDefaultInspector();

        //myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        //EditorGUILayout.LabelField("Level", myTarget.Level.ToString());

        if (GUILayout.Button("Convert To Edge Collider (Destructive)"))
        {
            ConvertPolyToEdge(col);
        }
    }
}