using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float fireCooldown = 1f;
    public GameObject bulletPrefab = null;

    private CharacterController characterController = null;
    private float timeUntilFire = 0.0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Vector3 inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController?.SimpleMove(inputMovement * moveSpeed);

        if(Input.GetKey(KeyCode.Space) && bulletPrefab != null && timeUntilFire <= 0.0f)
        {
            Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
            timeUntilFire = fireCooldown;
        }
        if(timeUntilFire > 0.0f) timeUntilFire -= Time.deltaTime;
    }
}
