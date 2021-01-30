using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleSpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObjectPool pool;

    public float spawnVertical = 1f;
    public float spawnHorizontal = 0f;

    public float spawnDistance;
    public float despawnDistance;

    public int initialEnemiesToSpawn = 1;
    public int maxEnemiesToSpawn;
    public int spawnCount = 0;

    public List<Transform> spawnedEnemies; // TODO: make this private again after debugging

    private float offsetY = 0f;
    private float offsetX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnedEnemies = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
