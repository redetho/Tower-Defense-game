using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GamerManager gameManager;
    public GameObject pathBlockPrefab;
    
    public GameObject breakParticlesPrefab;

    private void Awake()
    {
        gameManager = FindObjectOfType<GamerManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            Destroy(gameObject);
            
            Instantiate(breakParticlesPrefab, transform.position, Quaternion.identity);

            Instantiate(pathBlockPrefab, transform.position, Quaternion.identity);
            gameManager.AddPoints();

        }
    }
}
