using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<Health> enemyPrefabTypes;
    public float waveCooldown = 1f;
    public int minimumEnemyAmount = 3;
    public float mapEdge = 19f;
    public float mapCeiling = 10f;

    [Header("Required References")]
    public PlayerController defaultPlayerController = null;
    public ProjectilePool defaultEnemyProjectilePool = null;

    public delegate void OnSpawn(Health newEnemy);
    public OnSpawn onSpawn;

    /* runtime variables */
    private List<Health> pool;
    private float timeUntilWave = 0f;
    private int waveNumber = 0;


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
        timeUntilWave = 0f;
    }

    public void Reset()
    {
        waveNumber = 0;
        timeUntilWave = 0f;
    }

    private void Update()
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
        StartCoroutine(SpawnWaveCR());
    }
    public IEnumerator SpawnWaveCR()
    {
        for(int i = 0; i < minimumEnemyAmount + waveNumber; i++)
        {
            int enemyIdx = Random.Range(0, enemyPrefabTypes.Count);
            Vector3 spawnPosition = RandomlyGenerateSpawnPoint();
            Debug.Log(spawnPosition);

            Health newEnemyHealth = null;
            for(int poolIdx = 0; poolIdx < pool.Count; poolIdx++)
            {
                if(pool[poolIdx].IsDead() && pool[poolIdx].name.StartsWith(enemyPrefabTypes[enemyIdx].name))
                {
                    newEnemyHealth = pool[poolIdx];
                    break;
                }
            }
            if(newEnemyHealth == null)
            {
                newEnemyHealth = Instantiate(enemyPrefabTypes[enemyIdx]);
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
            /* Character Controller disrupts transform position changes, so it is disabled at start.
             * it is reenabled by EnemyController.
             */
            CharacterController characterController = newEnemyHealth.GetComponent<CharacterController>();
            if(characterController != null)
            {
                characterController.enabled = false;
            }
            newEnemyHealth.transform.position = spawnPosition;
            yield return null;
        }
        waveNumber += 1;
    }
}
