using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{

    public float speed = 1f;

    public float tiles = 0f;

    public float difficulty = 0f;

    public float difficultyfactor = 10f;

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

            difficulty = tiles / difficultyfactor;
            speed *= difficulty;
        }
        else
        {
            speed = speed * 50 / difficultyfactor;
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
