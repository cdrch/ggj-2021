using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    private Vector2 movementInput;

    private bool onTree;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        // Get movement input from axes
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");        
    }

    private void Move()
    {
        // Add normalized movement to position
        Vector2 movement = movementInput.normalized * speed;
        
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }
}
