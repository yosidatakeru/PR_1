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

//public class PlayerScript : MonoBehaviour
//{
//    // 発射する弾のプレハブ
//    public GameObject Bullet;

//    // 右壁との接触時のパーティクル
//    public ParticleSystem sparkR;
//    // 左壁との接触時のパーティクル
//    public ParticleSystem sparkL;

//    public ParticleSystem accelerationeffect;

//    public GameObject defense;

  
//    // プレイヤーの操作による移動速度
//    float playerSpeed = 1f;
//    // 前方への自動移動速度
//    public float forwardSpeed = 30f;

//    // 次の弾が撃てるまでのクールタイム
//    int timeUntilNextShot = 0;
//    // 弾の連射間隔（フレーム数）
//    int bulletNext = 4;

//    // 回転のスムーズさ
//    float rotationSpeed = 5.0f;
//    // 現在の回転
//    private Vector3 playerRotation;

//    // ターゲット回転値（傾き用）
//    private Vector3 targetRotation;

//    // トリガー入力
//    float triggerValue;

//    // 壁にぶつかったときの反発距離
//    float bounceDistance = 2.0f;
//    // 反発後の一時的な操作無効時間
//    float bounceDisableTime = 1f;
//    private bool isBounced = false;
//    // 反発中フラグ
//    private float bounceTimer = 0.0f;
//    // 反発の残り時間

//    // 現在の速度
//    Vector3 velocity;
//    // 減速率
//    float damping = 1f;
//    // 最大速度
//    float maxSpeed = 25f;

//    // ロール回転のZ角度
//    private float rollZAngle = 0f;
//    // バレルロール中かどうか
//    bool isRolling = false;
//    private bool isInvincible;

//    // ロール持続時間
//    // float rollTime = 0.5f;

//    HPScript hpScript;

//    //デバック用
//    bool invincibl = false;

//    bool isControlEnabled = true;

//    float fallSpeed = 0.1f;

//    public bool acceleration = false;

//    float goalLine = 3300f;

//    void Start()
//    {

//        playerRotation = Vector3.zero;

//        sparkR.Stop();

//        sparkL.Stop();

//        accelerationeffect.Stop();

//        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();
//    }

//    void Update()
//    {
//        // プレイヤーの座標によって操作可能フラグを切り替え
//        if (transform.position.z <= 50f || transform.position.z >= goalLine || hpScript.Gauge <= 0)
//        {
//            isControlEnabled = false;
//        }
//        else
//        {
//            isControlEnabled = true;
//        }
//        if (hpScript.Gauge >= 0)
//        {
//            // 常に前方へ進む
//            transform.position += forwardSpeed * Vector3.forward * Time.deltaTime;
//        }

//        if (goalLine <= transform.position.z)
//        {
//            acceleration = false;
//            playerRotation = Vector3.zero;
//        }
//        GameOver();

//        // フラグが無効なら以降の入力や移動処理をスキップ
//        if (isControlEnabled == false)
//        {
//            return;
//        }
//        //デバック用

//        if (Input.GetKey(KeyCode.Alpha0))
//        {
//            hpScript.Gauge = 0;

//        }
//        if (Input.GetKey(KeyCode.Alpha1))
//        {
//            hpScript.Gauge = 20000;

//        }

//        if (Input.GetKey(KeyCode.Alpha2))
//        {
//            invincibl = true;

//        }

//        if (Input.GetKey(KeyCode.Alpha3))
//        {
//            forwardSpeed = 100;
//        }

//        HandleInput();
//        UpdateRotation();
//        UpdateMovement();
//        Acceleration();
//        UpdateParticles();
//        HandleShooting();
//        UpdateBounceTimer();




//    }


    
//    void HandleInput()
//    {
//        // バレルロール開始
//        if (Input.GetKeyDown(KeyCode.Q) && !isRolling || Input.GetButtonDown("LB") && !isRolling)
//        {
//            StartCoroutine(DoBarrelRoll());
//        }
//    }

//    // 回転（プレイヤー傾き + ロール）更新
//    void UpdateRotation()
//    {
//        playerRotation = Vector3.Lerp(playerRotation, targetRotation, Time.deltaTime * rotationSpeed);
//        transform.rotation = Quaternion.Euler(playerRotation.x, playerRotation.y, playerRotation.z + rollZAngle);
//    }

