using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float fireCooldown = 1f;
    public GameObject bulletPrefab = null;
    public ProjectilePool projectilePool = null;
    public Camera mainCamera = null;

    private CharacterController characterController = null;
    public GameObject playerMesh = null;
    private float timeUntilFire = 0.0f;

    private bool isMouse = false;
    private Vector3 lastInputDirection = Vector3.forward;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Vector3 inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController?.SimpleMove(inputMovement * moveSpeed);

        Vector3 inputDirection = lastInputDirection;

        /* mouse */
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
            if(inputDirection == Vector3.zero || inputDirection.magnitude <= 0.3f)
                inputDirection = lastInputDirection;
        }
        playerMesh.transform.rotation = Quaternion.LookRotation(inputDirection, Vector3.up);

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
        if(timeUntilFire > 0.0f) timeUntilFire -= Time.deltaTime;

        lastInputDirection = inputDirection;
    }

    private void FireBullet(Vector3 inputDirection)
    {
        if(projectilePool != null && timeUntilFire <= 0.0f)
        {
            Projectile projectile = projectilePool.Spawn(this.transform.position, playerMesh.transform.rotation);
            projectile.direction = inputDirection.normalized * 0.05f;
            timeUntilFire = fireCooldown;
        }
    }
}
