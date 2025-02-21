using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BulletScript : MonoBehaviour
{
    float bulletSpeed = 20;
    public float homingStrength = 10f;   // 弾がどれくらい引き寄せられるか
    public float detectionRadius = 50f;  // 敵を検出する範囲
    private Rigidbody rb;                // Rigidbody（物理エンジン）
    private GameObject target;           // ロックオンするターゲット

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed; // 初速設定
    }

     
    // Update is called once per frame
    void Update()
    {
        transform.position += bulletSpeed * transform.forward * Time.deltaTime;
        Destroy(gameObject, 5);

    }

    void FixedUpdate()
    {
        if (target == null)
        {
            target = FindClosestEnemy();  // 近くの敵を探す
        }

        if (target != null)
        {
            // 弾が敵の方向に向かう
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            Vector3 homingDirection = Vector3.Lerp(rb.velocity.normalized, directionToTarget, homingStrength * Time.fixedDeltaTime);
            rb.velocity = homingDirection * bulletSpeed;  // 新しい速度を設定
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");  // 敵タグのオブジェクトを検索
        GameObject closestEnemy = null;
        float closestDistance = detectionRadius;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  // 赤色で表示
        Gizmos.DrawWireSphere(transform.position, detectionRadius);  // 検出範囲を球体で描画
    }
    void OnCollisionEnter(Collision Bullet)
    {
      
        if (Bullet.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
         

        
    }

}
