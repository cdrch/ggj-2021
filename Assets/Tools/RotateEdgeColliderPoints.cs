using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode]
public class RotateEdgeColliderPoints : MonoBehaviour
{
    public int amount;

    public void Rotate()
    {
        EdgeCollider2D col = GetComponent<EdgeCollider2D>();

        // TODO: put some safeguards here later

        List<Vector2> list = new List<Vector2>(col.points);

        for (int i = 0; i < amount; i++)
        {
            Vector2 first = list[0];
            list.RemoveAt(0);
            list.Add(first);
        }

        for (int i = 0; i > amount; i--)
        {
            Vector2 last = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            list.Insert(0, last);
        }



        string temp = "";
        for (int i = 0; i < list.Count; i++)
        {
            temp += list[i].ToString() + ", ";
        }
        Debug.Log(temp);

        col.points = list.ToArray();
    }
}
