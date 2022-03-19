using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    public ProjectilePool projectilePool = null;

    private float lifetime = 0f;

    public void Reset(Vector3 position, Quaternion rotation, Vector3 newDirection, float newLifetime)
    {
        transform.position = position;
        transform.rotation = rotation;
        lifetime = newLifetime;
        direction = newDirection;
    }
    private void Update()
    {
        this.transform.position += direction;
        if(lifetime < 0f)
        {
            this.gameObject.SetActive(false);
        }
        lifetime -= Time.deltaTime;
    }
}
