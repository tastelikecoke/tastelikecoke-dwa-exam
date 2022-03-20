using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyConfig", fileName = "Config/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public static string fileName = "Config/EnemyConfig"; 

    [Header("General")]
    public float moveSpeed = 2.5f;
    public float approachRadius = 0f;
    public float attackPermittedMaximumHeight = 0.6f;

    [Header("Melee Settings (if Melee)")]
    public float meleeCooldown = 0.8f;
    public float meleeDistance = 1.5f;

    [Header("Ranged Settings (if Ranged)")]
    public float fireCooldown = 0.8f;
    public float bulletSpeed = 8f;

    [Header("Health")]
    public float maxHealth = 3f;
    public float damageCooldown = 1f;
    public string attackFlag = "Enemy";
}
