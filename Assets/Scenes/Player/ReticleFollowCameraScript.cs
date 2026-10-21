
using UnityEngine;

public class ReticleFollowCameraScript : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Camera mainCamera;

    [Header("Reticle Movement")]
    public float distanceAhead = 20f;
    public float stickSpeed = 0.5f;
    public float arrowSpeed = 0.15f;

    [Header("Screen Bounds")]
    [Range(0f, 0.5f)]
    public float minX = 0.1f;

    [Range(0.5f, 1f)]
    public float maxX = 0.9f;

    [Range(0f, 0.5f)]
    public float minY = 0.1f;

    [Range(0.5f, 1f)]
    public float maxY = 0.9f;

    [Header("Auto Follow")]
    public float followSpeed = 8f;

    // true: 自動追従 / false: 手動操作
    private bool autoFollow = true;

    // 画面上の照準位置（0～1）
    private Vector2 reticleViewport =
        new Vector2(0.5f, 0.5f);

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (player == null || mainCamera == null)
        {
            Debug.LogError(
                "Player と Main Camera を設定してください。"
            );

            enabled = false;
            return;
        }

        // 現在の照準位置を画面座標に変換
        Vector3 screenPos =
            mainCamera.WorldToViewportPoint(transform.position);

        if (screenPos.z > 0f)
        {
            reticleViewport = new Vector2(
                screenPos.x,
                screenPos.y
            );
        }

        ClampViewport();
    }

    void Update()
    {
        // Fキーで操作モードを切り替え
        if (Input.GetKeyDown(KeyCode.F)|| Input.GetKeyDown(KeyCode.JoystickButton11))
        {
            autoFollow = !autoFollow;

            // 手動操作へ切り替えたときに
            // 現在の画面位置を引き継ぐ
            Vector3 screenPos =
                mainCamera.WorldToViewportPoint(transform.position);

            if (screenPos.z > 0f)
            {
                reticleViewport = new Vector2(
                    screenPos.x,
                    screenPos.y
                );

                ClampViewport();
            }
        }

        if (!autoFollow)
        {
            MoveReticle();
        }
        else
        {
            // 自動追従中は画面中央を目指す
            reticleViewport = Vector2.Lerp(
                reticleViewport,
                new Vector2(0.5f, 0.5f),
                followSpeed * Time.deltaTime
            );
        }

        ClampViewport();
        UpdateWorldPosition();
        FaceCamera();
    }

    void MoveReticle()
    {
        float moveX = Input.GetAxis("R_Stick_H");
        float moveY = Input.GetAxis("R_Stick_V");

        // 十字キー入力
        bool usingArrow =
            Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow);

        if (Input.GetKey(KeyCode.RightArrow))
            moveX += 1f;

        if (Input.GetKey(KeyCode.LeftArrow))
            moveX -= 1f;

        if (Input.GetKey(KeyCode.UpArrow))
            moveY += 1f;

        if (Input.GetKey(KeyCode.DownArrow))
            moveY -= 1f;

        Vector2 input = Vector2.ClampMagnitude(
            new Vector2(moveX, moveY), 1f
        );

        float speed = usingArrow
            ? arrowSpeed
            : stickSpeed;

        reticleViewport += input * speed * Time.deltaTime;
    }

    void ClampViewport()
    {
        reticleViewport.x = Mathf.Clamp(
            reticleViewport.x, minX, maxX
        );

        reticleViewport.y = Mathf.Clamp(
            reticleViewport.y, minY, maxY
        );
    }

    void UpdateWorldPosition()
    {
        // 照準が存在する平面をプレイヤー前方に作る
        Vector3 planePoint =
            player.position +
            mainCamera.transform.forward * distanceAhead;

        Plane targetPlane = new Plane(
            mainCamera.transform.forward,
            planePoint
        );

        // 画面上の照準位置から3D空間へのRayを作成
        Ray ray = mainCamera.ViewportPointToRay(
            new Vector3(
                reticleViewport.x,
                reticleViewport.y,
                0f
            )
        );

        // Rayと平面の交点に照準を配置
        if (targetPlane.Raycast(ray, out float enter)
            && enter >= 0f)
        {
            transform.position = ray.GetPoint(enter);
        }
    }

    void FaceCamera()
    {
        // 照準の表面をカメラに向ける
        transform.rotation = Quaternion.LookRotation(
            -mainCamera.transform.forward,
            mainCamera.transform.up
        );
    }
}
