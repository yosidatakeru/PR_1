using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{

    public static int score = 0;
    private static ScoreScript instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private ScoreResultScript resultScript;

    private int displayedScore = 0;
    private Vector3 originalScale;
    private bool isAnimating = false;

    float minSpeed = 30f;
    float speedMultiplier = 2f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        score = 0;
        displayedScore = 0;

        if (scoreText != null)
            scoreText.text = "0";

        originalScale = transform.localScale;
    }

    void Update()
    {
        if (score < 0) score = 0;

       
        if (displayedScore != score)
        {
            int diff = Mathf.Abs(score - displayedScore);
            float dynamicSpeed = Mathf.Max(minSpeed, diff * speedMultiplier);
            int delta = Mathf.CeilToInt(dynamicSpeed * Time.deltaTime);

            if (displayedScore < score)
            {
                displayedScore += delta;
                if (displayedScore > score) displayedScore = score;
            }
            else
            {
                displayedScore -= delta;
                if (displayedScore < score) displayedScore = score;
            }

            if (scoreText != null)
                scoreText.text = displayedScore.ToString();

            if (!isAnimating)
                StartCoroutine(AnimateScale());
        }

        
    }

    public static void AddScore(int amount)
    {
        if (amount <= 0) return;

       

        if (instance != null && instance.resultScript != null)
        {
            instance.resultScript.ShowAddedValue(amount);
        }
    }

    private System.Collections.IEnumerator AnimateScale()
    {
        isAnimating = true;
        transform.localScale = originalScale * 1.2f;

        float t = 0f;
        float duration = 0.6f;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, t / duration);
            yield return null;
        }

        transform.localScale = originalScale;
        isAnimating = false;
    }

}




