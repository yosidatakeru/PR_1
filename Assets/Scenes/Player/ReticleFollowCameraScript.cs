using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReticleFollowCameraScript : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public Camera mainCamera; // メインカメラ
    float speed = 150.0f; // 移動速度
   // float maxRange = 200f; // 移動範囲
    float distanceAhead = 10f; // プレイヤーの前方に配置する距離
   
   
    private bool playerPosition = true;
    private Vector3 offset; // プレイヤーとの相対位置
   
    float previousPlayerZ = 0;
    float previousPlayerX = 0;
    float previousPlayerY = 0;
    private Vector3 previousPlayerPosition;
    void Start()
    {
        offset = transform.position - player.position;
        previousPlayerZ = player.position.z;
        previousPlayerPosition = player.position;
    }

    void Update()
    {
        MoveReticle();
        transform.LookAt(player); // レティクルがプレイヤーの方向を見る
    }

    void MoveReticle()
    {
        if (Input.GetButtonDown("R3"))
        {
            playerPosition = !playerPosition; // 切り替え
        }

       //  Vector3 playerDelta = player.position - new Vector3(previousPlayerX, previousPlayerY, previousPlayerZ);

        float deltaZ = player.position.z - previousPlayerZ; // プレイヤーのZ移動量

        if (!playerPosition)
        {
            // スティック入力
            float moveX = Input.GetAxis("R_Stick_H");
            float moveY = Input.GetAxis("R_Stick_V");

            Vector3 stickMove = new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;

            // プレイヤーの移動量を計算（Zも含めてるけどあとで無視する）
            Vector3 playerDelta = player.position - previousPlayerPosition;

            // プレイヤー移動量 + スティック入力（Zはこの時点では使わない）
            Vector3 move = playerDelta + stickMove;

            // 現在位置に加算
            Vector3 newPosition = transform.position + move;

            // ビューポート内に制限（Clamp処理の中ではX・Yだけ処理してZは触らない想定）
            newPosition = ClampToCameraBounds(newPosition);

            // ★ Z軸処理は前のコードと同じにする
            newPosition.z = player.position.z + distanceAhead;

            // 位置を反映
            transform.position = newPosition;

            // 次回の比較用に現在のプレイヤー位置を保存
            previousPlayerPosition = player.position;
        }
        else
        {
            // カメラの前方に一定距離配置
            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 targetPosition = player.position + cameraForward * distanceAhead;

            // 滑らかに追従
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        }

        previousPlayerX = player.position.x;
        previousPlayerY = player.position.y;
        previousPlayerZ = player.position.z;
    }

    Vector3 ClampToCameraBounds(Vector3 position)
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(position);
        screenPoint.x = Mathf.Clamp(screenPoint.x, 0.1f, 0.9f);
        screenPoint.y = Mathf.Clamp(screenPoint.y, 0.1f, 0.9f);
        return mainCamera.ViewportToWorldPoint(screenPoint);
    }
}


