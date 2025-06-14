using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboGaugeScript : MonoBehaviour
{
    public float Gauge = 0;
    public Slider comboGauge;

    private CanvasGroup canvasGroup;
    private bool isBlinking = false;
    public float blinkThreshold = 30f; // この値以下で点滅開始

    private Image fillImage; // Fillイメージの参照
    public Color normalColor = Color.white;
    public Color yellowColor = Color.yellow;
    public Color redColor = Color.red;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        fillImage = comboGauge.fillRect.GetComponent<Image>();
        Gauge = 0;
    }

    void Update()
    {
        comboGauge.value = Gauge;

        // カラー変更処理
        if (Gauge <= 100)
        {
            fillImage.color = redColor;
        }
        else if (Gauge <= 300)
        {
            fillImage.color = yellowColor;
        }
        else
        {
            fillImage.color = normalColor;
        }

        if (Gauge <= 0)
        {
            canvasGroup.alpha = 0;
            StopBlinking(); // ゲージゼロでは点滅終了
        }
        else
        {
            if (Gauge <= blinkThreshold)
            {
                StartBlinking();
            }
            else
            {
                canvasGroup.alpha = 1;
                StopBlinking(); // 通常表示に戻す
            }
        }

        if (Time.timeScale != 0)
        {
            Gauge -= Time.deltaTime * 100; // ゲージの減少速度
            if (Gauge < 0) Gauge = 0;
        }
    }

    void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(Blink());
        }
    }

    void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            StopCoroutine(Blink());
            canvasGroup.alpha = 1;
        }
    }

    System.Collections.IEnumerator Blink()
    {
        while (isBlinking)
        {
            for (float t = 0; t < 1f; t += Time.deltaTime * 5)
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0.3f, t);
                yield return null;
            }
            for (float t = 0; t < 1f; t += Time.deltaTime * 5)
            {
                canvasGroup.alpha = Mathf.Lerp(0.3f, 1f, t);
                yield return null;
            }
        }
    }
}

