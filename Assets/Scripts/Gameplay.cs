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
    public EnemySpawner spawner = null;
    public int currentKills = 0;

    private float timeUntilPlayerRespawn = 0f;
    private int highscoreKills = 0;

    private void Awake()
    {
        timeUntilPlayerRespawn = playerRespawnTime;
        
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
    }
}
