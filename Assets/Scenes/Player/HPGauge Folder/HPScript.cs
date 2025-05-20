using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPScript : MonoBehaviour
{
    public float Gauge = 0;
    public Slider HPGauge;
    CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {

        canvasGroup = GetComponent<CanvasGroup>();

        // HPゲージを初期化
        Gauge = 1000;
    }

    // Update is called once per frame
    void Update()
    {

        if (Gauge >= 1000) 
        {
            Gauge = 1000;
        }

        // スライダーにゲージの値を反映
        HPGauge.value = Gauge;

        // ゲージが0以下なら非表示
        if (Gauge <= 0)
        {
            canvasGroup.alpha = 0;

        }
        else
        {
            canvasGroup.alpha = 1;
        }
    }
}
