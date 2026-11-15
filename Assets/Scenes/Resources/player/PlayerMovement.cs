
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("レール")]
    public Transform DollyTrack;

    [Header("移動設定")]
    public float playerSpeed = 1f;
    public float maxSpeed = 20f;
    public float damping = 1f;
    public float forwardSpeed = 0f;

    [Header("機体の傾き")]
    public float maxPitchAngle = 35f;
    public float maxRollAngle = 35f;
    public float rotationSpeed = 5f;

    [Header("移動範囲")]
    public float limitX = 15f;
    public float limitY = 10f;
    public float limitZ = 15f;

    private Vector3 velocity;
    private Vector3 playerRotation;
    private Vector3 targetRotation;

    private HPScript hpScript;

    // カメラから参照する移動速度（レール基準）
    public Vector3 LocalVelocity => velocity;

    void Start()
    {
        GameObject hpObject = GameObject.Find("HPGauge");

        if (hpObject != null)
        {
            hpScript = hpObject.GetComponent<HPScript>();
        }
    }

    void Update()
    {
        if (hpScript == null || hpScript.Gauge < 1)
        {
            forwardSpeed = 0f;
            playerSpeed = 0f;
            velocity = Vector3.zero;
        }
    }

    public void HandleMoveAndRotation()
    {
        if (hpScript == null || hpScript.Gauge < 1)
            return;

        if (DollyTrack == null)
            return;

        HandleInput();
        UpdateMovement();
        UpdateRotation();
    }

    void HandleInput()
    {
        float inputX = Input.GetAxis("L_Stick_H");
        float inputY = Input.GetAxis("L_Stick_V");

        if (Input.GetKey(KeyCode.D)) inputX += 1f;
        if (Input.GetKey(KeyCode.A)) inputX -= 1f;
        if (Input.GetKey(KeyCode.W)) inputY += 1f;
        if (Input.GetKey(KeyCode.S)) inputY -= 1f;

        Vector3 input = new Vector3(inputX, inputY, 0f);

        if (input.magnitude > 1f)
        {
            input.Normalize();
        }

        // レール基準で加速
        velocity += input * playerSpeed * Time.deltaTime * 60f;

        // 最大速度制限
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // 徐々に減速
        velocity = Vector3.Lerp(
            velocity,
            Vector3.zero,
            damping * Time.deltaTime
        );
    }

    void UpdateMovement()
    {
        Vector3 localPosition =
            DollyTrack.InverseTransformPoint(transform.position);

        // ローカル座標で移動
        localPosition += velocity * Time.deltaTime;

        // 移動範囲制限
        localPosition.x = Mathf.Clamp(localPosition.x, -limitX, limitX);
        localPosition.y = Mathf.Clamp(localPosition.y, -limitY, limitY);
        localPosition.z = Mathf.Clamp(localPosition.z, -limitZ, limitZ);

        transform.position = DollyTrack.TransformPoint(localPosition);
    }

    void UpdateRotation()
    {
        // 上下移動によるピッチ
        float targetPitch =
            -velocity.y / maxSpeed * maxPitchAngle;

        // 左右移動によるロール
        float targetRoll =
            -velocity.x / maxSpeed * maxRollAngle;

        targetRotation.x = Mathf.Clamp(
            targetPitch, -maxPitchAngle, maxPitchAngle);

        targetRotation.z = Mathf.Clamp(
            targetRoll, -maxRollAngle, maxRollAngle);

        playerRotation = Vector3.Lerp(
            playerRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        Quaternion localTilt = Quaternion.Euler(
            playerRotation.x,
            playerRotation.y,
            playerRotation.z
        );

        transform.rotation = DollyTrack.rotation * localTilt;
    }

    public void StopMovement()
    {
        velocity = Vector3.zero;
    }

    public void RotationReset()
    {
        playerRotation = Vector3.zero;
        targetRotation = Vector3.zero;
    }
}
