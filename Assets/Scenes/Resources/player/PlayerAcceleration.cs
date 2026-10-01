using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAcceleration : MonoBehaviour
{
    public ParticleSystem accelerationEffect;
    public float normalForwardSpeed = 30f;
    public float acceleratedForwardSpeed = 50f;

    public bool isAccelerating = false;

    private PlayerController controller;

    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!controller.IsControlEnabled())
            return;

        if (Input.GetButtonDown("L3") || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isAccelerating = !isAccelerating;
        }

        if (isAccelerating)
        {
            accelerationEffect.Play();
            controller.forwardSpeed = acceleratedForwardSpeed;
        }
        else
        {
             accelerationEffect.Stop();
             controller.forwardSpeed = normalForwardSpeed;
        }
    }
       

    public bool IsAccelerating()
    {
        return isAccelerating;
    }

    
}

