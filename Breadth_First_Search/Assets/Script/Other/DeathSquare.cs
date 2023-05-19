using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSquare : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
