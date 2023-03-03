using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotSettings : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] lasers;

    private void Start()
    {
        lasers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void ChangeLaserColor(Color laserColor)
    {
        foreach (var laser in lasers)
        {
            laser.color = laserColor;
        }
    }
}
