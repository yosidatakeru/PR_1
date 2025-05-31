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

    private Vector3 lastPlayerPosition;
    private float tiltAmount = 0f;
    private float tiltVelocity = 0f;

    float forwardTriggerZ = 3300f;

    // 演出関連
    private bool isStarting = true;
    private float startDuration = 5.0f;
    private float startTimer = 0f;
    private Vector3 startOffset = new Vector3(-4, -3, 10); // スタート演出時のカメラ位置

    private Camera cam;

    PlayerScript playerScript;

    public float normalFOV = 60f;
    public float boostFOV = 80f;
    public float fovLerpSpeed = 5f;


    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        if (player != null)
        {
            lastPlayerPosition = player.position;
            transform.position = player.position + startOffset;

            Vector3 initialDir = player.position - transform.position;
            if (initialDir.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(initialDir);
            }
        }

        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (cam != null)
        {
            float targetFOV = playerScript.acceleration ? boostFOV : normalFOV;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);
        }

        // プレイヤーが無い or ポーズ中なら何もしない
        if (player == null || Time.timeScale == 0f) return;

        Vector3 currentOffset = player.position.z > forwardTriggerZ ? newOffset : defaultOffset;

        // スタート演出中
        if (isStarting)
        {
            startTimer += Time.deltaTime;
            float t = Mathf.Clamp01(startTimer / startDuration);

            Vector3 desiredStartPos = Vector3.Lerp(player.position + startOffset, player.position + currentOffset, t);
            transform.position = desiredStartPos;

            Vector3 dir = player.position - transform.position;
            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * smoothSpeed);
            }

            if (t >= 1.0f)
            {
                isStarting = false;
            }

            return;
        }

        // 通常追従
        Vector3 desiredPosition = player.position + currentOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 横移動による傾き処理
        float speedX = (player.position.x - lastPlayerPosition.x) / Time.deltaTime;
        float targetTilt = Mathf.Clamp((speedX / 10f) * maxTiltAngle, -maxTiltAngle, maxTiltAngle);
        tiltAmount = Mathf.SmoothDamp(tiltAmount, targetTilt, ref tiltVelocity, 0.2f);

        // 回転（LookRotation）
        Vector3 lookDir = player.position - transform.position;
        if (lookDir.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation * Quaternion.Euler(0, 0, -tiltAmount), Time.deltaTime * smoothSpeed);
        }

        lastPlayerPosition = player.position;
    }

    // ダメージ時に呼び出し
    public void TakeDamage()
    {
        Debug.Log("ダメージを受けた！");
        TriggerCameraShake(0.1f, 0.5f);
    }

    // カメラシェイクのトリガー
    public void TriggerCameraShake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    // カメラシェイクのコルーチン
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
