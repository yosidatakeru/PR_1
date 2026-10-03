using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankResurtScript : MonoBehaviour
{
    private TMP_Text resultText;
    int score = 0;
    int result = 0;
    public CanvasGroup fade;
    // Start is called before the first frame update
    void Start()
    {
        resultText = GetComponent<TMP_Text>();
        fade.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
       score = ResultScoreScript.score;
       result =  ResultScoreScript.result;


        if (score == result)
        {
            fade.alpha = 1;
            if (ScoreScript.score >= 200000)
            {
                resultText.text = "SS";
                resultText.color = new Color32(255, 215, 0, 255); // ゴールド
            }
            else if (ScoreScript.score <= 200000 && ScoreScript.score >= 140000)
            {
                resultText.text = "S";
                resultText.color = new Color32(192, 192, 192, 255); // シルバー
            }
            else if (ScoreScript.score <= 140000 && ScoreScript.score >= 120000)
            {
                resultText.text = "A";
                resultText.color = new Color32(0, 255, 0, 255); // 緑
            }
            else if (ScoreScript.score <= 120000 && ScoreScript.score >= 80000)
            {
                resultText.text = "B";
                resultText.color = new Color32(0, 128, 255, 255); // 青
            }
            else if (ScoreScript.score <= 80000 && ScoreScript.score >= 40000)
            {
                resultText.text = "C";
                resultText.color = new Color32(255, 165, 0, 255); // オレンジ
            }
            else if (ScoreScript.score <= 40000)
            {
                resultText.text = "D";
                resultText.color = new Color32(255, 0, 0, 255); // 赤
            }
        }
    }
}
