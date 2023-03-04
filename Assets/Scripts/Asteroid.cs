using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 15.0f;
    [SerializeField] private GameObject explosionAnim;
    [SerializeField] private float hoverSpeed = 1.0f;
    [SerializeField] private float amplitude = 1.0f;

    private Vector3 tempPosition;
    private Vector3 startPosition;

    SpawnManager _spawnManager;

    Collider2D asteroidCollider2D;

    private void Start()
    {
        if (explosionAnim == null) { Debug.LogError("No Explosion Prefab Assigned"); }
        asteroidCollider2D = GetComponent<Collider2D>();

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null) { Debug.LogError("No Spawn Manager found!"); }

        tempPosition = gameObject.transform.position;
        startPosition = transform.position;

        print(tempPosition);
    }

    private void FixedUpdate()
    {
        Rotator();
        hoverMovement();
    }

    private void Rotator()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, (1 * (rotationSpeed * Time.deltaTime))), Space.Self);
    }

    private void hoverMovement()
    {
        tempPosition.y = (Mathf.Sin(Time.time * hoverSpeed) * amplitude) + startPosition.y;
        transform.position = tempPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            asteroidCollider2D.enabled = false;

            _spawnManager.StartSpawning();

            Instantiate(explosionAnim, gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject); // Destroy Laser
            Destroy(this.gameObject); // Destroy Asteroid
            // Destroy(explosionAnim, 2.0f); // Destroy Explosion
        }
    }
}
