using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHEALTH : MonoBehaviour
{
    public int cont = 0;
    public int hitsToDie;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            cont++;
            if(cont >= hitsToDie)
            {

                Destroy(this.gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}
