
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("追従対象")]
    public Transform player;
    public Transform rail;

    [Header("カメラ位置")]
    public Vector3 offset = new Vector3(0f, 4f, -15f);

    [Header("追従速度")]
    public float positionSpeed = 5f;
    public float rotationSpeed = 3f;

    [Header("プレイヤー移動範囲")]
    public float minX = -20f;
    public float maxX = 20f;
    public float minY = -5f;
    public float maxY = 15f;

    [Header("カメラ追従量")]
    [Range(0f, 1f)]
    public float followAmount = 0.7f;

    [Header("カメラ注視位置")]
    public float lookAhead = 30f;

    [Header("カーブ対応")]
    public float curveLookSpeed = 4f;

    private Vector3 currentForward;
    private Vector3 currentUp;

    void Start()
    {
        if (rail != null)
        {
            currentForward = rail.forward;
            currentUp = rail.up;
        }
    }

    void LateUpdate()
    {
        if (player == null || rail == null) return;

        // レールの進行方向を滑らかに取得
        currentForward = Vector3.Slerp(
            currentForward,
            rail.forward,
            curveLookSpeed * Time.deltaTime
        ).normalized;

        currentUp = Vector3.Slerp(
            currentUp,
            rail.up,
            curveLookSpeed * Time.deltaTime
        ).normalized;

        // カメラの基準となる回転
        Quaternion railRotation = Quaternion.LookRotation(
            currentForward,
            currentUp
        );

        // レール基準の左右上下方向
        Vector3 railRight = railRotation * Vector3.right;
        Vector3 railUp = railRotation * Vector3.up;

        // プレイヤーの位置を取得
        Vector3 playerOffset = player.position - rail.position;

        float x = Vector3.Dot(playerOffset, railRight);
        float y = Vector3.Dot(playerOffset, railUp);

        // 移動範囲を制限
        x = Mathf.Clamp(x, minX, maxX);
        y = Mathf.Clamp(y, minY, maxY);

        // プレイヤーに追従するカメラ位置
        Vector3 targetPosition =
            player.position
            + railRight * offset.x
            + railUp * offset.y
            - currentForward * Mathf.Abs(offset.z);

        // 左右上下の追従量を調整
        targetPosition += railRight * (x * (followAmount - 1f));
        targetPosition += railUp * (y * (followAmount - 1f));

        // 滑らかに追従
        float positionLerp = 1f -
            Mathf.Exp(-positionSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            positionLerp
        );

        // プレイヤーより前方を注視
        Vector3 lookPosition =
            player.position + currentForward * lookAhead;

        Vector3 direction = lookPosition - transform.position;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(
                direction,
                currentUp
            );

            float rotationLerp = 1f -
                Mathf.Exp(-rotationSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationLerp
            );
        }
    }
}
