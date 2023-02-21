using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 15.0f;
    [SerializeField] private GameObject explosionAnim;


    Collider2D asteroidCollider2D;

    private void Start()
    {
        if (explosionAnim == null) { Debug.LogError("No Explosion Prefab Assigned"); }
        asteroidCollider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Rotator();
    }

    private void Rotator()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, (1 * (rotationSpeed * Time.deltaTime))), Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            asteroidCollider2D.enabled = false;
            explosionAnim = Instantiate(explosionAnim, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject); // Destroy Asteroid
            Destroy(explosionAnim, 2.0f); // Destroy Explosion
        }
    }
}
