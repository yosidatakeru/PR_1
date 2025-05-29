using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboGaugeScript : MonoBehaviour
{
    // Start is called before the first frame update
    // コンボゲージの値
    public float Gauge = 0;

    // UIスライダーコンポーネント（インスペクターで設定）
    public Slider comboGauge;

    // CanvasGroup（透明度などを制御するため）
    CanvasGroup canvasGroup;
   

    void Start()
    {
        // CanvasGroupを現在のGameObjectから取得
        canvasGroup = GetComponent<CanvasGroup>();

        // コンボゲージを初期化
        Gauge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // スライダーにゲージの値を反映
        comboGauge.value = Gauge;

        // ゲージが0以下なら非表示
        if (Gauge <= 0)
        {
            canvasGroup.alpha = 0;
           
        }
        else 
        {
            canvasGroup.alpha = 1;
        }
        if (Time.timeScale != 0)
        { 
        // 毎フレームゲージを減少させる
        Gauge--;
        }
       
    }
}
