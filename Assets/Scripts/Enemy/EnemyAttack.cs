using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Enemy Ranged Settings")]
    public float fireCooldown = 0.1f;
    public float bulletSpeed = 10f;
    public float attackPermittedMaximumHeight = 0.6f;

    [Header("Required References")]
    public PlayerController targetPlayer = null;
    public ProjectilePool projectilePool = null;
    public GameObject mesh = null;

    /* runtime variables */
    private float timeUntilFire = 0.0f;
    private Health health = null;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if(health.IsDead())
        {
            return;
        }
        if(attackPermittedMaximumHeight < transform.position.y)
        {
            return;
        }
        
        if(targetPlayer != null)
        {
            Vector3 playerDirection = targetPlayer.transform.position -  transform.position;
            FireBullet(playerDirection);
        }
        if(timeUntilFire > 0.0f) timeUntilFire -= Time.deltaTime;
    }
    private void FireBullet(Vector3 inputDirection)
    {
        if(projectilePool != null && timeUntilFire <= 0.0f)
        {
            Projectile projectile = projectilePool.Spawn(this.transform.position, mesh.transform.rotation);
            projectile.direction = inputDirection.normalized * bulletSpeed;
            timeUntilFire = fireCooldown;
        }
    }
}
