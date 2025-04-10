using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public  class PlayerScript : MonoBehaviour
{
    public GameObject Bullet;
    public ParticleSystem sparkR;
    public ParticleSystem sparkL;

    //プレイヤーの移動スピード
    float playerSpeed = 30f;

    //Z方向に進むスピード
     float playerZSpeed = 30f;

    ////弾のインタバル制御
    int timeUntilNextShot = 0;

    int bulletNexst = 4;

    float rotationSpeed = 5.0f; // 回転の慣性調整
    private Vector3 playerRotation;    // 現在の回転値
    private Vector3 targetRotation;    // 目標の回転値
    float triggerValue;
    float moveX;
    float moveY;
    float moveZ;
    float bounceDistance = 2.0f; // 弾かれる距離
    float bounceDisableTime = 0.3f; // 操作無効時間
    private bool isBounced = false; // 操作無効フラグ
    private float bounceTimer = 0.0f; // 無効時間計測用
    bool isBlockedForward = false; // 前進禁止フラグ
    public float checkDistance = 0.0f; // 障害物チェック距離

   
    void Start()
    {
        playerRotation = Vector3.zero;
       
    }



    // Update is called once per frame
    void Update()
    {
       
        // 通常の操作処理
        MovePlayer();


       
        //if (!isBlockedForward && isBounced == false)
        //{
            transform.position += playerZSpeed * Vector3.forward * Time.deltaTime;
       // }
        // 慣性をつけて回転をスムーズにする
        playerRotation = Vector3.Lerp(playerRotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(playerRotation);



        // 弾の発射処理
        HandleShooting();
        //壁にぶつかった時の処理
        if (transform.position.x >= 34.0f)
        {
            sparkR.Play(); // パーティクル再生

        }
        else 
        {
            sparkR.Stop(); // パーティクルストップ
        }

        if (transform.position.x <= -34.0f)
        {
            sparkL.Play(); // パーティクル再生

        }
        else
        {
            sparkL.Stop(); // パーティクルストップ
        }

    }





    void MovePlayer()
    {
        Vector3 move = new Vector3(moveX, moveY, moveZ) * playerSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + move;

        // 移動範囲を制限
        newPosition.x = Mathf.Clamp(newPosition.x, -34.8f, 34.8f);
        newPosition.y = Mathf.Clamp(newPosition.y, -5f, 54f);


        transform.position = newPosition;

        moveX = Input.GetAxis("L_Stick_H"); // A（-1）D（+1）、Lスティック左右
        moveY = Input.GetAxis("L_Stick_V");   // W（+1）S（-1）、Lスティック上下

       
        // キーボード & コントローラー両対応の移動処理
      

        // 移動制限（範囲: X[-20,20], Y[-5,15]）
       
        Collider[] hitColliders = Physics.OverlapBox(newPosition, transform.localScale / 2);

        transform.position = newPosition;



        if (transform.position.z >= 3300)
        {
            SceneManager.LoadScene("ClearScene"); // "NextSceneName" を切り替えたいシーン名に変更
        }

        if (Input.GetKey(KeyCode.G))
        {
            SceneManager.LoadScene("GameOverScene");
        }




        // 機体の傾き調整（ターゲット回転）
        if (moveY > 0) targetRotation.x = Mathf.Max(targetRotation.x - 10, -35); // 前進
        else if (moveY < 0) targetRotation.x = Mathf.Min(targetRotation.x + 2, 20); // 後退
        else targetRotation.x = Mathf.Lerp(targetRotation.x, 0, Time.deltaTime * rotationSpeed);

        if (moveX > 0) targetRotation.z = Mathf.Max(targetRotation.z - 2, -200); // 右移動
        else if (moveX < 0) targetRotation.z = Mathf.Min(targetRotation.z + 2, 200); // 左移動
        else targetRotation.z = Mathf.Lerp(targetRotation.z, 0, Time.deltaTime * rotationSpeed);




        ////前に移動

        ////デバックのために残しとく
        //// Wキー（前方移動）
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += playerSpeed * Vector3.up * Time.deltaTime;
            //targetRotation.x = Mathf.Max(targetRotation.x - 10, -35);
        }
        else
        {

            targetRotation.x = Mathf.Lerp(targetRotation.x, 0, Time.deltaTime * rotationSpeed);
        }

        // Sキー（後方移動）
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= playerSpeed * Vector3.up * Time.deltaTime;
            targetRotation.x = Mathf.Min(targetRotation.x + 2, 20);
        }
        else
        {
            targetRotation.x = Mathf.Lerp(targetRotation.x, 0, Time.deltaTime * rotationSpeed);
        }

        // Dキー（右移動）
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += playerSpeed * Vector3.right * Time.deltaTime;
            targetRotation.z = Mathf.Max(targetRotation.z - 2, -35);
        }
        else
        {
            targetRotation.z = Mathf.Lerp(targetRotation.z, 0, Time.deltaTime * rotationSpeed);
        }

        // Aキー（左移動）
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= playerSpeed * Vector3.right * Time.deltaTime;
            targetRotation.z = Mathf.Min(targetRotation.z + 2, 35);
        }
        else
        {
            targetRotation.z = Mathf.Lerp(targetRotation.z, 0, Time.deltaTime * rotationSpeed);
        }

    }
   
    void HandleShooting()
    {
        triggerValue = Input.GetAxis("RightTrigger");
        timeUntilNextShot--;

        if ((Input.GetKey(KeyCode.Space) || triggerValue > 0.1f) && timeUntilNextShot <= 0)
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);
            timeUntilNextShot = bulletNexst;
        }

       
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyWoll"))
        {
          //  ContactPoint contact = collision.contacts[0]; // 最初の接触点
          //  Vector3 normal = contact.normal;  // 衝突法線

          //  Debug.Log("衝突検出: " + collision.gameObject.name + " / 法線: " + normal);

          //  Vector3 bounceDirection = Vector3.zero; // 弾かれる方向

          //  ////  **正面からの衝突（Z軸）**
          //  if (normal.z < checkDistance) // ほぼ正面から当たった場合
          //  {
          //      //isBlockedForward = true; // 前進を禁止
          //      //bounceDirection.z = -Mathf.Sign(normal.z);
          //  }
          //  // **横方向の衝突（X軸）**
          //  if (Mathf.Abs(normal.x) > Mathf.Abs(normal.z) && Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
          //  {
          //      bounceDirection.x = -Mathf.Sign(normal.x); // 右の壁なら左へ、左の壁なら右へ
          //  }
          // // **上下方向の衝突（Y軸）**
          //  if (Mathf.Abs(normal.y) > Mathf.Abs(normal.z))
          //  {
          //      bounceDirection.y = -Mathf.Sign(normal.y); // 天井なら下へ、床なら上へ
          //  }

           

          //  //  **弾かれる処理 * *
          //  transform.position -= bounceDirection * bounceDistance;

          //  Debug.Log("弾かれる方向: " + bounceDirection);

          ////  **一定時間操作を無効化 * *
          // isBounced = true;
          // bounceTimer = bounceDisableTime;

           
        }

       
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(enemySpawnScript.enemySpawns);

        if (collision.gameObject.tag == "PerspectiveOn")
        {

            Debug.Log("方向変換");


        }

        if (collision.gameObject.tag == "PerspectiveOff")
        {

            Debug.Log("方向変換2");


        }

    }


   
   
}
  


  


    



