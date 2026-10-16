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
        if (Input.GetButtonDown("R3") || Input.GetKeyDown(KeyCode.F))
        {
            playerPosition = !playerPosition; // 切り替え
            previousPlayerPosition = player.position;
        }

        float deltaZ = player.position.z - previousPlayerZ; // プレイヤーのZ移動量

        if (!playerPosition)
        {
            // スティック入力
            float moveX = Input.GetAxis("R_Stick_H");
            float moveY = Input.GetAxis("R_Stick_V");

            // アローキー入力も加算
            if (Input.GetKey(KeyCode.RightArrow)) moveX += 1f;
            if (Input.GetKey(KeyCode.LeftArrow)) moveX -= 1f;
            if (Input.GetKey(KeyCode.UpArrow)) moveY += 1f;
            if (Input.GetKey(KeyCode.DownArrow)) moveY -= 1f;

            // 正規化
            Vector2 inputVector = new Vector2(moveX, moveY);
            if (inputVector.magnitude > 1f)
                inputVector.Normalize();

            // 必要に応じてアローキー時だけスピードを落とす
            bool usingArrow = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) ||
                              Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow);

            float moveSpeed = usingArrow ? speed * 0.1f : speed;

            // --- 修正箇所：カメラの向き基準で移動ベクトルを作成 ---
            // カメラの「右方向」と「上方向」を基準に入力ベクトルを回転させる
            Vector3 worldMoveDir = (mainCamera.transform.right * inputVector.x) + (mainCamera.transform.up * inputVector.y);

            // スティック入力による移動量
            Vector3 stickMove = worldMoveDir * moveSpeed * Time.deltaTime;

            // プレイヤーのフレーム間移動量
            Vector3 playerDelta = player.position - previousPlayerPosition;

            // 現在位置に加算（プレイヤー移動追従 + カメラ基準スティック移動）
            Vector3 newPosition = transform.position + playerDelta + stickMove;

            // 画面内制限（ビューポート座標で制御）
            newPosition = ClampToCameraBounds(newPosition);

            // --- 修正箇所：カメラの回転に合わせて前方の距離を正確に保つ ---
            // プレイヤー位置からカメラ前方方向へ distanceAhead 分だけ離した平面上の位置に補正
            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 playerToNewPos = newPosition - player.position;

            // カメラ前方向の成分を引いて、カメラの向きに対して垂直（上下左右）なオフセットのみ抽出
            Vector3 orthogonalPos = playerToNewPos - Vector3.Project(playerToNewPos, cameraForward);

            // プレイヤー位置 + 上下左右オフセット + カメラ前方向 distanceAhead
            newPosition = player.position + orthogonalPos + (cameraForward * distanceAhead);

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