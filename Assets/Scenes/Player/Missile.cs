using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 15f; // 弾の速度
    public float rotationSpeed = 30f; // 回転速度
    public float detectionRadius = 2.0f; // 衝突判定の半径


    private Transform target; // ターゲット
    private Rigidbody rb;
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // ← ここを変更
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            // ターゲットがいない場合、弾を削除
            Destroy(gameObject);
            return;
        }

        // ターゲットに向かう
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 前進
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        
        FixedUpdate();


    }

    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        rb.velocity = transform.forward * speed; // ← 物理エンジンを利用した移動

        // Raycast で進行方向に障害物があるかチェック
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, speed * Time.fixedDeltaTime))
        {
            Debug.Log("Hit detected via Raycast: " + hit.collider.gameObject.name);
           // Explode();
        }

        DetectCollision();
    }

    void DetectCollision()
    {
        float detectionDistance = speed * Time.fixedDeltaTime; // フレーム間の移動距離
        LayerMask enemyLayer = LayerMask.GetMask("Enemy"); // 敵レイヤーのみを対象にする

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, detectionRadius, transform.forward, out hit, detectionDistance, enemyLayer))
        {
            Debug.Log("Missile hit detected (SphereCast): " + hit.collider.gameObject.name);
            Explode();
        }
        else
        {
            Debug.Log("SphereCast did not hit anything.");
        }

        // OverlapSphere も試す（最後の保険）
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                Debug.Log("Missile hit detected (OverlapSphere): " + col.gameObject.name);
              //  Explode();
                return;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            Explode();
        }
    }

    void Explode()
    {
        // 自身を削除
        Destroy(gameObject);
    }

}
