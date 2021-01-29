using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkPart : MonoBehaviour
{
    public TreePart side = TreePart.None;
    public Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        GameObject child = transform.GetChild(0).gameObject;

        PolygonCollider2D polyCol = GetComponentInChildren<PolygonCollider2D>();

        EdgeCollider2D edgeCol = child.AddComponent<EdgeCollider2D>();

        edgeCol.points = polyCol.points;
        edgeCol.isTrigger = false;

        Destroy(polyCol);

        Rigidbody2D rb = child.AddComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
