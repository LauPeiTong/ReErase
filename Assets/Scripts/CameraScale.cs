using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    public float screenHeight = 1920f;
    public float screenWidth = 1080f;
    public float targetAspect = 9f / 16f;
    public float orthographicSize;
    private Camera mainCamera;

    // Use this for initialization
    void Start () {

        // Initialize variables
        mainCamera = Camera.main;
        orthographicSize = mainCamera.orthographicSize;

        // Calculating ortographic width
        float orthoWidth = orthographicSize / screenHeight * screenWidth;
        // Setting aspect ration
        orthoWidth = orthoWidth / (targetAspect / mainCamera.aspect);
        // Setting Size
        Camera.main.orthographicSize = (orthoWidth / Screen.width * Screen.height);
    }
}
