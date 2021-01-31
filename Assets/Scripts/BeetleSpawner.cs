﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleSpawner : MonoBehaviour
{
    
    public GameObject beetle;
    public static GameObjectPool pool;
    public List<GameObject> spawnedEnemies; // TODO: make this private again after debugging
    public List<Transform> spawnedEnemiesXY;

    public int initialEnemiesToSpawn = 3;
    public int maxEnemiesToSpawn = 5;
    public int spawnCount = 0;
    public int difficulty = 1;
    private bool Initialized = false;

    public GameObject player;

    public float spawnDistance = 20f;
    public float despawnDistance = 15f;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        spawnedEnemies = new List<GameObject>();
        GameObject enemyBeetle;
        for(int i=0; i < initialEnemiesToSpawn; i++)
        {
            enemyBeetle = Instantiate(beetle);
            enemyBeetle.SetActive(false);
            spawnedEnemies.Add(enemyBeetle);
            spawnedEnemiesXY.Add(enemyBeetle.transform);
        }
        Initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Initialized)
        {
            CheckForNewBeetle();
        }
    }

    private void CheckForNewBeetle()
    {
        for(int j=0; j < spawnedEnemiesXY.Count; j++) {
            //Debug.Log(cam.transform.position.y + " - " + despawnDistance + " > " + spawnedEnemiesXY[j].position.y);
            if (cam.transform.position.y - spawnedEnemiesXY[j].position.y > spawnDistance && !spawnedEnemies[j].activeInHierarchy)
            {
                spawnCount += 1;
                spawnedEnemiesXY[j].position = cam.transform.position + new Vector3(0f,15f,10f);
                spawnedEnemies[j].SetActive(true);
            }
            if (cam.transform.position.y - despawnDistance > spawnedEnemiesXY[j].position.y)
            {
                spawnedEnemies[j].SetActive(false);
            }
        }
    }

}
