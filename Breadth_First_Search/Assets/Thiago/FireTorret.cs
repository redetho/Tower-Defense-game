using SVS.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTorret : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float rotationSpeed = 5f;
    public Transform firePoint;
    private AIPlayerDetector AI;

    public float fireRate = 0.5f; // Intervalo de tempo entre os disparos
    private float fireTimer = 0f;

    private void Start()
    {
        AI = GetComponent<AIPlayerDetector>();
    }

    private void FixedUpdate()
    {
        TurnTurretTowardsEnemy();

        if (AI.PlayerDetected)
        {
            fireTimer += Time.deltaTime; // Incrementa o temporizador com o tempo passado desde o último frame

            if (fireTimer >= fireRate)
            {
                FireProjectile(); // Dispara o projétil
                fireTimer = 0f; // Reinicia o temporizador
            }
        }
    }
    private void TurnTurretTowardsEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            Vector3 targetDirection = nearestEnemy.transform.position - transform.position;
            targetDirection.y = 0f; // Mantém a torreta nivelada no eixo Y (vertical)
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        TowerBullet bullet = projectile.GetComponent<TowerBullet>();
        bullet.SetDirection(firePoint.forward);
    }
}