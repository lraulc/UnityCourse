using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 15.0f;
    [SerializeField] private GameObject explosionAnim;
    [SerializeField] private float hoverSpeed = 1.0f;
    [SerializeField] private float amplitude = 1.0f;
    [SerializeField] private int health = 3;

    private Vector3 tempPosition;
    private Vector3 startPosition;

    SpawnManager _spawnManager;
    Collider2D asteroidCollider2D;

    Material asteroidMaterial;
    private int flickerSpeedID;

    private void Awake()
    {
        flickerSpeedID = Shader.PropertyToID("_Speed");
    }


    private void Start()
    {
        if (explosionAnim == null) { Debug.LogError("No Explosion Prefab Assigned"); }
        asteroidCollider2D = GetComponent<Collider2D>();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null) { Debug.LogError("No Spawn Manager found!"); }

        tempPosition = gameObject.transform.position;
        startPosition = transform.position;

        asteroidMaterial = gameObject.GetComponent<SpriteRenderer>().material;
    }

    private void FixedUpdate()
    {
        Rotator(rotationSpeed);
        hoverMovement();
    }

    private void Rotator(float rotSpeed)
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, (1 * (rotSpeed * Time.deltaTime))), Space.Self);
    }

    private void hoverMovement()
    {
        tempPosition.y = (Mathf.Sin(Time.time * hoverSpeed) * amplitude) + startPosition.y;
        transform.position = tempPosition;
    }

    IEnumerator flickerAnim()
    {
        asteroidMaterial.SetFloat(flickerSpeedID, 300);
        yield return new WaitForSeconds(0.1f);
        asteroidMaterial.SetFloat(flickerSpeedID, 0);
        yield return new WaitForSeconds(0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        health -= 1;
        if (other.tag == "Laser" && health == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            asteroidCollider2D.enabled = false;

            _spawnManager.StartSpawning();

            Instantiate(explosionAnim, gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject); // Destroy Laser
            Destroy(this.gameObject); // Destroy Asteroid
            // Destroy(explosionAnim, 2.0f); // Destroy Explosion
        }
        else
        {
            Destroy(other.gameObject); // Destroy Laser on Impact
            StartCoroutine(flickerAnim());
        }
    }
}
