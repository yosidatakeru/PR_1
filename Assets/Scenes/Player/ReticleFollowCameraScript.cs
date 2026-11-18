
using UnityEngine;

public class ReticleFollowCameraScript : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Camera mainCamera;

    [Header("Reticle Position")]
    public float distanceAhead = 20f;

    [Header("Follow Settings")]
    public float followSpeed = 10f;

    [Header("Screen Offset")]
    public Vector2 offset = Vector2.zero;

    [Header("Screen Bounds")]
    [Range(0f, 0.5f)]
    public float minX = 0.05f;

    [Range(0.5f, 1f)]
    public float maxX = 0.95f;

    [Range(0f, 0.5f)]
    public float minY = 0.05f;

    [Range(0.5f, 1f)]
    public float maxY = 0.95f;

    private Vector2 reticleViewport;


    [Header("Forward Reticle")]
    public float forwardOffset = 5f;


    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (player == null || mainCamera == null)
        {
            Debug.LogError("Player または Main Camera が未設定です。");
            enabled = false;
            return;
        }
    }

    void LateUpdate()
    {
        UpdateReticlePosition();
        UpdateWorldPosition();
        FaceCamera();
    }


    void UpdateReticlePosition()
    {
        // プレイヤーの進行方向を取得
        Vector3 forwardPosition =
            player.position +
            player.forward * forwardOffset;

        // 進行方向の先を画面座標に変換
        Vector3 screenPosition =
            mainCamera.WorldToViewportPoint(forwardPosition);

        if (screenPosition.z <= 0f)
            return;

        Vector2 targetViewport = new Vector2(
            screenPosition.x + offset.x,
            screenPosition.y + offset.y
        );

        // 画面内に制限
        targetViewport.x = Mathf.Clamp(
            targetViewport.x, minX, maxX
        );

        targetViewport.y = Mathf.Clamp(
            targetViewport.y, minY, maxY
        );

        // 滑らかに追従
        reticleViewport = Vector2.Lerp(
            reticleViewport,
            targetViewport,
            followSpeed * Time.deltaTime
        );
    }

    // 画面上の位置を3D空間に変換
    void UpdateWorldPosition()
    {
        Vector3 planePoint =
            player.position +
            mainCamera.transform.forward * distanceAhead;

        Plane targetPlane = new Plane(
            mainCamera.transform.forward,
            planePoint
        );

        Ray ray = mainCamera.ViewportPointToRay(
            new Vector3(
                reticleViewport.x,
                reticleViewport.y,
                0f
            )
        );

        if (targetPlane.Raycast(ray, out float enter)
            && enter >= 0f)
        {
            Vector3 targetPosition = ray.GetPoint(enter);

            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                followSpeed * Time.deltaTime
            );
        }
    }

    // カメラに正面を向ける
    void FaceCamera()
    {
        transform.rotation = Quaternion.LookRotation(
            -mainCamera.transform.forward,
            mainCamera.transform.up
        );
    }

    // 射撃・ロックオン用
    public Ray GetReticleRay()
    {
        return mainCamera.ViewportPointToRay(
            new Vector3(
                reticleViewport.x,
                reticleViewport.y,
                0f
            )
        );
    }
}
