using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float meleeCooldown = 0.4f;
    public float meleeDistance = 2f;

    [Header("Required References")]
    public PlayerController targetPlayer = null;
    public Animator meleeAnimator = null;

    private float timeUntilMelee = 0.0f;

    private void Update()
    {
        Vector3 playerDirection = targetPlayer.transform.position -  transform.position;
        
        if(playerDirection.magnitude <= meleeDistance && timeUntilMelee <= 0.0f)
        {
            meleeAnimator.SetTrigger("Activate");
            timeUntilMelee = meleeCooldown;

            Health playerHealth = targetPlayer.GetComponent<Health>();
            playerHealth?.Damage(1.0f);
        }
        if(timeUntilMelee > 0.0f) timeUntilMelee -= Time.deltaTime;
    }
}
