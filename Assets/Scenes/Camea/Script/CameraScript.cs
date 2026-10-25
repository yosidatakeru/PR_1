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
    private Vector3 defaultOffset = new Vector3(0, 0, -400);

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
        ////if (target != null)
        ////{
        ////    lastPosition = target.position;
        ////}

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

      
    }
}