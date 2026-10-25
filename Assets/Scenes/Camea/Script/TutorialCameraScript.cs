using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCameraScript : MonoBehaviour
{

    [Header("Target & Follow")]
    public Transform playerTarget;       // 追従するプレイヤー（自機）
    public float smoothSpeed = 0.1f;     // 追従の滑らかさ
    public Vector3 offset = new Vector3(0f, 1.5f, -10f); // カメラの基本オフセット

    [Header("Camera Bounds (Clamping)")]
    public float minPositionX = -4f;
    public float maxPositionX = 4f;
    public float minPositionY = -3f;
    public float maxPositionY = 3f;

    void FixedUpdate()
    {
        if (playerTarget == null) return;

        // 1. プレイヤーの位置を基準にした目標位置を計算
        Vector3 desiredPosition = playerTarget.position + offset;

        // 2. 現在の位置から目標位置へスムーズに補間
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // 3. カメラの移動範囲を制限（クランプ）しつつ適用
        transform.position = new Vector3(
            Mathf.Clamp(smoothedPosition.x, minPositionX, maxPositionX),
            Mathf.Clamp(smoothedPosition.y, minPositionY, maxPositionY),
            desiredPosition.z // Z軸（前進方向）は目標位置に追従
        );
    }
}
