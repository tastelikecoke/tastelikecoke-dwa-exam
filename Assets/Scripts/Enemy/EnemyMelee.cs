using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("Enemy Melee Settings")]
    public float meleeCooldown = 0.4f;
    public float meleeDistance = 2f;
    public float attackPermittedMaximumHeight = 0.6f;

    [Header("Required References")]
    public PlayerController targetPlayer = null;
    public Animator meleeAnimator = null;

    /* runtime variables */
    private float timeUntilMelee = 0.0f;
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
            
            if(playerDirection.magnitude <= meleeDistance && timeUntilMelee <= 0.0f)
            {
                meleeAnimator.SetTrigger("Activate");
                timeUntilMelee = meleeCooldown;

                Health playerHealth = targetPlayer.GetComponent<Health>();
                playerHealth?.Damage(1.0f);
            }
        }
        if(timeUntilMelee > 0.0f) timeUntilMelee -= Time.deltaTime;
    }
}
