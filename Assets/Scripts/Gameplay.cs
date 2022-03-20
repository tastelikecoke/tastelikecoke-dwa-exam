using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public Health playerHealth;
    public List<Health> enemiesHealth;
    public MainDisplay mainDisplay;
    public float playerRespawnTime = 3f;
    public GameObject playerSpawnPoint = null;

    private float timeUntilPlayerRespawn = 0f;

    private void Awake()
    {
        timeUntilPlayerRespawn = playerRespawnTime;
    }

    private void Update()
    {
        if(playerHealth.IsDead())
        {
            if(timeUntilPlayerRespawn > 0f)
            {
                mainDisplay?.ShowRespawn(timeUntilPlayerRespawn);
                timeUntilPlayerRespawn -= Time.deltaTime;
            }
            else
            {
                mainDisplay?.HideRespawn();
                timeUntilPlayerRespawn = playerRespawnTime;
                playerHealth.Reset();
                if(playerSpawnPoint != null)
                {
                    playerHealth.gameObject.transform.position = playerSpawnPoint.transform.position;
                }
            }
        }
    }
}
