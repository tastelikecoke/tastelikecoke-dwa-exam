using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Health> enemyPrefabTypes;
    public float waveCooldown = 10f;
    public float mapEdge = 20f;
    public float mapCeiling = 10f;
    public PlayerController defaultPlayerController = null;
    public ProjectilePool defaultEnemyProjectilePool = null;

    private List<Health> pool;
    private float timeUntilWave = 0f;
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
    }

    private void Start()
    {
        
        SpawnWave();
    }

    public void SpawnWave()
    {
        for(int i = 0; i < 19; i++)
        {
            Vector3 spawnPosition = RandomlyGenerateSpawnPoint();
            Health newEnemyHealth = Instantiate(enemyPrefabTypes[0]);
            newEnemyHealth.transform.position = spawnPosition;
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
        }
    }
}
