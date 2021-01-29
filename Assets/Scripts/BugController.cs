using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{

    public float speed = 1f; // Base speed multiplier (can be modified)

    public float tiles = 10f;

    public float difficultyFactor = 10f; // Base difficulty divisor (can be modified)
    public float difficultyLimit = 50f;
    public int[] direction = new int[] { -1, 1 };

    public int directionMultiplierx = 0;
    public int directionMultipliery = 0;

    public float angleRandomizer = 0f;

    private Vector2 movement;

    // Awake is called before Start

    private void Awake()
    {
        // Get number of tiles spawned this run
        // tiles = GameObject.Find("Trunk").GetComponent<TreeTrunk>().spawnCount;

        // Set difficulty based on tiles spawned, cap difficulty at limit
        if (tiles < difficultyLimit)
        {
            speed *= tiles / difficultyFactor;
        }
        else
        {
            speed *= difficultyLimit / difficultyFactor;
        }

        // Randomize direction using array of [-1, 1] and randomize angle from [0:90) degrees
        directionMultiplierx = direction[Random.Range(0, 2)];
        directionMultipliery = direction[Random.Range(0, 2)];
        angleRandomizer = Random.Range((float)0.0, (float)1.0);

        // Set final values for movement vectors and normalize
        movement.x = speed * directionMultiplierx * angleRandomizer;
        movement.y = speed * directionMultipliery * (1.0f - angleRandomizer);
        movement.Normalize();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)movement * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            // Re-randomize angle
            angleRandomizer = Random.Range((float)0.0, (float)1.0);
            movement.x = speed * directionMultiplierx * angleRandomizer;
            movement.y = speed * directionMultipliery * (1.0f - angleRandomizer);
            // Invert direction
            movement *= -1;
            movement.Normalize();
            return;
        }

        Health h = collision.GetComponent<Health>();

        if (h == null)
        {
            Debug.Log("There's a missing health component on " + collision.name + "!");
            return;
        }

        h.TakeDamage(1);

        Destroy(gameObject); // TODO: replace with pooling later

    }

}
