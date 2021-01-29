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
        col = GetComponentInChildren<Collider2D>();
    }
}
