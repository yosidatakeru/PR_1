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

public class PlayerScript : MonoBehaviour
{
    public GameObject Bullet;
    public ParticleSystem sparkR;
    public ParticleSystem sparkL;

    //プレイヤーの移動スピード
    float playerSpeed = 1f;

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

    Vector3 velocity; // ← プレイヤーの速度を保持する変数を用意
    float damping = 1f; // ← 減速する速さ（慣性の強さ）
    float maxSpeed = 30f;

    private float rollZAngle = 0f; // ロール用のZ回転角
    bool isRolling = false; // ロール中かどうか
    float rollTime = 0.5f; // ロールの持続時間

    void Start()
    {
        playerRotation = Vector3.zero;
        sparkR.Stop();
        sparkL.Stop();
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q) && !isRolling)
        {
            StartCoroutine(DoBarrelRoll());
        }

        // 慣性をつけて回転をスムーズにする
        playerRotation = Vector3.Lerp(playerRotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Z軸にrollZAngleを加算（playerRotation.z + rollZAngle）
        transform.rotation = Quaternion.Euler(playerRotation.x, playerRotation.y, playerRotation.z + rollZAngle);
        // 通常の操作処理
        MovePlayer();





        transform.position += playerZSpeed * Vector3.forward * Time.deltaTime;




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
        float inputX = Input.GetAxis("L_Stick_H");
        float inputY = Input.GetAxis("L_Stick_V");

        if (Input.GetKey(KeyCode.D)) inputX += 1f;
        if (Input.GetKey(KeyCode.A)) inputX -= 1f;
        if (Input.GetKey(KeyCode.W)) inputY += 1f;
        if (Input.GetKey(KeyCode.S)) inputY -= 1f;

        Vector3 input = new Vector3(inputX, inputY, 0f);
        if (input.magnitude > 1f) input.Normalize();

        // 加速
        velocity += input * playerSpeed;

        // ★最高速度制限
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        // 減衰
        velocity = Vector3.Lerp(velocity, Vector3.zero, damping * Time.deltaTime);

        // 移動
        Vector3 newPosition = transform.position + velocity * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -34.8f, 34.8f);
        newPosition.y = Mathf.Clamp(newPosition.y, -5f, 54f);

        transform.position = newPosition;

        // 傾き
        if (velocity.y > 0.1f) targetRotation.x = Mathf.Max(targetRotation.x - 10, -35);
        else if (velocity.y < -0.1f) targetRotation.x = Mathf.Min(targetRotation.x + 2, 20);
        else targetRotation.x = Mathf.Lerp(targetRotation.x, 0, Time.deltaTime * rotationSpeed);

        if (velocity.x > 0.1f) targetRotation.z = Mathf.Max(targetRotation.z - 2, -35);
        else if (velocity.x < -0.1f) targetRotation.z = Mathf.Min(targetRotation.z + 2, 35);
        else targetRotation.z = Mathf.Lerp(targetRotation.z, 0, Time.deltaTime * rotationSpeed);
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

            ContactPoint contact = collision.contacts[0]; // 最初の接触点
            Vector3 normal = contact.normal;  // 衝突法線

            Debug.Log("衝突検出: " + collision.gameObject.name + " / 法線: " + normal);

            Vector3 bounceDirection = Vector3.zero; // 弾かれる方向

            ////  **正面からの衝突（Z軸）**
            if (normal.z < checkDistance) // ほぼ正面から当たった場合
            {
                bounceDirection.x = -Mathf.Sign(normal.x);
            }
            // **横方向の衝突（X軸）**
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.z) && Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                bounceDirection.x = -Mathf.Sign(normal.x); // 右の壁なら左へ、左の壁なら右へ
            }
            // **上下方向の衝突（Y軸）**
            if (Mathf.Abs(normal.y) > Mathf.Abs(normal.z))
            {
                bounceDirection.x = -Mathf.Sign(normal.x);// 天井なら下へ、床なら上へ
            }



            //  **弾かれる処理 * *
            transform.position -= bounceDirection * bounceDistance;

            Debug.Log("弾かれる方向: " + bounceDirection);

            //  **一定時間操作を無効化 * *
            isBounced = true;
            bounceTimer = bounceDisableTime;


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


    IEnumerator DoBarrelRoll()
    {
        isRolling = true;
        float elapsed = 0f;

        float startZ = rollZAngle;
        float endZ = startZ + 360f;

        float repelInterval = 0.05f; // はじく処理の間隔（重すぎ防止）
        float repelTimer = 0f;

        while (elapsed < rollTime)
        {
            float t = elapsed / rollTime;
            rollZAngle = Mathf.Lerp(startZ, endZ, t);
            elapsed += Time.deltaTime;
            repelTimer += Time.deltaTime;

            if (repelTimer >= repelInterval)
            {
                RepelNearbyBullets();
                repelTimer = 0f;
            }

            yield return null;
        }

        rollZAngle = endZ % 360f;
        isRolling = false;
    }

    void RepelNearbyBullets()
    {
        float repelRadius = 5.0f; // はじく半径
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, repelRadius);

        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("EnemyBullet"))
            {
                // 方法①: 弾を消す
                // Destroy(hit.gameObject);

               // EnemyBullet bulletScript = hit.GetComponent<EnemyBullet>();
                //if (bulletScript != null)
                //{
                //    bulletScript.ReflectFrom(transform.position);
                //}
            }
        }


    }
}
  


  


    



