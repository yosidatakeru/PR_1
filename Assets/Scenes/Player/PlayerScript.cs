using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public  class PlayerScript : MonoBehaviour
{
    public GameObject Bullet;
    //プレイヤーの移動スピード
    public float playerSpeed = 15;

    //Z方向に進むスピード
    public float playerZSpeed=20f;
  

    ////弾のインタバル制御
    int timeUntilNextShot = 0;

    int bulletNexst = 10;

    public float rotationSpeed = 5.0f; // 回転の慣性調整
    private Vector3 playerRotation;    // 現在の回転値
    private Vector3 targetRotation;    // 目標の回転値
    float triggerValue;
    float moveX;
    float moveY;
    // Start is called before the first frame update
    void Start()
    {
        playerRotation = Vector3.zero;
       
    }



    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Euler(playerRotation.x, playerRotation.y, playerRotation.z);

       

        // プレイヤーの移動処理
        MovePlayer();

        // 慣性をつけて回転をスムーズにする
        playerRotation = Vector3.Lerp(playerRotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(playerRotation);


        triggerValue = Input.GetAxis("RightTrigger");
        timeUntilNextShot--;
        if (Input.GetKey(KeyCode.Space) && timeUntilNextShot <= 0 || triggerValue > 0.1f&& timeUntilNextShot <= 0)
        {


            Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);


            timeUntilNextShot = bulletNexst;

        }
        //トリガーの取得
       
      

    }
    void MovePlayer()
    {


        moveX = Input.GetAxis("L_Stick_H"); // A（-1）D（+1）、Lスティック左右
        moveY = Input.GetAxis("L_Stick_V");   // W（+1）S（-1）、Lスティック上下

        // キーボード & コントローラー両対応の移動処理
        Vector3 move = new Vector3(moveX, moveY, 0) * playerSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + move;

        // 移動制限（範囲: X[-20,20], Y[-5,15]）
        newPosition.x = Mathf.Clamp(newPosition.x, -20.0f, 20.0f);
        newPosition.y = Mathf.Clamp(newPosition.y, -5.0f, 15.0f);
        transform.position = newPosition;

        // 機体の傾き調整（ターゲット回転）
        if (moveY > 0) targetRotation.x = Mathf.Max(targetRotation.x - 10, -35); // 前進
        else if (moveY < 0) targetRotation.x = Mathf.Min(targetRotation.x + 2, 20); // 後退
        else targetRotation.x = Mathf.Lerp(targetRotation.x, 0, Time.deltaTime * rotationSpeed);

        if (moveX > 0) targetRotation.z = Mathf.Max(targetRotation.z - 2, -35); // 右移動
        else if (moveX < 0) targetRotation.z = Mathf.Min(targetRotation.z + 2, 35); // 左移動
        else targetRotation.z = Mathf.Lerp(targetRotation.z, 0, Time.deltaTime * rotationSpeed);




        //前に移動
       // transform.position += playerZSpeed * Vector3.forward * Time.deltaTime;
        //デバックのために残しとく
        // Wキー（前方移動）
        if (Input.GetKey(KeyCode.W) && transform.position.y <= 15.0f)
        {
            transform.position += playerSpeed * Vector3.up * Time.deltaTime;
            targetRotation.x = Mathf.Max(targetRotation.x - 10, -35);
        }
        else
        {

            targetRotation.x = Mathf.Lerp(targetRotation.x, 0, Time.deltaTime * rotationSpeed);
        }

        // Sキー（後方移動）
        if (Input.GetKey(KeyCode.S) && transform.position.y >= -5.0f)
        {
            transform.position -= playerSpeed * Vector3.up * Time.deltaTime;
            targetRotation.x = Mathf.Min(targetRotation.x + 2, 20);
        }
        else
        {
            targetRotation.x = Mathf.Lerp(targetRotation.x, 0, Time.deltaTime * rotationSpeed);
        }

        // Dキー（右移動）
        if (Input.GetKey(KeyCode.D) && transform.position.x <= 20.0f)
        {
            transform.position += playerSpeed * Vector3.right * Time.deltaTime;
            targetRotation.z = Mathf.Max(targetRotation.z - 2, -35);
        }
        else
        {
            targetRotation.z = Mathf.Lerp(targetRotation.z, 0, Time.deltaTime * rotationSpeed);
        }

        // Aキー（左移動）
        if (Input.GetKey(KeyCode.A) && transform.position.x >= -20.0f)
        {
            transform.position -= playerSpeed * Vector3.right * Time.deltaTime;
            targetRotation.z = Mathf.Min(targetRotation.z + 2, 35);
        }
        else
        {
            targetRotation.z = Mathf.Lerp(targetRotation.z, 0, Time.deltaTime * rotationSpeed);
        }
    }
}

  


    



