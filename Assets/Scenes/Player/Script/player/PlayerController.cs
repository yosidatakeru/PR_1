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
        // 操作制御ON/OFF判定
        if (transform.position.z <= 50f || transform.position.z >= goalLine || hpScript.Gauge <= 0)
        {
            isControlEnabled = false;
        }
        else
        {
            isControlEnabled = true;
        }

        // 常に前進処理（止めない）
        transform.position += forwardSpeed * Vector3.forward * Time.deltaTime;

        // 入力や移動・回転は無効化
        if (isControlEnabled == false)
        {
            movement.StopMovement(); // velocity ゼロにする
            return;
        }

        // 操作可能なときだけ処理
        movement.HandleMoveAndRotation();

        if (transform.position.z >= goalLine || isControlEnabled == false)
        {

            playerAcceleration.isAccelerating = false;
            // ※回転リセットは PlayerMovement 側で行ってください（必要なら）
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

