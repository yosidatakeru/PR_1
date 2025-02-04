using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSample : MonoBehaviour
{
    public Transform player; // プレイヤーのTransformをアサイン
    public Vector3 offset = new Vector3(0, 1, -3); // カメラのオフセット
    public float smoothSpeed = 10.0f; // カメラの追尾速度
    public float maxTiltAngle = 15.0f; // カメラの最大傾き角度
    public float tiltSpeed = 5.0f; // カメラの傾きスムーズ速度

    private Vector3 lastPlayerPosition; // 前フレームのプレイヤー位置
    private float tiltAmount = 0f; // 現在の傾き

    void Start()
    {
        if (player != null)
        {
            lastPlayerPosition = player.position; // 初期位置を記録
        }
    }


    void Update()
    {
        if (player == null) return; // プレイヤーが未設定なら処理しない

        // カメラの目標位置を計算
        Vector3 desiredPosition = player.position + offset;

        // スムーズに目標位置に移動
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // プレイヤーの移動速度を計算（前フレームとの位置変化）
        float speedX = (player.position.x - lastPlayerPosition.x) / Time.deltaTime;

        // 傾きを計算（スムーズに変化させる）
        float targetTilt = Mathf.Clamp(speedX / 10f, -1f, 1f) * maxTiltAngle;
        tiltAmount = Mathf.Lerp(tiltAmount, targetTilt, tiltSpeed * Time.deltaTime);

        // カメラの回転をスムーズに適用（Z軸を傾ける）
        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -tiltAmount);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);

        // 現在のプレイヤー位置を保存
        lastPlayerPosition = player.position;
    }
}
