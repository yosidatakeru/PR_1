using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRailMover : MonoBehaviour
{
    [Header("移動速度")]
    public float baseSpeed = 20f;
    public float currentSpeed;

    void Start()
    {
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        // 毎フレーム前方に進める（実際のパスやCinemachineを使う場合はDolly位置を制御）
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}