//    // プレイヤーの移動処理
//    void UpdateMovement()
//    {

//        float inputX = Input.GetAxis("L_Stick_H");
//        float inputY = Input.GetAxis("L_Stick_V");

//        if (Input.GetKey(KeyCode.D)) inputX += 1f;
//        if (Input.GetKey(KeyCode.A)) inputX -= 1f;
//        if (Input.GetKey(KeyCode.W)) inputY += 1f;
//        if (Input.GetKey(KeyCode.S)) inputY -= 1f;
       

//        Vector3 input = new Vector3(inputX, inputY, 0f);
//        if (input.magnitude > 1f) input.Normalize();

//        // 加速度的な移動
//        velocity += input * playerSpeed;
//        if (velocity.magnitude > maxSpeed)
//        {
//            velocity = velocity.normalized * maxSpeed ;
           
//        }

//        // 徐々に減速
//        velocity = Vector3.Lerp(velocity, Vector3.zero, damping * Time.deltaTime);

//        // 位置更新
//        Vector3 newPosition = transform.position + velocity * Time.deltaTime;
//        newPosition.x = Mathf.Clamp(newPosition.x, -34.8f, 34.8f);
//        newPosition.y = Mathf.Clamp(newPosition.y, -5f, 54f);
//        transform.position = newPosition;


//        // =====================================
//        // 傾き処理：速度に比例してスムーズに回転
//        // =====================================

//        // X軸（上下）傾き：maxPitchAngle を上限にして滑らかに傾く
//        float maxPitchAngle = 35f; // 上下傾きの最大角
//        float targetPitch = -velocity.y / maxSpeed * maxPitchAngle;
//        targetRotation.x = Mathf.Lerp(targetRotation.x, targetPitch, Time.deltaTime * rotationSpeed);

//        // Z軸（左右）傾き：maxRollAngle を上限にして滑らかに傾く
//        float maxRollAngle = 35f;
//        float targetRoll = -velocity.x / maxSpeed * maxRollAngle;
//        targetRotation.z = Mathf.Lerp(targetRotation.z, targetRoll, Time.deltaTime * rotationSpeed);
//    }


//    // 弾の発射処理
//    void HandleShooting()
//    {
//        triggerValue = Input.GetAxis("RightTrigger");
//        timeUntilNextShot--;

//        if ((Input.GetKey(KeyCode.Space) || triggerValue > 0.1f) && timeUntilNextShot <= 0)
//        {
//           /// Vector3 muzzleOffset = transform.forward * 2f;
//            Instantiate(Bullet, transform.position, transform.rotation);
//            timeUntilNextShot = bulletNext;
//        }
//    }
//    // 壁エフェクトのオンオフ制御
//    void UpdateParticles()
//    {
//        if (transform.position.x >= 34.0f)
//        {
//            if (!sparkR.isPlaying) sparkR.Play();
//        }
//        else if (sparkR.isPlaying)
//        {
//            sparkR.Stop();
//        }

//        if (transform.position.x <= -34.0f)
//        {
//            if (!sparkL.isPlaying) sparkL.Play();
//        }
//        else if (sparkL.isPlaying)
//        {
//            sparkL.Stop();
//        }
//    }
//    // 反発後の無操作時間カウント
//    void UpdateBounceTimer()
//    {
//        if (isBounced)
//        {
//            bounceTimer -= Time.deltaTime;
//            if (bounceTimer <= 0f)
//            {
//                isBounced = false;
//            }
//        }
//    }
//    // 壁接触時に反発する処理
//    void OnCollisionStay(Collision collision)
//    {
//        if (invincibl == false)
//        {
//            if (collision.gameObject.CompareTag("EnemyWoll"))
//            {
//                ContactPoint contact = collision.contacts[0];
//                Vector3 normal = contact.normal;
//                Vector3 absNormal = new Vector3(Mathf.Abs(normal.x), Mathf.Abs(normal.y), Mathf.Abs(normal.z));

//                Vector3 bounceDirection = Vector3.zero;

