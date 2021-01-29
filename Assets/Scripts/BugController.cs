using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{

    public float speed = 1f; // Base speed multiplier (can be modified)

    public float tiles = 0f;

    public float difficultyfactor = 10f; // Base speed divisor (can be modified)

    public float pathdistance = 0f; // Limit distance from spawn point

    private Vector2 movement;

    // Awake is called before Start

    private void Awake()
    {
        // Get number of tiles spawned this run
        tiles = GameObject.Find("Tree").GetComponent<TreeTrunk>().spawnCount;
    }

    // Start is called before the first frame update
    void Start()
    {

            if (tiles < 50)
            {
                speed *= tiles / difficultyfactor;
                pathdistance = Random.Range(tiles/5, tiles/5 + 3);
            }
            else
            {
                speed *= 50 / difficultyfactor;
                pathdistance = Random.Range(3, 13);
            }
        movement.x = speed;
        movement.y = speed;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)movement.normalized;
    }
}
