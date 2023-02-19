using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    private Player player;
    private float vertLimit = -5.8f;
    private float horLimit = -8.5f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null) { Debug.LogError("No Player Found = NULL"); }
    }


    private void Update()
    {
        movement();
    }

    float RandomNum()
    {
        return Random.Range(horLimit, -horLimit);
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

            if (player != null)
            {
                player.addScore(10);
            }

            Destroy(other.gameObject);
        }
        if (other.tag == "Player")
        {
            // Damage the player
            // Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }
    }


}
