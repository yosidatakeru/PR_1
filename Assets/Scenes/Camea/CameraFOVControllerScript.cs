using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOVControllerScript : MonoBehaviour
{
    public Camera camera;
    public PlayerScript playerScript;
    float normalFOV = 60f;
    float boostFOV = 80f;
    float lerpSpeed = 5f;

    void Update()
    {
        if (camera == null || playerScript == null) return;

        float targetFOV = playerScript.acceleration ? boostFOV : normalFOV;
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, Time.deltaTime * lerpSpeed);
    }
}

