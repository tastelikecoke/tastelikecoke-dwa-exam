using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float moveSpeed = 1f;
    public float approachRadius = 7f;

    [Header("Required References")]
    public PlayerController targetPlayer = null;

    /* runtime variables */
    private CharacterController characterController = null;
    private Health health = null;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if(health.IsDead())
        {
            characterController.enabled = false;
            return;
        }
        else
        {
            characterController.enabled = true;
        }
        Vector3 playerDisplacement = transform.position - targetPlayer.transform.position;
        if(playerDisplacement.magnitude < approachRadius)
        {
            characterController?.SimpleMove(playerDisplacement.normalized * moveSpeed);
        }
        else if(playerDisplacement.magnitude > approachRadius)
        {
            characterController?.SimpleMove(-playerDisplacement.normalized * moveSpeed);
        }
    }
}
