﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transform cam;
    private Vector3 lastCameraPosition;
    public Vector2 startingPoint; // does nothing currently
    public Vector2 parallaxMultipler;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameManager.instance.cam.transform;
        lastCameraPosition = cam.position;
    }
    
    void LateUpdate()
    {
        Vector3 deltaMovement = cam.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxMultipler.x, deltaMovement.y * parallaxMultipler.y, deltaMovement.z);
        lastCameraPosition = cam.position;
    }
}
