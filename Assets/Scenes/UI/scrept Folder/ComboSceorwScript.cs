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
    private CanvasGroup canvasGroup;

    private Vector3 originalScale;
    private int lastConboScore = 0;
    private Coroutine scaleCoroutine;

    void Start()
    {
        conboScore = 0;
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "";
        comboGaugeScript = GameObject.Find("ComboGauge").GetComponent<ComboGaugeScript>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalScale = scoreText.rectTransform.localScale;
    }

    void Update()
    {
        if (comboGaugeScript.Gauge <= 0)
        {
            conboScore = 0;
            scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, 0f);
        }

        if (conboScore != 0)
        {
            scoreText.text = conboScore.ToString();
            scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, 1f);

            if (conboScore > lastConboScore)
            {
                if (scaleCoroutine != null)
                    StopCoroutine(scaleCoroutine);

                scaleCoroutine = StartCoroutine(PopText());
            }

            lastConboScore = conboScore;
        }
    }

    private IEnumerator PopText()
    {
        float duration = 0.15f;
        float elapsed = 0f;
        Vector3 targetScale = originalScale * 1.3f;

        // Šg‘å
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            scoreText.rectTransform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        // k¬
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            scoreText.rectTransform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }

        scoreText.rectTransform.localScale = originalScale;
    }
}

