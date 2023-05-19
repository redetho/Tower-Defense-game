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
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
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
        gameManager.GameOver();
        Destroy(gameObject);
    }
}
