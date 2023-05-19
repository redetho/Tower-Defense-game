using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    public float projectileSpeed = 10f;
    private Rigidbody rb;

    private void Start()
    {
        Destroy(this.gameObject, 6);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = transform.forward * projectileSpeed;
        
    }
    public void SetDirection(Vector3 direction)
    {
        transform.forward = direction;
    }
}
    

