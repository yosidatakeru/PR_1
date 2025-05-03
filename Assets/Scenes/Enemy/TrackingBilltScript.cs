using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TrackingBilltScript : MonoBehaviour
{
    string targetTag = "Player";
    float speed = 30f;
    float rotateSpeed = 1000f;
    float lifeTime = 10f;
    float homingDelay = 0.5f; // 追尾開始までの遅延

    private Transform target;
    private bool isHoming = false;
    private float homingTimer = 0f;

    void Start()
    {
        GameObject targetObj = GameObject.FindWithTag(targetTag);
        if (targetObj != null)
        {
            target = targetObj.transform;
        }

        Destroy(gameObject, lifeTime);
    }


    void Update()
    {
        // ミサイルは常に前進
        transform.position += transform.forward * speed * Time.deltaTime;

        // 一定時間後に追尾開始
        if (!isHoming)
        {
            homingTimer += Time.deltaTime;
            if (homingTimer >= homingDelay)
            {
                isHoming = true;
            }
            return;
        }

        if (target == null) return;

        // ターゲット方向を算出
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 自然な追尾回転（RotateTowards）
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }

   
}




   
    

