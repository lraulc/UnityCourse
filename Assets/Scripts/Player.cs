using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3.5f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleShot;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private int lives = 3;

    private float canFire = -1f;
    [SerializeField] public bool tripleShotActive = false;


    private SpawnManager spawnManager;

    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager == null) Debug.LogError("Spawn Manager not found = NULL");
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
        if (tripleShotActive == true)
        {
            Instantiate(tripleShot, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y + transform.localScale.y * 2.1f, 0), Quaternion.identity);
        }
    }

    public void calculateMovement()
    {
        float horizontalLimit = 9.4f;
        float verticalLimit = -4.0f;


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * movementSpeed * Time.deltaTime);

        // Vertical Bounds

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, verticalLimit, -verticalLimit), 0);

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

    public void Damage()
    {
        lives -= 1;
        if (lives <= 0)
        {
            spawnManager.OnPlayerDeath();
            print("You lose");
            Destroy(gameObject);
        }
    }
}
