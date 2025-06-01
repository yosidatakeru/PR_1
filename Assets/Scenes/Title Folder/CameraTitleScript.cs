using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTitleScript : MonoBehaviour
{
    Vector3 initialPosition = new Vector3(0f, 2f, -13f);
    Vector3 initialRotation = new Vector3(0f, 0f, 0f);

    public bool IsCameraMoveFinished { get; private set; } = false;

    public Transform player;
    Vector3 targetPosition = new Vector3(0f, 2f, 13f); // ← 移動先の座標
    float moveSpeed = 10f;

    bool isMoving = false;

    public CanvasGroup title;
    public CanvasGroup OperationInstructions;

    // Start is called before the first frame update
    void Start()
    {
        // カメラの位置を初期化
        transform.position = initialPosition;

        // カメラの回転を初期化
        transform.rotation = Quaternion.Euler(initialRotation);
        isMoving = false;
        OperationInstructions.alpha = 0;
        title.alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("Abutton"))
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

               
                OperationInstructions.alpha = 1;

              
                IsCameraMoveFinished = true;
                
            }
        }
    }

}
