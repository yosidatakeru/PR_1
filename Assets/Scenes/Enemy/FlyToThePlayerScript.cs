using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToThePlayerScript : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 targetPosition;
    Vector3 direction;
    void Start()
    {
        // プレイヤーを取得
           GameObject player = GameObject.FindGameObjectWithTag("Player");
       
            Rigidbody rb = player.GetComponent<Rigidbody>();
            Vector3 playerVelocity = rb != null ? rb.velocity : Vector3.zero;

            // 未来の位置を計算（1.5秒後の位置）
            float predictionTime = 1.5f;
            targetPosition = player.transform.position + playerVelocity * predictionTime;

            direction = (targetPosition - transform.position).normalized;
        

    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        Destroy(gameObject,7);
    }
}
