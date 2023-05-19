using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QPathFinder;

public class Block : MonoBehaviour
{
    public GamerManager gameManager;
    public GameObject pathBlockPrefab;
    [SerializeField] private bool IsPathChanger;
    public GameObject BreakSound;
    
    public GameObject breakParticlesPrefab;

    [SerializeField] private PathFinder _pathFinder;
    [SerializeField] private int _pathIndex;
    
    
    private void Awake()
    {
        _pathFinder = FindObjectOfType<PathFinder>();
        gameManager = FindObjectOfType<GamerManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (IsPathChanger)
            {
                ClearPath();
                GetComponentInChildren<Block>();
                
            }

            Instantiate(BreakSound);
            Destroy(gameObject);
            
            Instantiate(breakParticlesPrefab, transform.position, Quaternion.identity);

            Instantiate(pathBlockPrefab, transform.position, Quaternion.identity);
            gameManager.AddPoints();

        }
    }
    
    public void ClearPath()
    {
        _pathFinder.graphData.GetPath(_pathIndex).isOpen = true;
    }
    
    
}
