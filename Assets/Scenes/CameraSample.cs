using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSample : MonoBehaviour
{
    public Transform player; // プレイヤーのTransformをアサイン
    public Vector3 offset = new Vector3(0, 1, -3); // カメラのオフセット
    public float smoothSpeed = 40; // カメラの追尾速度

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        if (player == null) return; // プレイヤーが未設定なら処理しない

        // カメラの目標位置を計算
        Vector3 desiredPosition = player.position + offset;

        // スムーズに目標位置に移動
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // プレイヤーを見る
        transform.LookAt(player);

    }
}
