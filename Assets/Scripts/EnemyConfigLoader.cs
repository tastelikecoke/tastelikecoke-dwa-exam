using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConfigLoader : MonoBehaviour
{
    public string configFileName = "";
    private Health enemyHealth;
    private EnemyController enemyController;
    private EnemyMelee enemyMelee;
    private EnemyAttack enemyAttack;
    private void Awake()
    {
        enemyHealth = this.GetComponent<Health>();
        enemyController = this.GetComponent<EnemyController>();
        enemyMelee = this.GetComponent<EnemyMelee>();
        enemyAttack = this.GetComponent<EnemyAttack>();
    }

    private void Update()
    {
        EnemyConfig enemyConfig = Resources.Load<EnemyConfig>(configFileName);
        if(enemyConfig != null)
        {
            if(enemyHealth != null)
            {
                enemyHealth.maxHealth = enemyConfig.maxHealth;
                enemyHealth.damageCooldown = enemyConfig.damageCooldown;
                enemyHealth.attackFlag = enemyConfig.attackFlag;
            }
            if(enemyController != null)
            {
                enemyController.moveSpeed = enemyConfig.moveSpeed;
                enemyController.approachRadius = enemyConfig.approachRadius;
            }
            if(enemyMelee != null)
            {
                enemyMelee.meleeCooldown = enemyConfig.meleeCooldown;
                enemyMelee.meleeDistance = enemyConfig.meleeDistance;
                enemyMelee.attackPermittedMaximumHeight = enemyConfig.attackPermittedMaximumHeight;
            }
            if(enemyAttack != null)
            {
                enemyAttack.fireCooldown = enemyConfig.fireCooldown;
                enemyAttack.bulletSpeed = enemyConfig.bulletSpeed;
                enemyAttack.attackPermittedMaximumHeight = enemyConfig.attackPermittedMaximumHeight;
            }
        }
    }
}
