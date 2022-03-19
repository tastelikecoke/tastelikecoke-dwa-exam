using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float fireCooldown = 1f;
    public GameObject bulletPrefab = null;
    public Camera mainCamera = null;

    private CharacterController characterController = null;
    public GameObject playerMesh = null;
    private float timeUntilFire = 0.0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Vector3 inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController?.SimpleMove(inputMovement * moveSpeed);

        Ray inputRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(inputRay, out hit))
        {
            Vector3 inputDirection = hit.point - transform.position;
            inputDirection = new Vector3(inputDirection.x, 0, inputDirection.z);
            playerMesh.transform.rotation = Quaternion.LookRotation(inputDirection, Vector3.up);

            if(Input.GetMouseButtonDown(0) && bulletPrefab != null && timeUntilFire <= 0.0f)
            {
                GameObject bulletInstance = Instantiate(bulletPrefab, this.transform.position, playerMesh.transform.rotation);
                Projectile projectile = bulletInstance.GetComponent<Projectile>();
                projectile.direction = inputDirection.normalized * 0.05f;
                timeUntilFire = fireCooldown;
            }
        }
        if(timeUntilFire > 0.0f) timeUntilFire -= Time.deltaTime;
    }
}
