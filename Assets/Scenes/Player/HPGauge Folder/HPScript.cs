using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPScript : MonoBehaviour
{
    public float Gauge = 0;
    public Slider HPGauge;

    private CanvasGroup canvasGroup;
    private float previousGauge;
    private RectTransform gaugeTransform;
    private Vector3 originalPos;
    private float shakeTime = 0;
    private float shakeDuration = 0.6f;
    private float shakeMagnitude = 10f;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        gaugeTransform = HPGauge.GetComponent<RectTransform>();
        originalPos = gaugeTransform.anchoredPosition;

        // HPゲージを初期化
        Gauge = 1000;
        previousGauge = Gauge;
    }

    void Update()
    {
        // HPの上限を制限
        if (Gauge >= 1000)
        {
            Gauge = 1000;
        }

        // スライダーにゲージの値を反映
        HPGauge.value = Gauge;

        // ゲージが減ったときに振動開始
        if (Gauge < previousGauge)
        {
            shakeTime = shakeDuration;
        }

        // 振動処理
        if (shakeTime > 0)
        {
            Vector2 shakeOffset = Random.insideUnitCircle * shakeMagnitude;
            gaugeTransform.anchoredPosition = originalPos + new Vector3(shakeOffset.x, shakeOffset.y, 0);
            shakeTime -= Time.deltaTime;
        }
        else
        {
            gaugeTransform.anchoredPosition = originalPos;
        }

        // ゲージが0以下なら非表示
        if (Gauge <= 0)
        {
            canvasGroup.alpha = 0;
        }
       

        // 現在のゲージを保存
        previousGauge = Gauge;
    }
}
