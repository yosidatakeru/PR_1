using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour
{
    private CameraController cameraController;
   
    void Start()
    {
        // シーン上の CameraController を検索（MainCamera にアタッチされている想定）
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            cameraController = mainCam.GetComponent<CameraController>();
        }
    }

    

   public void TakeDamage()
    {
        // ここにHP減少などの処理を記述
        Debug.Log("ダメージを受けた！");

        // カメラシェイクを呼び出す
        if (cameraController != null)
        {
            cameraController.TriggerCameraShake(0.3f, 0.2f);
        }
    }
}
