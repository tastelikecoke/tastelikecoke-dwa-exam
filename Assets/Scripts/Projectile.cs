using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    private void Update()
    {
        this.transform.position += direction;
    }
}
