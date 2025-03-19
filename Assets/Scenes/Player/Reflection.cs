using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : MonoBehaviour
{
    float speed = 100f;//弾の速さ
    float lifetime = 8f;//消すまでの時間
    float homingStrength = 10f;    // 誘導の強さ
    float detectionRadius = 10.0f;  // 検出範囲
    private Rigidbody rb;                // 物理エンジン
    private GameObject target;           // 追尾するターゲット

    private Vector3 moveDirection; // 発射方向

    // Start is called before the first frame update
    Vector3 BulletPos = Vector3.zero;
    float reflectionStrength = 10.0f;  // 反射の強さ（1.0f は元の反射の強さ）
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // プレイヤーの位置を取得
        GameObject player = GameObject.FindGameObjectWithTag("Frame");
        // 近くの敵を探してターゲットに設定
        target = FindClosestEnemy();
        if (player != null)
        {
            // 発射方向を計算（正規化して速度に影響を与えないようにする）
            moveDirection = (player.transform.position - transform.position).normalized;
        }
        else
        {
            // 念のためデフォルトで前方向に進む
            moveDirection = transform.forward;
        }

        // 一定時間後に自動で削除
        Destroy(gameObject, lifetime);

    }

    // Update is called once per frame
    void Update()
    {

        // ターゲット位置に向かって移動
        transform.position += moveDirection * speed * Time.deltaTime;


    }

    void FixedUpdate()
    {
        if (target == null)
        {
            target = FindClosestEnemy();  // 近くの敵を探す
        }

        if (target != null)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

            // 目標が近い場合は方向を完全に切り替える
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget < 3.0f) // 近づいたら即ターゲット方向に変更
            {
                moveDirection = directionToTarget;
            }
            else
            {
                moveDirection = Vector3.Slerp(moveDirection, directionToTarget, homingStrength * 2 * Time.fixedDeltaTime).normalized;
            }
        }

        // Rigidbodyの速度を更新
        rb.velocity = moveDirection * speed;

        Debug.DrawLine(transform.position, transform.position + moveDirection * 3.0f, Color.blue);
    }

    // 最も近い敵を探す
    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
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

    
    void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "EnemyWoll")
        {
            // 衝突した面の法線ベクトルを取得
            Vector3 normal = collision.contacts[0].normal;

            // 衝突の反射ベクトルを計算
            Vector3 reflectDirection = Vector3.Reflect(transform.forward, normal);

            // 反射の強さを調整（反射ベクトルにスカラー値を掛けて強さを増す）
          
            reflectDirection *= reflectionStrength;

            // 反射方向に移動する
            transform.forward = reflectDirection; // 進行方向を反射方向に設定

            // 反射後に移動方向を更新
            moveDirection = reflectDirection;

            // 反射方向に移動
            transform.position += reflectDirection * speed * Time.deltaTime;
        }

    }

    
}
