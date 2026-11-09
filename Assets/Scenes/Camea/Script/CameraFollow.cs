
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

    [Header("画面移動制限")]
    [Range(0f, 0.4f)]
    public float marginX = 0.03f;

    [Range(0f, 0.4f)]
    public float marginY = 0.03f;

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

        Quaternion railRotation = Quaternion.LookRotation(
            currentForward,
            currentUp
        );

        Vector3 railRight = railRotation * Vector3.right;
        Vector3 railUp = railRotation * Vector3.up;

        Vector3 playerOffset = player.position - rail.position;

        float x = Vector3.Dot(playerOffset, railRight);
        float y = Vector3.Dot(playerOffset, railUp);

        // カメラ位置を計算
        Vector3 targetPosition =
            player.position
            + railRight * offset.x
            + railUp * offset.y
            - currentForward * Mathf.Abs(offset.z);

        targetPosition +=
            railRight * (x * (followAmount - 1f));

        targetPosition +=
            railUp * (y * (followAmount - 1f));

        float positionLerp = 1f -
            Mathf.Exp(-positionSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            positionLerp
        );

        // カメラの向きを計算
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

        // プレイヤーの画面上の位置を取得
        Camera cam = GetComponent<Camera>();

        if (cam == null) return;

        Vector3 viewport =
            cam.WorldToViewportPoint(player.position);

        // 画面端に余白を設定
        float minViewportX = marginX;
        float maxViewportX = 1f - marginX;

        float minViewportY = marginY;
        float maxViewportY = 1f - marginY;

        // 画面外に出ないように制限
        Vector3 clampedViewport = viewport;

        clampedViewport.x = Mathf.Clamp(
            viewport.x,
            minViewportX,
            maxViewportX
        );

        clampedViewport.y = Mathf.Clamp(
            viewport.y,
            minViewportY,
            maxViewportY
        );

        // 画面座標からワールド座標へ変換
        Vector3 clampedWorld =
            cam.ViewportToWorldPoint(clampedViewport);

        // レール方向の左右上下だけ補正
        Vector3 correction =
            clampedWorld - player.position;

        float correctionX =
            Vector3.Dot(correction, railRight);

        float correctionY =
            Vector3.Dot(correction, railUp);

        player.position +=
            railRight * correctionX +
            railUp * correctionY;
    }
}
