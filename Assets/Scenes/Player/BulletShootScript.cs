
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletShootScript : MonoBehaviour
{
    private float speed = 170f;
    private float lifetime = 1f;
    private float homingStrength = 10f;
    private float detectionRadius = 8f;

    private Rigidbody rb;
    private SphereCollider sphereCollider;
    private GameObject target;
    private PlayerController playerController;

    private Vector3 moveDirection;

    private float detectionRadiusSqr;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();

        detectionRadiusSqr = detectionRadius * detectionRadius;

        // 元の発射方向を維持
        GameObject player = GameObject.FindGameObjectWithTag("Frame");

        if (player != null)
        {
            moveDirection =
                (player.transform.position - transform.position).normalized;
        }
        else
        {
            moveDirection = transform.forward;
        }

        // 元と同じタイミングで敵を検索
        target = FindClosestEnemy();

        // 一定時間後に削除
        Destroy(gameObject, lifetime);

        // PlayerControllerを取得
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }
    }

    private void Update()
    {
        // 元の加速処理を維持
        if (playerController != null)
        {
            speed += playerController.forwardSpeed;
        }

        // 元の移動処理を維持
        transform.position += moveDirection * speed * Time.deltaTime;

        // ターゲットがいない場合だけ再検索
        if (target == null)
        {
            target = FindClosestEnemy();
        }

        if (target != null)
        {
            Vector3 offset = target.transform.position - transform.position;
            float distanceSqr = offset.sqrMagnitude;

            if (distanceSqr > 0.000001f)
            {
                Vector3 directionToTarget = offset.normalized;

                // 検出範囲内なら即座に方向転換
                if (distanceSqr < detectionRadiusSqr)
                {
                    moveDirection = directionToTarget;
                }
                else
                {
                    moveDirection = Vector3.Slerp(
                        moveDirection,
                        directionToTarget,
                        homingStrength * 2f * Time.fixedDeltaTime
                    ).normalized;
                }
            }
        }

        // 元のRigidbody速度更新を維持
        rb.velocity = moveDirection * speed;

        Debug.DrawLine(
            transform.position,
            transform.position + moveDirection * 3f,
            Color.blue
        );
    }

    // 最も近い敵を検索
    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closestEnemy = null;
        float closestDistanceSqr = detectionRadiusSqr;

        Vector3 position = transform.position;

        foreach (GameObject enemy in enemies)
        {
            Vector3 offset = enemy.transform.position - position;
            float distanceSqr = offset.sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestEnemy = enemy;
                closestDistanceSqr = distanceSqr;
            }
        }

        return closestEnemy;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyWoll"))
        {
            if (sphereCollider != null)
            {
                sphereCollider.enabled = false;
            }

            Destroy(gameObject);
        }
    }

    // 選択時のみ検出範囲を表示
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
