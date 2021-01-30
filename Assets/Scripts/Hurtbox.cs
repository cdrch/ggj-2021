using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{

    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Health h = collision.GetComponent<Health>();

        if (h == null)
        {
            Debug.Log("There's a missing health component on " + collision.name + "!");
            return;
        }

        h.TakeDamage(damage);

    }
}
