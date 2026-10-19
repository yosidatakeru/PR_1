using UnityEngine;

/// <summary>
/// ターゲット（プレイヤー等）を追従し、移動速度に応じたカメラの傾き（Z軸回転）や
/// 特定のZ座標到達によるオフセット切替を制御するカメラ制御クラス
/// </summary>
public class CameraScript : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("追従対象のTransform")]
    public Transform target;

    [Tooltip("HPゲージ制御スクリプト（未設定の場合は自動検索）")]
    [SerializeField] private HPScript hpScript;

    [Header("Offset Settings")]
    // 通常時のカメラオフセット（ターゲットのローカル座標系）
    private Vector3 defaultOffset = new Vector3(0, 0, -4);

    // ブースト（特定エリア到達）時のカメラオフセット
    private Vector3 boostOffset = new Vector3(-4, 3, 10);

    // ブーストオフセットに切り替えるZ座標の閾値
    private float forwardTriggerZ = 3300f;

    [Header("Movement & Rotation Settings")]
    // カメラの移動・回転の補間スピード
    public float smoothSpeed = 5f;

    // カメラの最大傾き角度（度）
    public float maxTiltAngle = 5.0f;

    // 1フレーム前のターゲット位置（X軸方向の速度計算用）
    private Vector3 lastPosition;

    // 現在の傾き量（Z軸の回転角度）
    private float tiltAmount = 0f;

    // SmoothDamp制御用の速度参照変数
    private float tiltVelocity = 0f;

    void Start()
    {
        // ターゲットが設定されている場合、初期位置を記録
        if (target != null)
        {
            lastPosition = target.position;
        }

        // HPScriptがインスペクターで未設定の場合のみ検索
        if (hpScript == null)
        {
            GameObject hpObj = GameObject.Find("HPGauge");
            if (hpObj != null)
            {
                hpScript = hpObj.GetComponent<HPScript>();
            }
            else
            {
                Debug.LogWarning("HPGauge オブジェクトが見つかりません。", this);
            }
        }
    }

    void LateUpdate()
    {
        // HPチェック（参照が存在し、かつGaugeが0以下の場合処理を無効化）
        if (hpScript != null && hpScript.Gauge <= 0)
        {
            enabled = false;
            return;
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

        // --- 位置の補間計算 ---

        // ターゲットのZ座標に応じて追従オフセットを切り替え
        Vector3 currentOffset = target.position.z > forwardTriggerZ ? boostOffset : defaultOffset;

        // ターゲットの後ろの水平方向ベクトルを算出
        Vector3 rearDirection = -target.forward;
        rearDirection.y = 0;
        rearDirection.Normalize();

        // 目標位置の算出（if/elseの重複を削除）
        Vector3 desiredPosition = target.position
            + rearDirection * Mathf.Abs(currentOffset.z)
            + target.up * currentOffset.y
            + target.right * currentOffset.x;

        // カメラの位置を目標位置に向けて滑らかに移動 (Lerp)
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // --- X軸の速度計算と傾き処理 ---

        // X軸方向の移動速度を算出
        float speedX = (target.position.x - lastPosition.x) / Time.deltaTime;

        // 非数値 (NaN) や無限大 (Infinity) の例外を防ぐガード処理
        if (float.IsNaN(speedX) || float.IsInfinity(speedX))
        {
            speedX = 0f;
        }

        // 速度に応じた目標傾き角度の計算と制限（-maxTiltAngle ～ maxTiltAngle）
        float targetTilt = Mathf.Clamp((speedX / 10f) * maxTiltAngle, -maxTiltAngle, maxTiltAngle);

        // 傾きを滑らかに変化させる (SmoothDamp)
        tiltAmount = Mathf.SmoothDamp(tiltAmount, targetTilt, ref tiltVelocity, 0.2f);

        // 傾き量の例外値ガード処理
        if (float.IsNaN(tiltAmount) || float.IsInfinity(tiltAmount))
        {
            tiltAmount = 0f;
            tiltVelocity = 0f;
        }

        // --- 回転の補間計算 ---

        // カメラからターゲットへの方向ベクトルを算出
        Vector3 dir = target.position - transform.position;

        // ベクトルの要素に非数値・無限大が含まれている場合は処理を中断
        if (float.IsNaN(dir.x) || float.IsNaN(dir.y) || float.IsNaN(dir.z) ||
            float.IsInfinity(dir.x) || float.IsInfinity(dir.y) || float.IsInfinity(dir.z))
        {
            lastPosition = target.position;
            return;
        }

        // 方向ベクトルの長さが十分にある場合のみ回転処理を実行
        if (dir.sqrMagnitude > 0.001f)
        {
            // ターゲットを注視する回転を作成
            Quaternion rot = Quaternion.LookRotation(dir);

            // Z軸周りの傾き回転を作成
            Quaternion tiltRotation = Quaternion.Euler(0f, 0f, -tiltAmount);

            // 注視回転と傾き回転を結合
            Quaternion targetRotation = rot * tiltRotation;

            // カメラの回転を目標回転に向けて滑らかに補間 (Slerp)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);
        }

        // 次フレームの速度計算のために現在のターゲット位置を保存
        lastPosition = target.position;
    }
}