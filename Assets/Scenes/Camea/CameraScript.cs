using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;

    Vector3 defaultOffset = new Vector3(0, 0, -4);
    Vector3 newOffset = new Vector3(-4, 3, 10);

    float smoothSpeed = 5.0f;
    float maxTiltAngle = 2.0f;
   // float tiltSpeed = 5.0f;

    private Vector3 lastPlayerPosition;
    private float tiltAmount = 0f;
    private float tiltVelocity = 0f;

    float forwardTriggerZ = 3300f;

    //private bool hasSwitched = false;

    // 演出関連
    private bool isStarting = true;
    private float startDuration = 5.0f;
    private float startTimer = 0f;
    private Vector3 startOffset = new Vector3(-4, -3, 10); // スタート演出時のカメラ位置

    // 自身のカメラコンポーネント（必要なら）
    private Camera cam;

    void Start()
    {
        if (player != null)
        {
            lastPlayerPosition = player.position;
            transform.position = player.position + startOffset;

            Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = lookRotation;
        }

        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (player == null) return;

        Vector3 currentOffset = player.position.z > forwardTriggerZ ? newOffset : defaultOffset;

        if (isStarting)
        {
            startTimer += Time.deltaTime;
            float t = Mathf.Clamp01(startTimer / startDuration);

            Vector3 desiredStartPos = Vector3.Lerp(player.position + startOffset, player.position + currentOffset, t);
            transform.position = desiredStartPos;

            Quaternion targetRot = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * smoothSpeed);

            if (t >= 1.0f)
            {
                isStarting = false;
            }

            return;
        }

        Vector3 desiredPosition = player.position + currentOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        float speedX = (player.position.x - lastPlayerPosition.x) / Time.deltaTime;
        float targetTilt = Mathf.Clamp((speedX / 10f) * maxTiltAngle, -maxTiltAngle, maxTiltAngle);
        tiltAmount = Mathf.SmoothDamp(tiltAmount, targetTilt, ref tiltVelocity, 0.2f);

        Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation * Quaternion.Euler(0, 0, -tiltAmount), Time.deltaTime * smoothSpeed);

        lastPlayerPosition = player.position;
    }

    // ここから追加部分
    public void TakeDamage()
    {
        Debug.Log("ダメージを受けた！");

        // カメラシェイクを呼ぶ（このスクリプト自身に揺れ処理があればそちらを使う想定）
        TriggerCameraShake(0.1f, 0.5f);
    }

    // カメラシェイクの例（簡易実装）
    public void TriggerCameraShake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private System.Collections.IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
