using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiScript : MonoBehaviour
{
    public CanvasGroup hpGauge;
    public CanvasGroup canvasGroup;
    public CanvasGroup canvasUi;
   
    public CanvasGroup ScoreText;
    public CanvasGroup fadeinUp;
    public CanvasGroup FadeInDown;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        fadeinUp.alpha = 1;
        FadeInDown.alpha = 1;
        hpGauge.alpha = 0;
        canvasGroup.alpha = 0;
        canvasUi.alpha = 0;
        ScoreText.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 pos = player.transform.position;
            Debug.Log("プレイヤーの座標: " + pos);
        }

        if (player.transform.position.z >= 0 && player.transform.position.z <= 3300)
        {
            fadeinUp.alpha = 0;
            FadeInDown.alpha = 0;
            hpGauge.alpha = 1;
            canvasGroup.alpha = 1;
            canvasUi.alpha = 1;
            ScoreText.alpha = 1;
        }

        if (player.transform.position.z >= 3300)
        {
            fadeinUp.alpha = 0;
            FadeInDown.alpha = 0;
            hpGauge.alpha = 0;
            canvasGroup.alpha = 0;
            canvasUi.alpha = 0;
           
        }
    }
}
