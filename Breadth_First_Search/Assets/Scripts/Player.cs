using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SearchPath _searchPath;

    [SerializeField] private float _movementSpeed = 10;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _rotationSpeed = 10;
    private int _currentPathIndex;


    private void Start()
    {
        _searchPath = FindObjectOfType<SearchPath>();
        if (_searchPath.Path != null)    // If the player has a path to move on 
        {
            StartCoroutine(Movement(_searchPath.Path));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }

    IEnumerator Movement(List<Node> paths)
    {
        _currentPathIndex = 0;

        while (_currentPathIndex < paths.Count)
        {
            Node currentPath = paths[_currentPathIndex];
            Vector3 targetPosition = new Vector3(currentPath.transform.position.x, _yOffset, currentPath.transform.position.z);

            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.deltaTime);

                // Calculate the rotation angle towards the next target position
                Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

                // Rotate faster by multiplying the rotation angle with rotation speed
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

                yield return null;
            }

            _currentPathIndex++;
        }

        GameManager.Instance.LoadNextLevel();
        Destroy(gameObject);
    }
}