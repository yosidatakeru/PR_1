using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // プレイヤー
    public Transform DollyTrack;   // レール

    public float smoothSpeed = 10f;
    public float rotationSpeed = 5f;

    [Header("プレイヤーとの距離")]
    public Vector3 offset = new Vector3(0, 0, -10f);

    [Header("移動範囲の制限")]
    public bool useBounds = true;

    public Vector2 minBounds = new Vector2(-10f, -10f);
    public Vector2 maxBounds = new Vector2(10f, 10f);

    public float minZ = -50f;
    public float maxZ = 50f;

    private Camera cam;
    private float defaultFOV;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (cam != null)
        {
            defaultFOV = cam.fieldOfView;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // プレイヤーの後ろの位置を計算
        Vector3 desiredPosition =
            target.position + target.rotation * offset;

        // 移動範囲の制限
        if (useBounds && DollyTrack != null)
        {
            Vector3 basePosition = DollyTrack.position;

            desiredPosition.x = Mathf.Clamp(
                desiredPosition.x,
                basePosition.x + minBounds.x,
                basePosition.x + maxBounds.x
            );

            desiredPosition.y = Mathf.Clamp(
                desiredPosition.y,
                basePosition.y + minBounds.y,
                basePosition.y + maxBounds.y
            );

            desiredPosition.z = Mathf.Clamp(
                desiredPosition.z,
                basePosition.z + minZ,
                basePosition.z + maxZ
            );
        }

        // カメラ位置を追従
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // レールの進行方向を見る
        if (DollyTrack != null)
        {
            Vector3 railDirection = DollyTrack.forward;

            Quaternion targetRotation =
                Quaternion.LookRotation(railDirection, DollyTrack.up);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    public void AdjustZoom(float zoomAmount)
    {
        if (cam != null)
        {
            float targetFOV = defaultFOV + zoomAmount;

            cam.fieldOfView = Mathf.Lerp(
                cam.fieldOfView,
                targetFOV,
                Time.deltaTime * 5f
            );
        }
    }
}