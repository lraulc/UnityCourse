using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    private float vertLimit = -5.8f;
    private float horLimit = -8.5f;


    private void Update()
    {
        movement();
    }

    float RandomNum()
    {
        return Random.Range(horLimit, -horLimit);
    }

    public GameObject Respawn(Transform parent)
    {
        Instantiate(this.gameObject, new Vector3(RandomNum(), -vertLimit, 0), Quaternion.identity, parent);
        return this.gameObject;
    }

    void movement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y <= vertLimit)
        {
            transform.position = new Vector3(RandomNum(), -vertLimit, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
        if (other.tag == "Player")
        {
            // Damage the player
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
    }
}
