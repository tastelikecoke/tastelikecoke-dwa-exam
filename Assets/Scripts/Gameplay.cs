using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    [Header("Gameplay Settings")]
    public float playerRespawnTime = 3f;

    [Header("Required References")]
    public Health playerHealth;
    public MainDisplay mainDisplay;
    public GameObject playerSpawnPoint = null;
    public EnemySpawner spawner = null;
    public ProjectilePool defaultEnemyProjectilePool = null;

    /* runtime variables */
    private int currentKills = 0;
    private List<Health> enemiesHealth;
    private float timeUntilPlayerRespawn = 0f;
    private int highscoreKills = 0;

    private void Awake()
    {
        timeUntilPlayerRespawn = playerRespawnTime;
        enemiesHealth = new List<Health>();
        
        for(int i = 0; i < enemiesHealth.Count; i++)
        {
            enemiesHealth[i].onDeath += EnemyDied;
        }
        highscoreKills = PlayerPrefs.GetInt("highscoreKills", 0);
        mainDisplay?.UpdateKills(currentKills, highscoreKills);

        spawner.onSpawn += OnSpawn;
    }

    private void OnSpawn(Health newEnemy)
    {
        enemiesHealth.Add(newEnemy);
        newEnemy.onDeath += EnemyDied;
    }

    private void AddKills()
    {
        currentKills += 1;
        if(highscoreKills < currentKills)
        {
            PlayerPrefs.SetInt("highscoreKills", currentKills);
            highscoreKills = currentKills;
        }
        mainDisplay?.UpdateKills(currentKills, highscoreKills);
    }

    private void EnemyDied(Health enemyHealth)
    {
        AddKills();
        /* enemyHealth.gameObject.SetActive(false); */

        enemyHealth.onDeath -= EnemyDied;
        enemiesHealth.Remove(enemyHealth);
    }

    private void Update()
    {
        mainDisplay?.UpdateHealth(playerHealth.HealthRatio);
        if(playerHealth.IsDead())
        {
            if(timeUntilPlayerRespawn > 0f)
            {
                mainDisplay?.ShowRespawn(timeUntilPlayerRespawn);
                timeUntilPlayerRespawn -= Time.deltaTime;
            }
            else
            {
                RespawnPlayer();
            }
        }
    }

    private void RespawnPlayer()
    {
        mainDisplay?.HideRespawn();
        timeUntilPlayerRespawn = playerRespawnTime;
        playerHealth.Reset();
        currentKills = 0;
        mainDisplay?.UpdateKills(currentKills, highscoreKills);
        if(playerSpawnPoint != null)
        {
            playerHealth.gameObject.transform.position = playerSpawnPoint.transform.position;
        }
        playerHealth.enabled = true;

        spawner?.Reset();
        for(int i = enemiesHealth.Count - 1; i >= 0; i--)
        {
            enemiesHealth[i].Kill();
            enemiesHealth[i].onDeath -= EnemyDied;
            enemiesHealth.Remove(enemiesHealth[i]);
        }
        defaultEnemyProjectilePool.ClearAll();
    }
}
