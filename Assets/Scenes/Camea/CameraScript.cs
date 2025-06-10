using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //プレイヤーのTransformへの参照
    public Transform player;
    // カメラの基本オフセット
    //通常時
    Vector3 defaultOffset = new Vector3(0, 0, -4);
    //加速時
    Vector3 newOffset = new Vector3(-4, 3, 10);
    // カメラ追従のスムーズさ
    float smoothSpeed = 5.0f;
    // 横方向移動に対する傾きの最大角度
    float maxTiltAngle = 2.0f;

    // 前フレームのプレイヤー位置
    private Vector3 lastPlayerPosition;
    // 現在の傾き量
    private float tiltAmount = 0f;
    // 傾きの補間用変数
    private float tiltVelocity = 0f;

    // このZ位置を超えるとnewOffsetに切り替える
    float forwardTriggerZ = 3300f;

    // 演出関連
    //演出開始
    bool isStarting = true;
    float startDuration = 5.0f;// スタート演出の長さ（秒）
    float startTimer = 0f;
    private Vector3 startOffset = new Vector3(-4, -3, 10); // スタート演出時のカメラ位置

    private Camera camera;

    PlayerScript playerScript;


    // 通常時と加速時のFOV（視野角）切り替え
    // 通常時のカメラ視野角
    float normalFOV = 60f;
    // 加速時のカメラ視野角
    float boostFOV = 80f;
    // FOVを補間する速度（
    float fovLerpSpeed = 5f;


    void Start()
    {   
        // プレイヤースクリプトの取得
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
       
        
        if (player != null)
        {
            lastPlayerPosition = player.position;
            
            // 初期位置はスタート演出の位置
            transform.position = player.position + startOffset;

            // カメラの初期回転を設定
            Vector3 initialDir = player.position - transform.position;
            if (initialDir.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(initialDir);
            }
        }

        camera = GetComponent<Camera>();
    }



    void Update()
    {    // カメラのFOV（視野角）をプレイヤーの加速状態に応じて変更
        if (camera != null)
        {
            float targetFOV = playerScript.acceleration ? boostFOV : normalFOV;
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);
        }



        // プレイヤーが無い or ポーズ中なら何もしない
        if (player == null || Time.timeScale == 0f) 
        {
            return;
        }

        
        Vector3 currentOffset = player.position.z > forwardTriggerZ ? newOffset : defaultOffset;


        // スタート演出中
        if (isStarting)
        {
            startTimer += Time.deltaTime;
            float t = Mathf.Clamp01(startTimer / startDuration);

            // カメラ位置をスタート演出位置から通常追従位置へ補間
            Vector3 desiredStartPos = Vector3.Lerp(player.position + startOffset, player.position + currentOffset, t);
            transform.position = desiredStartPos;

            // 回転も補間
            Vector3 dir = player.position - transform.position;
            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * smoothSpeed);
            }

            // 演出が終了したらフラグをオフに
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
        // プレイヤー位置の更新
        lastPlayerPosition = player.position;
    }


    // === ダメージ関連 ===

    // ダメージ時に呼び出し
    public void TakeDamage()
    {
        Debug.Log("ダメージを受けた！");
        // 振動時間0.1秒、強さ0.5
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
        // 元の位置に戻す
        transform.localPosition = originalPos;
    }
}
