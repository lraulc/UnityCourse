using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float moveSpeed = -3.0f;
    private float verticalLimit = 7.0f;

    // Update is called once per frame
    void Update()
    {
        move();
        outOfBounds();
    }

    void move()
    {
        transform.position += new Vector3(0.0f, moveSpeed * Time.deltaTime, 0.0f);
    }

    void outOfBounds()
    {
        if (transform.position.y <= -verticalLimit)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("Powerup Collected!");
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TripleShotActive();
                Destroy(this.gameObject);
            }
            else
            {
                print("No 'Player' script added to 'Powerup'");
            }
        }
    }
}
