﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager instance;

    public int difficultyLevel = 1;

    public float elevatorSpeed = 1f;
    public float elevatorSpeedChangePerDifficultyLevel = 0.25f; 
    // each level, elevator speed increases by elevatorSpeed * elevatorSpeedChangePerDifficultyLevel

    private Camera cam;
    private GameObject tree;
    private TreeTrunk treeTrunk;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton pattern
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // caching expensive operations
        cam = Camera.main;
        tree = GameObject.Find("Tree");
        treeTrunk = tree.GetComponentInChildren<TreeTrunk>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
