using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private HPScript hpScript;
    public float goalLine = 3300f;

    private bool isControlEnabled = true;
    public float forwardSpeed = 30f;

    private PlayerMovement movement;
    public PlayerAcceleration playerAcceleration;
    public bool moveZ = true;
    void Start()
    {
      
        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();
        movement = GetComponent<PlayerMovement>();
        playerAcceleration = GetComponent<PlayerAcceleration>();
    }

    void Update()
    {
        
        // ëÄçÏêßå‰ON/OFFîªíË
        if (transform.position.z <= 50f || transform.position.z >= goalLine || hpScript.Gauge <= 0)
        {
            isControlEnabled = false;
        }
        else
        {
            isControlEnabled = true;
        }

       
        // ì¸óÕÇ‚à⁄ìÆÅEâÒì]ÇÕñ≥å¯âª
        if (isControlEnabled == false)
        {
            movement.StopMovement(); // velocity É[ÉçÇ…Ç∑ÇÈ
            return;
        }

        // ëÄçÏâ¬î\Ç»Ç∆Ç´ÇæÇØèàóù
        movement.HandleMoveAndRotation();

        if (transform.position.z >= goalLine || isControlEnabled == false)
        {

            playerAcceleration.isAccelerating = false;
          
        }

        if (transform.position.z >= goalLine) 
        {
            movement.RotationReset();
        }


    }
   

   
    internal bool IsControlEnabled()
    {
        return isControlEnabled;
    }
}

