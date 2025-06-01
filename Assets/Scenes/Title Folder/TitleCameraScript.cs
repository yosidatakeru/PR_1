using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraScript : MonoBehaviour
{
    Vector3 initialPosition = new Vector3(0f, 2f, -13f);
    Vector3 initialRotation = new Vector3(0f, 0f, 0f);

    public Transform player;
    Vector3 targetPosition = new Vector3(0f, 2f, 13f); // ← 移動先の座標
    float moveSpeed = 10f;

    private bool isMoving = false;

    public CanvasGroup title;

    // Start is called before the first frame update
    void Start()
    {
        // カメラの位置を初期化
        transform.position = initialPosition;

        // カメラの回転を初期化
        transform.rotation = Quaternion.Euler(initialRotation);
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = true;
            title.alpha = 0;
        }

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
            }
        }
    }
}

