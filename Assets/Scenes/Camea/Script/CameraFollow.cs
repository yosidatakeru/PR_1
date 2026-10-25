using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 追従するプレイヤー
    public float smoothSpeed = 10f;
    public Vector3 offset = new Vector3(0, 0, -5);
    public Transform DollyTrack; // 基準となるオブジェクト（任意）

    [Header("移動範囲の制限 (Min / Max)")]
    public bool useBounds = true; // 制限を有効にするかどうか
    public Vector2 minBounds = new Vector2(-10f, -10f); // X最小, Y最小
    public Vector2 maxBounds = new Vector2(100f, 200f);  // X最大, Y最大
    public float minZ = -50f; // Zの最小値
    public float maxZ = 50f;  // Zの最大値

    private Camera cam;
    private float defaultFOV;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam != null) defaultFOV = cam.fieldOfView;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 滑らかに追従する目標位置を計算
        Vector3 desiredPosition = target.position + offset;

        // 範囲制限が有効な場合、座標をクランプ（制限）する
        if (useBounds)
        {
            // DollyTrackが設定されている場合はその位置を基準にする
            Vector3 basePosition = (DollyTrack != null) ? DollyTrack.position : Vector3.zero;

            desiredPosition.x = Mathf.Clamp(desiredPosition.x, basePosition.x + minBounds.x, basePosition.x + maxBounds.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, basePosition.y + minBounds.y, basePosition.y + maxBounds.y);
            desiredPosition.z = Mathf.Clamp(desiredPosition.z, basePosition.z + minZ, basePosition.z + maxZ);
        }

        // 実際のカメラ位置を滑らかに更新
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    // ブースト・ブレーキ時のズーム処理の例
    public void AdjustZoom(float zoomAmount)
    {
        if (cam != null)
        {
            float targetFOV = defaultFOV + zoomAmount;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * 5f);
        }
    }
}