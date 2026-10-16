using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ターゲット（プレイヤー等）を追従し、移動速度に応じたカメラの傾き（Z軸回転）や
/// 特定のZ座標到達によるオフセット切替を制御するカメラ制御クラス
/// </summary>
public class CameraScript : MonoBehaviour
{
    // 追従対象のTransform
    public Transform target;

    // 通常時のカメラオフセット（ターゲットのローカル座標系）
    Vector3 defaultOffset = new Vector3(0, 0, -4);

    // ブースト（特定エリア到達）時のカメラオフセット
    Vector3 boostOffset = new Vector3(-4, 3, 10);

    // カメラの移動・回転の補間スピード
    float smoothSpeed = 5f;

    // ブーストオフセットに切り替えるZ座標の閾値
    float forwardTriggerZ = 3300f;

    // 回転補間速度（※現在は smoothSpeed が使用されています）
    public float rotationSmoothSpeed = 5f;

    // カメラの最大傾き角度（度）
    public float maxTiltAngle = 5.0f;

    // 1フレーム前のターゲット位置（X軸方向の速度計算用）
    private Vector3 lastPosition;

    // 現在の傾き量（Z軸の回転角度）
    private float tiltAmount = 0f;

    // SmoothDamp制御用の速度参照変数
    private float tiltVelocity = 0f;

    // HP制御用のスクリプト参照
    private HPScript hpScript;

    void Start()
    {
        // ターゲットが設定されている場合、初期位置を記録
        if (target != null)
        {
            lastPosition = target.position;
        }

        // HPGaugeオブジェクトからHPScriptを取得
        hpScript = GameObject.Find("HPGauge").GetComponent<HPScript>();
    }

    void LateUpdate()
    {
        // HPが0以下の場合、カメラ追従処理を停止する
        if (hpScript.Gauge <= 0)
        {
            enabled = false;
        }

        // ターゲットが未設定の場合は処理を行わない
        if (target == null)
        {
            return;
        }

        // デルタタイムが非常に小さすぎる（ポーズ中など）場合は処理を行わない
        if (Time.deltaTime <= 0.00001f)
        {
            return;
        }

        // ターゲットのZ座標に応じて追従オフセットを切り替え
        Vector3 currentOffset = target.position.z > forwardTriggerZ ? boostOffset : defaultOffset;
        
        Vector3 rearDirection = -target.forward; // ターゲットの後ろの方向
        rearDirection.y = 0; // 水平方向のみ考慮
        rearDirection.Normalize();

        Vector3 desiredPosition;
        if (target.position.z > forwardTriggerZ)
        {
            // ブーストオフセット（さらに後ろへ）
            desiredPosition = target.position + rearDirection * Mathf.Abs(currentOffset.z) + target.up * currentOffset.y + target.right * currentOffset.x;
        }
        else
        {
            // 通常オフセット（後ろへ）
            desiredPosition = target.position + rearDirection * Mathf.Abs(currentOffset.z) + target.up * currentOffset.y + target.right * currentOffset.x;
        }
        // --- 修正箇所：終了 ---

        // カメラの位置を目標位置に向けて滑らかに移動 (Lerp)
        transform.position =
            Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // X軸方向の移動速度を算出
        float speedX =
            (target.position.x - lastPosition.x)
            / Time.deltaTime;

        // 非数値 (NaN) や無限大 (Infinity) の例外を防ぐガード処理
        if (float.IsNaN(speedX) ||
            float.IsInfinity(speedX))
        {
            speedX = 0f;
        }

        // 速度に応じた目標傾き角度の計算と制限（-maxTiltAngle ～ maxTiltAngle）
        float targetTilt =
            (speedX / 10f) * maxTiltAngle;

        targetTilt = Mathf.Clamp(targetTilt, -maxTiltAngle, maxTiltAngle);

        // 傾きを滑らかに変化させる (SmoothDamp)
        tiltAmount =
            Mathf.SmoothDamp(
                tiltAmount,
                targetTilt,
                ref tiltVelocity,
                0.2f
            );

        // 傾き量の例外値ガード処理
        if (float.IsNaN(tiltAmount) ||
            float.IsInfinity(tiltAmount))
        {
            tiltAmount = 0f;
            tiltVelocity = 0f;
        }

        // カメラからターゲットへの方向ベクトルを算出
        Vector3 dir =
            target.position - transform.position;

        // ベクトルの要素に非数値・無限大が含まれている場合は処理を中断
        if (
            float.IsNaN(dir.x) ||
            float.IsNaN(dir.y) ||
            float.IsNaN(dir.z) ||
            float.IsInfinity(dir.x) ||
            float.IsInfinity(dir.y) ||
            float.IsInfinity(dir.z)
        )
        {
            lastPosition = target.position;
            return;
        }

        // 方向ベクトルの長さが十分にある場合のみ回転処理を実行
        if (dir.sqrMagnitude > 0.001f)
        {
            // ターゲットを注視する回転を作成
            Quaternion rot =
                Quaternion.LookRotation(dir);

            // Z軸周りの傾き回転を作成
            Quaternion tiltRotation =
                Quaternion.Euler(
                    0f,
                    0f,
                    -tiltAmount
                );

            // 注視回転と傾き回転を結合
            Quaternion targetRotation =
                rot * tiltRotation;

            // カメラの回転を目標回転に向けて滑らかに補間 (Slerp)
            transform.rotation =
                Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    Time.deltaTime * smoothSpeed
                );
        }

        // 次フレームの速度計算のために現在のターゲット位置を保存
        lastPosition = target.position;
    }
}