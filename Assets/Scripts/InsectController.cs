using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectController : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D wallcheck;

    public float difficulty = 1f; //slider for testing difficulty scaling
    private float speed = 2f;
    private float orientationX = 0;
    private float orientationY = 0;
    private float skewX = 1f;
    private float startAngle = 0f;
    private float targetAngle = 0f;
    private float xshift;
    private int rotationSpeed = 50;
    private int directionOfRotation = 1;
    private readonly float toDegrees = 180 / Mathf.PI;

    private bool Initialized = false;
    private Camera cam;
    private Vector2 offset = new Vector2(0f,10f);

    private Vector2 movement;

    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        wallcheck = GetComponent<BoxCollider2D>();

        orientationX = Random.Range(0, 2) * 2 - 1;
        orientationY = Random.Range(0, 2) * 2 - 1;
        skewX = Random.Range((float)1, (float)1.3);

        movement.x = speed * difficulty * orientationX;
        movement.y = speed * difficulty * orientationY;

        Redirect();

        rb.SetRotation(targetAngle);

        startAngle = targetAngle;
    }

    void Start()
    {
        cam = Camera.main;
        Initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if(Initialized){
            difficulty += 0.1f;
            rotationSpeed += 1;
            xshift = Random.Range(-3f, 3f);
            offset = new Vector2(xshift, cam.transform.position.y + 10f);
            rb.MovePosition(offset);
        }
    }


    private void FixedUpdate()
    {
        if (Mathf.Floor(startAngle) != Mathf.Floor(targetAngle))
        {
            startAngle += directionOfRotation * rotationSpeed * Time.fixedDeltaTime;
            rb.SetRotation(startAngle);
        }
        else
        {
            rb.MovePosition(rb.position + movement * difficulty * Time.fixedDeltaTime);
            if (Mathf.Floor(startAngle) == Mathf.Floor(targetAngle))
            {
                wallcheck.enabled = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.otherCollider.CompareTag("Player"))
        {
            startAngle = (Mathf.Atan2(movement.y, movement.x) * toDegrees) - 90;
            Redirect();
            wallcheck.enabled = false;
            return;
        }
    }    

    private void Redirect()
    {
        directionOfRotation = 1;
        movement.x *= skewX;
        movement *= -1;
        movement.Normalize();
        skewX = Random.Range((float)0.8, (float)1.3);
        targetAngle = (Mathf.Atan2(movement.y, movement.x) * toDegrees) - 90;
        if(targetAngle < startAngle)
        {
            directionOfRotation = -1;
        }
    }

}
