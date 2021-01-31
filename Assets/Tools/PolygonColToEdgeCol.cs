using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[ExecuteInEditMode]
public class PolygonColToEdgeCol : MonoBehaviour
{
    public bool convertToTriggers;

    private void Awake()
    {
        ConvertPolyToEdge(GetComponent<PolygonCollider2D>());
        DestroyImmediate(GetComponent<PolygonColToEdgeCol>());
    }

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
}