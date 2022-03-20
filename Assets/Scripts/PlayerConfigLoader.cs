using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfigLoader : MonoBehaviour
{
    [Header("Required References")]
    public GameObject player = null;
    public Gameplay gameplay = null;

    /* runtime */
    private Health playerHealth;
    private PlayerController playerController;

    private void Awake()
    {
        if(player != null)
        {
            playerHealth = player.GetComponent<Health>();
            playerController = player.GetComponent<PlayerController>();
        }
    }
    private void Update()
    {
        PlayerConfig playerConfig = Resources.Load<PlayerConfig>(PlayerConfig.fileName);
        Debug.Log(playerConfig);
        if(playerConfig != null)
        {
            if(playerHealth != null)
            {
                playerHealth.maxHealth = playerConfig.maxHealth;
                playerHealth.damageCooldown = playerConfig.damageCooldown;
                playerHealth.attackFlag = playerConfig.attackFlag;
            }
            if(playerController != null)
            {
                playerController.moveSpeed = playerConfig.moveSpeed;
                playerController.fireCooldown = playerConfig.fireCooldown;
                playerController.bulletSpeed = playerConfig.bulletSpeed;
                playerController.controllerDeadZone = playerConfig.controllerDeadZone;
                playerController.attackPermittedMaximumHeight = playerConfig.attackPermittedMaximumHeight;
            }
            if(gameplay != null)
            {
                gameplay.playerRespawnTime = playerConfig.playerRespawnTime;
            }
        }
    }
}
