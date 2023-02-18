using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private float screenSize;
    [SerializeField] Camera mainCam;

    private void Awake()
    {
        screenSize = mainCam.orthographicSize;
    }

    private void Start()
    {
        print(screenSize);
    }
}
