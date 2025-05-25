using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSample : MonoBehaviour
{
    public Transform player;

    Vector3 defaultOffset = new Vector3(0, 0, -6); // 通常時のオフセット
    Vector3 newOffset = new Vector3(-4, 3, 10);    // 切り替え後のオフセット

    float smoothSpeed = 5.0f;
    float maxTiltAngle = 2.0f;
    float tiltSpeed = 5.0f;

    private Vector3 lastPlayerPosition;
    private float tiltAmount = 0f;
    private float tiltVelocity = 0f;

    float forwardTriggerZ = 3000f;

    private bool hasSwitched = false;

    void Start()
    {
        if (player != null)
        {
            lastPlayerPosition = player.position;
        }
    }

    void Update()
    {
        if (player == null) return;

        // プレイヤーのZ座標によって追尾方法を変更
        Vector3 currentOffset = player.position.z > forwardTriggerZ ? newOffset : defaultOffset;

        // 追尾処理
        Vector3 desiredPosition = player.position + currentOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 傾き計算
        float speedX = (player.position.x - lastPlayerPosition.x) / Time.deltaTime;
        float targetTilt = Mathf.Clamp((speedX / 10f) * maxTiltAngle, -maxTiltAngle, maxTiltAngle);
        tiltAmount = Mathf.SmoothDamp(tiltAmount, targetTilt, ref tiltVelocity, 0.2f);

        // 回転処理
        Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation * Quaternion.Euler(0, 0, -tiltAmount), Time.deltaTime * smoothSpeed);

        lastPlayerPosition = player.position;
    }
}


