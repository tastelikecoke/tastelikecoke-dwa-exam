using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    public float maxHealth = 1f;
    public float damageCooldown = 0.5f;
    public string attackFlag = "";

    [Header("Required References")]
    public Animator damageAnimator = null;
    public Animator deathAnimator = null;

    public delegate void OnDeath(Health self);
    public OnDeath onDeath;

    /* runtime variables */
    private float health;
    private float timeUntilDamage = 0.0f;

    public float HealthRatio
    {
        get { return health / maxHealth; }
    }

    public bool IsDead()
    {
        return health <= 0.0f;
    }

    public void Reset()
    {
        timeUntilDamage = damageCooldown;
        health = maxHealth;
        if(deathAnimator != null)
            deathAnimator.SetBool("Activated", false);

        Collider collider = this.GetComponent<Collider>();
        if(collider != null)
        {
            collider.enabled = true;
        }

    }

    private void Awake()
    {
        Reset();
    }

    private void Update()
    {
        
        if(timeUntilDamage > 0.0f) timeUntilDamage -= Time.deltaTime;
    }

    public void Kill()
    {
        health = 0f;
        if(deathAnimator != null)
        {
            deathAnimator.SetBool("Activated", true);
            this.enabled = false;
            Collider collider = this.GetComponent<Collider>();
            if(collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    public bool Damage(float damageValue)
    {
        if(timeUntilDamage <= 0.0f)
        {
            health -= damageValue;
            timeUntilDamage = damageCooldown;
            if(IsDead())
            {
                if(onDeath != null)
                    onDeath.Invoke(this);
                if(deathAnimator != null)
                    deathAnimator.SetBool("Activated", true);
                this.enabled = false;
                Collider collider = this.GetComponent<Collider>();
                if(collider != null)
                {
                    collider.enabled = false;
                }
            }
            else if(damageAnimator != null)
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
