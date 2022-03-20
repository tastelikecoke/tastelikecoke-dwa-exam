using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Health> enemyPrefabTypes;
    public float waveCooldown = 1f;
    public int minimumEnemyAmount = 3;
    public float mapEdge = 19f;
    public float mapCeiling = 10f;
    public PlayerController defaultPlayerController = null;
    public ProjectilePool defaultEnemyProjectilePool = null;

    private List<Health> pool;
    public float timeUntilWave = 0f;
    private int waveNumber = 0;

    public delegate void OnSpawn(Health newEnemy);
    public OnSpawn onSpawn;

    public Vector3 RandomlyGenerateSpawnPoint()
    {
        Vector3 result = Vector3.zero;
        result.y = mapCeiling;
        /* is horizontal? */
        if(Random.Range(0,2) == 1)
        {
            /* top or bottom */
            result.z = (-1f + (float)Random.Range(0,2) * 2f) * mapEdge;
            result.x = Random.Range(-mapEdge , mapEdge);
        }
        else
        {
            /* left or right */
            result.x = (-1f + (float)Random.Range(0,2) * 2f) * mapEdge;
            result.z = Random.Range(-mapEdge , mapEdge);
        }
        return result;
    }

    private void Awake()
    {
        pool = new List<Health>();
        timeUntilWave = waveCooldown;
    }

    public void Reset()
    {
        waveNumber = 0;
        timeUntilWave = waveCooldown;
    }

    private void LateUpdate()
    {
        if(timeUntilWave <= 0.0f)
        {
            SpawnWave();
            timeUntilWave = waveCooldown;
        }
        if(timeUntilWave > 0.0f) timeUntilWave -= Time.deltaTime;
    }

    public void SpawnWave()
    {
        for(int i = 0; i < minimumEnemyAmount + waveNumber; i++)
        {
            Vector3 spawnPosition = RandomlyGenerateSpawnPoint();
            Debug.Log(spawnPosition);

            Health newEnemyHealth = null;
            for(int poolIdx = 0; poolIdx < pool.Count; poolIdx++)
            {
                if(pool[poolIdx].IsDead() && pool[poolIdx].name.StartsWith(enemyPrefabTypes[0].name))
                {
                    newEnemyHealth = pool[poolIdx];
                    break;
                }
            }
            if(newEnemyHealth == null)
            {
                newEnemyHealth = Instantiate(enemyPrefabTypes[0]);
                newEnemyHealth.transform.SetParent(this.transform);
                pool.Add(newEnemyHealth);
            }
            newEnemyHealth.enabled = true;

            EnemyController enemyController = newEnemyHealth.GetComponent<EnemyController>();
            if(enemyController != null)
            {
                enemyController.targetPlayer = defaultPlayerController;
            }
            EnemyAttack enemyAttack = newEnemyHealth.GetComponent<EnemyAttack>();
            if(enemyAttack != null)
            {
                enemyAttack.projectilePool = defaultEnemyProjectilePool;
                enemyAttack.targetPlayer = defaultPlayerController;
            }
            EnemyMelee enemyMelee = newEnemyHealth.GetComponent<EnemyMelee>();
            if(enemyMelee != null)
            {
                enemyMelee.targetPlayer = defaultPlayerController;
            }
            if(onSpawn != null)
                onSpawn(newEnemyHealth);
            newEnemyHealth.Reset();
            CharacterController characterController = newEnemyHealth.GetComponent<CharacterController>();
            if(characterController != null)
            {
                characterController.enabled = false;
            }
            newEnemyHealth.transform.position = spawnPosition;
        }
        waveNumber += 1;
    }
}