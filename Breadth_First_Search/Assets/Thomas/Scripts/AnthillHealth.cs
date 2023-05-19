using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnthillHealth : MonoBehaviour
{
    public int maxHealth = 10; // Maximum health of the tower
    public int currentHealth; // Current health of the tower
    public GamerManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GamerManager>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            Die();
            gameManager.GameOver();
        }

        gameManager.UpdateEnemyHealth(currentHealth);
    }

    private void Die()
    {
        // Logic for enemy death, such as playing death animation, adding score, etc.
        Destroy(gameObject);
    }
}
