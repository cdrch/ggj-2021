using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[ExecuteInEditMode]
public class EdgeColToPolygonCol : MonoBehaviour
{
    public bool convertToTriggers;

    private void Awake()
    {
        ConvertEdgeToPoly(GetComponent<EdgeCollider2D>());
        DestroyImmediate(GetComponent<EdgeColToPolygonCol>());
    }

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
}