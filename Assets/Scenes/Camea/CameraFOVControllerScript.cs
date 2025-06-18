using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOVControllerScript : MonoBehaviour
{
    public Camera camera;
    public PlayerAcceleration playerAcceleration;
    float normalFOV = 60f;
    float boostFOV = 80f;
    float lerpSpeed = 5f;

    void Update()
    {
        if (camera == null || playerAcceleration == null) return;

        float targetFOV = playerAcceleration.isAccelerating ? boostFOV : normalFOV;
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, Time.deltaTime * lerpSpeed);
    }
}

