using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float fireCooldown = 0.1f;
    public float bulletSpeed = 10f;

    [Header("Required References")]
    public PlayerController targetPlayer = null;
    public ProjectilePool projectilePool = null;
    public GameObject mesh = null;

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
        
        Vector3 playerDirection = targetPlayer.transform.position -  transform.position;
        
        FireBullet(playerDirection);
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
