
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("追従対象")]
    public Transform target;
    public Transform DollyTrack;
    public PlayerMovement playerMovement;

    [Header("カメラ基本位置")]
    public Vector3 offset = new Vector3(0f, 4f, -15f);

    [Header("カメラ移動")]
    public float followSpeed = 5f;
    public float smoothTime = 0.3f;

    [Header("画面内の追従範囲")]
    public float limitX = 8f;
    public float limitY = 5f;

    [Header("追従量")]
    [Range(0f, 1f)]
    public float followX = 0.5f;

    [Range(0f, 1f)]
    public float followY = 0.5f;

    [Header("カメラの角度変化")]
    public float maxPitch = 4f;
    public float maxYaw = 5f;
    public float maxRoll = 5f;

    [Header("角度変化の速度")]
    public float rotationSpeed = 3f;

    private Vector3 positionVelocity;

    void Start()
    {
        if (DollyTrack == null)
            return;

        transform.position = DollyTrack.TransformPoint(offset);
        transform.rotation = DollyTrack.rotation;
    }

    void LateUpdate()
    {
        if (target == null || DollyTrack == null)
            return;

        // プレイヤーの位置をレール基準に変換
        Vector3 localPlayerPosition =
            DollyTrack.InverseTransformPoint(target.position);

        // --------------------------------
        // 1. カメラの位置を計算
        // --------------------------------

        float followOffsetX = 0f;
        float followOffsetY = 0f;

        // 左右の追従判定
        if (localPlayerPosition.x > limitX)
        {
            followOffsetX =
                (localPlayerPosition.x - limitX) * followX;
        }
        else if (localPlayerPosition.x < -limitX)
        {
            followOffsetX =
                (localPlayerPosition.x + limitX) * followX;
        }

        // 上下の追従判定
        if (localPlayerPosition.y > limitY)
        {
            followOffsetY =
                (localPlayerPosition.y - limitY) * followY;
        }
        else if (localPlayerPosition.y < -limitY)
        {
            followOffsetY =
                (localPlayerPosition.y + limitY) * followY;
        }

        // レール基準のカメラ位置
        Vector3 localCameraPosition =
            offset + new Vector3(
                followOffsetX,
                followOffsetY,
                0f
            );

        Vector3 targetPosition =
            DollyTrack.TransformPoint(localCameraPosition);

        // 滑らかに追従
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref positionVelocity,
            smoothTime
        );

        // --------------------------------
        // 2. プレイヤーの移動速度を取得
        // --------------------------------

        Vector3 localVelocity = Vector3.zero;

        if (playerMovement != null)
        {
            localVelocity = playerMovement.LocalVelocity;
        }

        float normalizedX =
            Mathf.Clamp(localPlayerPosition.x / limitX, -1f, 1f);

        float normalizedY =
            Mathf.Clamp(localPlayerPosition.y / limitY, -1f, 1f);

        float normalizedVelocityX = Mathf.Clamp(
            localVelocity.x / 20f, -1f, 1f
        );

        // --------------------------------
        // 3. カメラの角度を計算
        // --------------------------------

        // 上下移動によるカメラのピッチ
        float pitch = -normalizedY * maxPitch;

        // 左右位置によるカメラの向き
        float yaw = normalizedX * maxYaw;

        // 左右移動によるカメラのロール
        float roll = -normalizedVelocityX * maxRoll;

        // レールの回転を基本に角度を追加
        Quaternion targetRotation =
            DollyTrack.rotation *
            Quaternion.Euler(pitch, yaw, roll);

        // 滑らかに回転
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
