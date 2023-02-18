using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] public float speed = 10.0f;

    void Update()
    {
        moveProjectile();
    }

    public void moveProjectile()
    {
        float vertLimit = 5.5f;

        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (transform.position.y >= vertLimit)
        {
            Destroy(this.gameObject);
        }
    }

}
