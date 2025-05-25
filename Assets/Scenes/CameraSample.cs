using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSample : MonoBehaviour
{
    public Transform player;

    Vector3 defaultOffset = new Vector3(0, 0, -6);
    Vector3 newOffset = new Vector3(-4, 3, 10);

    float smoothSpeed = 5.0f;
    float maxTiltAngle = 2.0f;
    float tiltSpeed = 5.0f;

    private Vector3 lastPlayerPosition;
    private float tiltAmount = 0f;
    private float tiltVelocity = 0f;

    float forwardTriggerZ = 3300f;

    private bool hasSwitched = false;

    // 演出関連
    private bool isStarting = true;
    private float startDuration = 5.0f;
    private float startTimer = 0f;
    private Vector3 startOffset = new Vector3(-4, -3, 10); // スタート演出時のカメラ位置

    void Start()
    {
        if (player != null)
        {
            lastPlayerPosition = player.position;
            transform.position = player.position + startOffset;

            // プレイヤーの方向を見る
            Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = lookRotation;
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector3 currentOffset = player.position.z > forwardTriggerZ ? newOffset : defaultOffset;

        // スタート演出中
        if (isStarting)
        {
            startTimer += Time.deltaTime;
            float t = Mathf.Clamp01(startTimer / startDuration);

            // プレイヤーに向かってカメラが近づいていく
            Vector3 desiredStartPos = Vector3.Lerp(player.position + startOffset, player.position + currentOffset, t);
            transform.position = desiredStartPos;

            // 回転も補間してスムーズに
            Quaternion targetRot = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * smoothSpeed);

            if (t >= 1.0f)
            {
                isStarting = false; // 演出終了
            }

            return; // 通常追尾はまだ行わない
        }

        // 通常の追尾処理
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


