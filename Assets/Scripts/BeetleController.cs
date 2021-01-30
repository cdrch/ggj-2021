using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleController : MonoBehaviour
{

    private bool onTree;

    private Rigidbody2D rb;

    public float speed = 1f; // Base speed multiplier (can be modified)

    public float tiles = 10f; // Test value for tiles

    public float difficultyFactor = 10f; // Base difficulty divisor (can be modified)
    public float difficultyLimit = 50f;
    public int[] direction = new int[] { -1, 1 };

    public int directionMultiplierX = 1;
    public int directionMultiplierY = 1;

    public float angleRandomizer = 1f; // No randomness

    private Vector2 movement;

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
        directionMultiplierX = direction[Random.Range(0, 2)];
        directionMultiplierY = direction[Random.Range(0, 2)];
        //angleRandomizer = Random.Range((float)0.0, (float)1.0); // Temporarily removed randomization

        // Set final values for movement vectors and normalize
        movement.x = speed * directionMultiplierX * angleRandomizer;
        Debug.Log(movement);
        movement.y = speed * directionMultiplierY * (2.0f - angleRandomizer);
        movement.Normalize();
        Debug.Log(movement);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.otherCollider.CompareTag("Player"))
        {
            // Re-randomize angle and invert direction
            // angleRandomizer = Random.Range((float)0.9, (float)1.1); // Temporarily removed randomization
            movement.x *= angleRandomizer * -1;
            movement.y *= (2.0f - angleRandomizer) * -1;
            movement.Normalize();
            Debug.Log(movement);
            return;
        }

        /*Health h = collision.GetComponent<Health>();

        if (h == null)
        {
            Debug.Log("There's a missing health component on " + collision.name + "!");
            return;
        }

        h.TakeDamage(1);

        Destroy(gameObject); // TODO: replace with pooling later*/

    }

}
