using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTitleScript : MonoBehaviour
{
    // === 初期設定 ===
    // カメラの初期位置
    Vector3 initialPosition = new Vector3(0f, 2f, -13f);
    // カメラの初期回転

    Vector3 initialRotation = new Vector3(0f, 0f, 0f);

    // === カメラ移動完了判定 ===
    public bool IsCameraMoveFinished { get; private set; } = false;
    // === プレイヤーと移動設定 ===
    // 注視対象のプレイヤー
    public Transform player;
    // カメラの移動先
    Vector3 targetPosition = new Vector3(0f, 2f, 13f); // ← 移動先の座標
    // 移動速度
    float moveSpeed = 10f;
    // 移動中フラグ
    bool isMoving = false;
    // === UI関連 ===
    // タイトルUI
    public CanvasGroup title;
    // 操作説明UI
    public CanvasGroup OperationInstructions;

    // Start is called before the first frame update
    void Start()
    {
        // カメラの位置を初期化
        transform.position = initialPosition;
        // カメラの回転を初期化
        transform.rotation = Quaternion.Euler(initialRotation);

        // 初期化
        isMoving = false;
        // UI設定
        OperationInstructions.alpha = 0;
        title.alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {    // スペースキーまたはゲームパッドのAボタンで開始
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("Abutton"))
        {
            isMoving = true;
            // タイトルを非表示
            title.alpha = 0;
        }
        // カメラ移動処理
        if (isMoving == true)
        {
            // プレイヤーの方向を向く
            if (player != null)
            {
                transform.LookAt(player);
            }

            // 特定の位置までスムーズに移動
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 到達したら移動停止
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;

                //操作説明を表示
                OperationInstructions.alpha = 1;

              
                IsCameraMoveFinished = true;
                
            }
        }
    }

}
