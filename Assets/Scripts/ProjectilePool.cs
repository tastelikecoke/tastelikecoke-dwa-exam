using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public GameObject prefab = null;
    public float projectileLifetime = 5f;

    private List<Projectile> pool;

    public void Awake()
    {
        pool = new List<Projectile>();
    }

    public Projectile Spawn(Vector3 position, Quaternion rotation)
    {
        Projectile projectile = null;
        for(int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].gameObject.activeSelf)
            {
                projectile = pool[i];
                break;
            }
        }
        if(projectile == null)
        {
            GameObject newPrefab = Instantiate(prefab, position, rotation);
            newPrefab.transform.SetParent(this.transform);
            projectile = newPrefab.GetComponent<Projectile>();
            pool.Add(projectile);
        }
        projectile.gameObject.SetActive(true);
        projectile.Reset(position, rotation, Vector3.zero, projectileLifetime);
        return projectile;
    }

    public void ClearAll()
    {
        for(int i = 0; i < pool.Count; i++)
        {
            pool[i].gameObject.SetActive(false);
        }
    }
}
