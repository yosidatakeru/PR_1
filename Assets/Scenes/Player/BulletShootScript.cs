using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletShootScript : MonoBehaviour
{
    public float speed = 20f;//弾の速さ
    public float lifetime = 5f;
    public float homingStrength = 15f;    // 誘導の強さ
    public float detectionRadius = 0.2f;  // 検出範囲
    private Rigidbody rb;                // 物理エンジン
    private GameObject target;           // 追尾するターゲット
  
    private Vector3 moveDirection; // 発射方向

    // Start is called before the first frame update
    Vector3 BulletPos = Vector3.zero;

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
            // ターゲット方向に少しずつ誘導
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            moveDirection = Vector3.Lerp(moveDirection, directionToTarget, homingStrength * Time.fixedDeltaTime);
        }

        // 弾を移動させる
        rb.velocity = moveDirection * speed;
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

    // 検出範囲を可視化（シーンビュー）
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  // 赤色で表示
        Gizmos.DrawWireSphere(transform.position, detectionRadius);  // 検出範囲を球体で描画
    }
    void OnCollisionEnter(Collision collision)
    {
       

        if (collision.gameObject.tag == "EnemyWoll")
        {
            GetComponent<SphereCollider>().enabled = false;
          
            Destroy(gameObject);



        }

    }

}