//                if (absNormal.x > absNormal.y && absNormal.x > absNormal.z)
//                {
//                    bounceDirection = new Vector3(-Mathf.Sign(normal.x), 0, 0);
//                    velocity.x = 0;
//                }
//                else if (absNormal.y > absNormal.x && absNormal.y > absNormal.z)
//                {
//                    bounceDirection = new Vector3(0, -Mathf.Sign(normal.y), 0);
//                    velocity.y = 0;
//                }
//                else
//                {
//                    bounceDirection = new Vector3(0, 0, -Mathf.Sign(normal.z));
//                    forwardSpeed = 0;
//                    hpScript.Gauge = 0;
//                }
//                transform.position -= bounceDirection * bounceDistance;
//                isBounced = true;
//                bounceTimer = bounceDisableTime;


               
//                //Debug.Log("Bounce direction: " + bounceDirection);]

//            }
//        }
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("PerspectiveOn"))
//        {
//            Debug.Log("方向変換");
//        }
//        if (collision.gameObject.CompareTag("PerspectiveOff"))
//        {
//            Debug.Log("方向変換2");
//        }
//    }
//    // バレルロールの実行
//    IEnumerator DoBarrelRoll()
//    {
//        isRolling = true; // プレイヤーが回転中かどうかのフラグ
//        isInvincible = true; // 回転中は無敵にする

//        float elapsed = 0f;
//        float startZ = rollZAngle;
//        int rollCount = 2;
//        float duration = 1.5f;

//        float endZ = startZ + (360f * rollCount);
       

//        while (elapsed < duration)
//        {
//            float t = elapsed / duration;
//            rollZAngle = Mathf.Lerp(startZ, endZ, t);
//            elapsed += Time.deltaTime;
            

            
//                RepelNearbyBullets(); // 弾をはじく処理
               
            

//            yield return null;
//        }

//        rollZAngle = endZ % 720f;
//        isRolling = false;
//        isInvincible = false; // 回転終了で無敵解除
//    }

//    // 近くの敵弾を削除（はじく処理）
//    void RepelNearbyBullets()
//    {
//        float repelRadius = 5.0f;
//        int layerMask = 1 << LayerMask.NameToLayer("EnemyBullet");
//        Collider[] hitColliders = Physics.OverlapSphere(transform.position, repelRadius, layerMask);

//        foreach (var hit in hitColliders)
//        {
//            TrackingBilltScript tracking = hit.GetComponent<TrackingBilltScript>();
//            PlayerFollowingBulletScript following = hit.GetComponent<PlayerFollowingBulletScript>();
//            // hpScript.Gauge += 10;

//            if (tracking != null || following != null)
//            {
//                Instantiate(defense, transform.position, Quaternion.identity);
//                Destroy(hit.gameObject);
//            }
//        }
//    }

//    void GameOver()
//    {
//        if (hpScript.Gauge == 0)
//        {
//            StartCoroutine(FallAndRotate());
//        }
//    }

//    void Acceleration() 
//    {
//        if (Input.GetButtonDown("L3") || Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift))
//        {
//            acceleration = !acceleration;
//        }

//        if (acceleration == true)
//        {
//            accelerationeffect.Play();
//            forwardSpeed = 50;
//        }
//        else 
//        {
//            accelerationeffect.Stop();
//            forwardSpeed = 30;
          
//        }

        

        

//    }

//    IEnumerator FallAndRotate()
//    {
       
//        float rotationSpeed = 1f;  // 毎秒90度回転に変更（見やすい速さ）

//        Vector3 fallDirection = new Vector3(0, -1, 1).normalized;

//        // 最初に斜め下を向かせる（回転を固定）
//       // transform.rotation = Quaternion.LookRotation(fallDirection);

//        while (true)
//        {
//            // 斜め下に一定速度で移動
//            transform.position += fallDirection * fallSpeed * Time.deltaTime;

//            // ローカルZ軸まわりに一定速度で回転（プレイヤーの正面を軸に回転）
//            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.World);

//            yield return null;
//        }
//    }

//    internal bool IsInvincible()
//    {
//        return isInvincible; // バレルロール中などに true にする
//    }
//}

  
