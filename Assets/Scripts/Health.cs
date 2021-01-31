using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 1;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    private void CheckForDeath()
    {
        if (health > 0)
            return;

        // handle death
        //Destroy(gameObject);
        GameManager.instance.stage = GameStage.None;
        //gameObject.SetActive(false);
        GameManager.instance.Failure();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        CheckForDeath();
    }
}
