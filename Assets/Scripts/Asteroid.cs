using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 15.0f;

    private void Update()
    {
        Rotator();
    }

    private void Rotator()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, (1 * (rotationSpeed * Time.deltaTime))), Space.Self);
    }
}
