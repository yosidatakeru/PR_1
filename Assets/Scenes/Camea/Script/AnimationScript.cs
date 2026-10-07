using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Transform player;
    Vector3 startOffset = new Vector3(-4, -3, 10);
    Vector3 defaultOffset = new Vector3(0, 0, -4);
    float duration = 5.0f;

    private float timer = 0f;
    public static bool isAnimating = true;

    public bool IsAnimating => isAnimating;

    CameraScript target;

    void Start()
    {
        if (player != null)
        {
            isAnimating = true;
            transform.position = player.position + startOffset;
            transform.LookAt(player);
            target = GetComponent<CameraScript>();
        }
    }

    // Update ではなく LateUpdate で処理（プレイヤーの移動が確定した後に実行する）
    void LateUpdate()
    {
       
        if (!isAnimating || player == null) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);

        // SmoothStepを使うことで、動き出しと終わりがなめらかになります
        float smoothT = Mathf.SmoothStep(0f, 1f, t);

        // 目標位置の計算
        Vector3 targetPos = Vector3.Lerp(player.position + startOffset, player.position + defaultOffset, smoothT);
        transform.position = targetPos;

        // 常に正確にプレイヤーを注視させることで視線のガタつきを防ぐ
        Vector3 dir = player.position - transform.position;
        if (dir.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }

        if (t >= 1.0f)
        {
            isAnimating = false;
            target.enabled = true;
        }
    }
}