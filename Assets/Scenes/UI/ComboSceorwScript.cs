using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class ComboSceorwScript : MonoBehaviour
{
    public int conboScore = 0;
    private TMP_Text scoreText;
    private ComboGaugeScript comboGaugeScript;
    private object canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        conboScore = 0;
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "";
        comboGaugeScript = GameObject.Find("ComboGauge").GetComponent<ComboGaugeScript>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(comboGaugeScript.Gauge<=0)
        {
            conboScore = 0;
            scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, 0f);
        }

        if (conboScore != 0)
        {
            scoreText.text = conboScore.ToString();
            scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, 1f);
        }
    }
}
