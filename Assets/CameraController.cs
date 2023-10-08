using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera mainCamera;
    void Start() {
        mainCamera = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Game.activeBoard == null)
            mainCamera.orthographicSize = 5;
        else {
            mainCamera.orthographicSize =  (5f / 4f) * (float)Game.activeBoard.Height();
        }
    }
}
