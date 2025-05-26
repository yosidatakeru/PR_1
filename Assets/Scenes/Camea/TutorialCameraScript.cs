using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCameraScript : MonoBehaviour
{
   
        public Transform player; // プレイヤーのTransformをアサイン
        Vector3 offset = new Vector3(0, 0, 0); // カメラのオフセット
        float smoothSpeed = 10.0f; // カメラの追尾速度
        float maxTiltAngle = 2.0f; // カメラの最大傾き角度
        //float tiltSpeed = 5.0f; // カメラの傾きスムーズ速度

        private Vector3 lastPlayerPosition; // 前フレームのプレイヤー位置
        private float tiltAmount = 0f; // 現在の傾き
        private float tiltVelocity = 0f; // SmoothDamp用の速度変数

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
            float targetTilt = Mathf.Clamp((speedX / 10f) * maxTiltAngle, -maxTiltAngle, maxTiltAngle);
            tiltAmount = Mathf.SmoothDamp(tiltAmount, targetTilt, ref tiltVelocity, 0.2f);

            // プレイヤーの向きを基準にカメラの回転をスムーズに適用
            Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation * Quaternion.Euler(0, 0, -tiltAmount), Time.deltaTime * smoothSpeed);

            // 現在のプレイヤー位置を保存
            lastPlayerPosition = player.position;
        }
    
}
