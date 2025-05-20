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
    // 発射する弾のプレハブ
    public GameObject Bullet;

    // 右壁との接触時のパーティクル
    public ParticleSystem sparkR;
    // 左壁との接触時のパーティクル
    public ParticleSystem sparkL;

    // プレイヤーの操作による移動速度
    float playerSpeed = 1f;
    // 前方への自動移動速度
    float forwardSpeed = 30f;

    // 次の弾が撃てるまでのクールタイム
    int timeUntilNextShot = 0;
    // 弾の連射間隔（フレーム数）
    int bulletNext = 4;

    // 回転のスムーズさ
    float rotationSpeed = 5.0f;
    // 現在の回転
    private Vector3 playerRotation;

    // ターゲット回転値（傾き用）
    private Vector3 targetRotation;

    // トリガー入力
    float triggerValue;

    // 壁にぶつかったときの反発距離
    float bounceDistance = 2.0f;
    // 反発後の一時的な操作無効時間
    float bounceDisableTime = 0.3f;
    private bool isBounced = false;
    // 反発中フラグ
    private float bounceTimer = 0.0f;
    // 反発の残り時間

    // 現在の速度
    Vector3 velocity;
    // 減速率
    float damping = 1f;
    // 最大速度
    float maxSpeed = 30f;

    // ロール回転のZ角度
    private float rollZAngle = 0f;
    // バレルロール中かどうか
    bool isRolling = false;
    // ロール持続時間
    float rollTime = 0.5f;

    HPScript hpScript;

    void Start()
    {
      
        playerRotation = Vector3.zero;
       
        sparkR.Stop();
       
        sparkL.Stop();

        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();
    }

    void Update()
    {
        // 入力処理（ロール開始）
        HandleInput();
        
        // 回転処理（傾きやロール）
        UpdateRotation();

        // 移動処理（通常）
        UpdateMovement();

        // 壁との接触エフェクト処理
        UpdateParticles();

        // 弾発射処理
        HandleShooting();

        // 反発タイマー更新
        UpdateBounceTimer();
    }

    void HandleInput()
    {
        // バレルロール開始
        if (Input.GetKeyDown(KeyCode.Q) && !isRolling)
        {
            StartCoroutine(DoBarrelRoll());
        }
    }

    // 回転（プレイヤー傾き + ロール）更新
    void UpdateRotation()
    {
        playerRotation = Vector3.Lerp(playerRotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(playerRotation.x, playerRotation.y, playerRotation.z + rollZAngle);
    }

    // プレイヤーの移動処理
    void UpdateMovement()
    {
        // 反発中は操作不能
        if (isBounced) 
        {
            return;
        }

        // 入力取得
        float inputX = Input.GetAxis("L_Stick_H");
        float inputY = Input.GetAxis("L_Stick_V");

        if (Input.GetKey(KeyCode.D))
        { 
            inputX += 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputX -= 1f;
        }
        
        if (Input.GetKey(KeyCode.W)) 
        { 
            inputY += 1f;
        }

        if (Input.GetKey(KeyCode.S)) 
        {
            inputY -= 1f; 
        }


        Vector3 input = new Vector3(inputX, inputY, 0f);
        if (input.magnitude > 1f) 
        { 
            input.Normalize(); 
        }



        // 加速度的な移動
        velocity += input * playerSpeed;
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        // 徐々に減速
        velocity = Vector3.Lerp(velocity, Vector3.zero, damping * Time.deltaTime);



        Vector3 newPosition = transform.position + velocity * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -34.8f, 34.8f);
        newPosition.y = Mathf.Clamp(newPosition.y, -5f, 54f);
        transform.position = newPosition;



        // 傾き
        if (velocity.y > 0.1f)
        {
            targetRotation.x = Mathf.Max(targetRotation.x - 10, -35);
        }
        else if (velocity.y < -0.1f)
        {
            targetRotation.x = Mathf.Min(targetRotation.x + 2, 20);
        }
        else
        {
            targetRotation.x = Mathf.Lerp(targetRotation.x, 0, Time.deltaTime * rotationSpeed);
        }


        if (velocity.x > 0.1f)
        {
            targetRotation.z = Mathf.Max(targetRotation.z - 2, -35);
        }
        else if (velocity.x < -0.1f)
        {
            targetRotation.z = Mathf.Min(targetRotation.z + 2, 35);
        }
        else
        {
            targetRotation.z = Mathf.Lerp(targetRotation.z, 0, Time.deltaTime * rotationSpeed);
        }

        // 常に前方に進む
        transform.position += forwardSpeed * Vector3.forward * Time.deltaTime;


    }
    // 弾の発射処理
    void HandleShooting()
    {
        triggerValue = Input.GetAxis("RightTrigger");
        timeUntilNextShot--;

        if ((Input.GetKey(KeyCode.Space) || triggerValue > 0.1f) && timeUntilNextShot <= 0)
        {
            Vector3 muzzleOffset = transform.forward * 2f;
            Instantiate(Bullet, transform.position + muzzleOffset, transform.rotation);
            timeUntilNextShot = bulletNext;
        }
    }
    // 壁エフェクトのオンオフ制御
    void UpdateParticles()
    {
        if (transform.position.x >= 34.0f)
        {
            if (!sparkR.isPlaying) sparkR.Play();
        }
        else if (sparkR.isPlaying)
        {
            sparkR.Stop();
        }

        if (transform.position.x <= -34.0f)
        {
            if (!sparkL.isPlaying) sparkL.Play();
        }
        else if (sparkL.isPlaying)
        {
            sparkL.Stop();
        }
    }
    // 反発後の無操作時間カウント
    void UpdateBounceTimer()
    {
        if (isBounced)
        {
            bounceTimer -= Time.deltaTime;
            if (bounceTimer <= 0f)
            {
                isBounced = false;
            }
        }
    }
    // 壁接触時に反発する処理
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyWoll"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 normal = contact.normal;
            Vector3 absNormal = new Vector3(Mathf.Abs(normal.x), Mathf.Abs(normal.y), Mathf.Abs(normal.z));

            Vector3 bounceDirection = Vector3.zero;

            if (absNormal.x > absNormal.y && absNormal.x > absNormal.z)
                bounceDirection = new Vector3(-Mathf.Sign(normal.x), 0, 0);
            else if (absNormal.y > absNormal.x && absNormal.y > absNormal.z)
                bounceDirection = new Vector3(0, -Mathf.Sign(normal.y), 0);
            else
                bounceDirection = new Vector3(0, 0, -Mathf.Sign(normal.z));

            transform.position -= bounceDirection * bounceDistance;
            isBounced = true;
            bounceTimer = bounceDisableTime;

            Debug.Log("Bounce direction: " + bounceDirection);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PerspectiveOn"))
        {
            Debug.Log("方向変換");
        }
        if (collision.gameObject.CompareTag("PerspectiveOff"))
        {
            Debug.Log("方向変換2");
        }
    }
    // バレルロールの実行
    IEnumerator DoBarrelRoll()
    {
        isRolling = true;
        float elapsed = 0f;

        float startZ = rollZAngle;
        int rollCount = 2;
        float duration = 1.5f;

        float endZ = startZ + (360f * rollCount);
        float repelInterval = 0.05f;
        float repelTimer = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            rollZAngle = Mathf.Lerp(startZ, endZ, t);
            elapsed += Time.deltaTime;
            repelTimer += Time.deltaTime;

            if (repelTimer >= repelInterval)
            {
                // 弾をはじく
                RepelNearbyBullets();
                repelTimer = 0f;
            }

            yield return null;
        }

        rollZAngle = endZ % 720f;
        isRolling = false;
    }

    // 近くの敵弾を削除（はじく処理）
    void RepelNearbyBullets()
    {
        float repelRadius = 5.0f;
        int layerMask = 1 << LayerMask.NameToLayer("EnemyBullet");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, repelRadius, layerMask);

        foreach (var hit in hitColliders)
        {
            TrackingBilltScript tracking = hit.GetComponent<TrackingBilltScript>();
            PlayerFollowingBulletScript following = hit.GetComponent<PlayerFollowingBulletScript>();
           // hpScript.Gauge += 10;

            if (tracking != null || following != null)
            {
                Destroy(hit.gameObject);
            }
        }
    }
}

  
