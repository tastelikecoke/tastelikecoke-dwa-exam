using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerConfig", fileName = "Config/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public static string fileName = "Config/PlayerConfig"; 
    public float moveSpeed = 5f;
    public float fireCooldown = 0.1f;
    public float bulletSpeed = 10f;
    public float controllerDeadZone = 0.3f;
    public float attackPermittedMaximumHeight = 0.6f;
    public float maxHealth = 3f;
    public float damageCooldown = 1f;
    public string attackFlag = "Player";
    public float playerRespawnTime = 3f;
}
