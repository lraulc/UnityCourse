using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3.5f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float fireRate = 0.5f;
    private float canFire = -1f;

    void Start()
    {
        // Take the current position = new position(0,0,0)
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);

    }

    void Update()
    {
        calculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            Fire();
        }
    }

    private void Fire()
    {
        canFire = Time.time + fireRate;
        Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2, 0), Quaternion.identity);
    }

    public void calculateMovement()
    {
        float horizontalLimit = 9.4f;
        float verticalLimit = -4.4f;


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * movementSpeed * Time.deltaTime);

        // Vertical Bounds

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, verticalLimit, 0), 0);

        // Horizontal clip

        if (transform.position.x >= horizontalLimit)
        {
            transform.position = new Vector3(-horizontalLimit, transform.position.y, 0);
        }
        else if (transform.position.x <= -horizontalLimit)
        {
            transform.position = new Vector3(horizontalLimit, transform.position.y, 0);
        }

    }
}
