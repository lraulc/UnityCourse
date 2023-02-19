using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float moveSpeed = -3.0f;
    private float verticalLimit = 7.0f;



    [SerializeField] //ID for powerups -> 0 = tripleshot, 1 = speedpowerup, 2 = shield
    [Tooltip("ID for powerups\n0 = TripleShot\n1 = Speed Boost\n2 = Shield")]
    private int powerupID;


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
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        Destroy(this.gameObject);
                        break;
                    case 1:
                        print("Speed boost!");
                        Destroy(this.gameObject);
                        break;
                    case 2:
                        print("Shields Collected");
                        Destroy(this.gameObject);
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            else
            {
                print("No 'Player' script added to 'Powerup'");
            }
        }
    }
}
