using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    // [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 0;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleShot;
    [SerializeField] private GameObject playerShield;
    [SerializeField] private GameObject playerThruster;
    [SerializeField] private Color speedBoostColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    [SerializeField] private Color shieldPowerupColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    [SerializeField] private Color maxPowerColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    [SerializeField] private GameObject[] Engines;

    [SerializeField] private float movementMultiplier = 2.0f;
    private float movementSpeed = 10.0f;
    private float fireRate = 0.15f;

#pragma warning disable
    [SerializeField] private bool isTripleShotActive = false;
    [SerializeField] private bool isBoostSpeedActive = false;
    [SerializeField] private bool isShieldActive = false;
#pragma warning restore

    [SerializeField] private int score = 0;

    private float canFire = -1f;
    private float randomEngine;

    /*
    * Shader Updates
    */

    private Material playerMaterial;
    private int flickerSpeedID;

    /*
    * Aditional Classes - Script Communications
    */
    private SpawnManager spawnManager;
    private UIManager uimanager;


    void Start()
    {
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (uimanager == null) Debug.LogError("UI Manager = NULL");
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager == null) Debug.LogError("Spawn Manager not found = NULL");
        isBoostSpeedActive = false;

        currentHealth = lives;
        uimanager.SetMaxHealth(lives);

        randomEngine = Random.Range(0, 2);

        // Turn off Engines (Sprites) on start
        for (int i = 0; i < Engines.Length; i++)
        {
            if (Engines[i].activeSelf == true)
            {
                Engines[i].SetActive(false);
            }
        }

        /*
        * Setup Shader ID
        */

        playerMaterial = gameObject.GetComponent<SpriteRenderer>().material;
        if (playerMaterial == null) { Debug.LogError("No Sprite Renderer found on Player"); }
        flickerSpeedID = Shader.PropertyToID("_Speed");
        playerMaterial.SetFloat(flickerSpeedID, 0);
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
        if (isTripleShotActive == true)
        {
            Instantiate(tripleShot, transform.position, Quaternion.identity);
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

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, verticalLimit, 0.0f), 0);

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

    public void SpeedBoostActive()
    {
        isBoostSpeedActive = true;
        playerThruster.GetComponent<SpriteRenderer>().color = speedBoostColor;
        movementSpeed *= movementMultiplier;
        StartCoroutine(SpeedBoostRoutine());
    }

    IEnumerator SpeedBoostRoutine()
    {
        // SpeedBoost Duration
        yield return new WaitForSeconds(3.0f);
        playerThruster.GetComponent<SpriteRenderer>().color = Color.white;
        isBoostSpeedActive = false;
        movementSpeed /= movementMultiplier;
    }


    private void EngineDamage()
    {
        if (randomEngine == 0)
        {
            Engines[0].SetActive(true);
            randomEngine = 1;
        }
        else
        {
            Engines[1].SetActive(true);
            randomEngine = 0;
        }
    }

    public void Damage()
    {
        if (isShieldActive == true)
        {
            isShieldActive = false;
            uimanager.UpdateLineColor(isShieldActive);
            playerShield.SetActive(false);
            return;
        }

        currentHealth -= 1;
        uimanager.SetHealth(currentHealth);
        uimanager.UpdateLives(currentHealth);
        EngineDamage();

        if (currentHealth == 1)
        {
            playerMaterial.SetFloat(flickerSpeedID, 300);
        }

        if (currentHealth <= 0)
        {
            spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void ShieldActive()
    {
        isShieldActive = true;
        playerShield.SetActive(true);
        uimanager.UpdateLineColor(isShieldActive);

    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
        tripleShot.GetComponent<TripleShotSettings>().ChangeLaserColor(Color.white);
    }

    public void addScore(int points)
    {
        score += points;
        uimanager.updateScoreText(score);
    }
}
