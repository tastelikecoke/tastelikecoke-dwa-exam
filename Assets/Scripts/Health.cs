using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Properties")]
    public float maxHealth = 1f;
    public float damageCooldown = 0.5f;
    public string attackFlag = "";
    public Animator damageAnimator = null;

    [Header("Live Properties")]
    public float health;
    private float timeUntilDamage = 0.0f;

    private void Reset()
    {
        health = maxHealth;
    }

    private void Update()
    {
        
        if(timeUntilDamage > 0.0f) timeUntilDamage -= Time.deltaTime;
    }

    private bool Damage(float damageValue)
    {
        if(timeUntilDamage <= 0.0f)
        {
            health -= damageValue;
            timeUntilDamage = damageCooldown;
            if(damageAnimator != null)
            {
                damageAnimator.SetTrigger("Activate");
            }
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider enter)
    {
        Projectile projectile = enter.gameObject.GetComponent<Projectile>();
        if(projectile != null)
        {
            if(attackFlag != projectile.attackFlag)
            {
                if(Damage(1.0f))
                    projectile.gameObject.SetActive(false);
            }
        }
    }
}
