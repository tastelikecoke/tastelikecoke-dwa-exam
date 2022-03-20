using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float fireCooldown = 0.1f;
    public float bulletSpeed = 10f;
    public float controllerDeadZone = 0.3f;
    public float attackPermittedMaximumHeight = 0.6f;

    [Header("Required References")]
    public ProjectilePool projectilePool = null;
    public Camera mainCamera = null;
    public GameObject playerMesh = null;

    /* runtime variables */
    private CharacterController characterController = null;
    private Health health = null;
    private float timeUntilFire = 0.0f;
    private bool isMouse = false;
    private Vector3 lastInputDirection = Vector3.forward;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        health = GetComponent<Health>();
    }

    private void Update()
    {

        if(Input.GetButton("Exit"))
        {
            Application.Quit();
        }
        if(health.IsDead())
        {
            characterController.enabled = false;
            return;
        }
        else
        {
            characterController.enabled = true;
        }
        Vector3 inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController?.SimpleMove(inputMovement.normalized * moveSpeed);

        Vector3 inputDirection = lastInputDirection;

        if(isMouse)
        {
            Ray inputRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(inputRay, out hit))
            {
                inputDirection = hit.point - transform.position;
                inputDirection = new Vector3(inputDirection.x, 0, inputDirection.z);
            }
        }
        else
        {
            inputDirection = new Vector3(Input.GetAxis("CameraHorizontal"), 0, Input.GetAxis("CameraVertical"));
            if(inputDirection == Vector3.zero || inputDirection.magnitude <= controllerDeadZone)
                inputDirection = lastInputDirection;
        }
        playerMesh.transform.rotation = Quaternion.LookRotation(inputDirection, Vector3.up);

        if(attackPermittedMaximumHeight >= transform.position.y)
        {
            if(Input.GetButton("Fire1"))
            {
                isMouse = true;
                FireBullet(inputDirection);
            }
            if(Input.GetButton("Fire1 Controller"))
            {
                isMouse = false;
                FireBullet(inputDirection);
            }
        }
        if(timeUntilFire > 0.0f) timeUntilFire -= Time.deltaTime;

        lastInputDirection = inputDirection;
        
    }

    private void FireBullet(Vector3 inputDirection)
    {
        if(projectilePool != null && timeUntilFire <= 0.0f)
        {
            Projectile projectile = projectilePool.Spawn(this.transform.position, playerMesh.transform.rotation);
            projectile.direction = inputDirection.normalized * bulletSpeed;
            timeUntilFire = fireCooldown;
        }
    }
}
