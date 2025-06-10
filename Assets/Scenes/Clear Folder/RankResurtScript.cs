using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankResurtScript : MonoBehaviour
{
    private TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreScript.score>= 200000)
        {
            scoreText.text = "SS";
            scoreText.color = new Color32(255, 215, 0, 255); // ゴールド
        }
        else if(ScoreScript.score <= 200000 && ScoreScript.score >= 140000)
        {
            scoreText.text = "S";
            scoreText.color = new Color32(192, 192, 192, 255); // シルバー
        }
        else if (ScoreScript.score <= 140000 && ScoreScript.score >= 120000)
        {
            scoreText.text = "A";
            scoreText.color = new Color32(0, 255, 0, 255); // 緑
        }
        else if (ScoreScript.score <= 120000 && ScoreScript.score >= 80000)
        {
            scoreText.text = "B";
            scoreText.color = new Color32(0, 128, 255, 255); // 青
        }
        else if (ScoreScript.score <= 80000 && ScoreScript.score >= 40000)
        {
            scoreText.text = "C";
            scoreText.color = new Color32(255, 165, 0, 255); // オレンジ
        }
        else if (ScoreScript.score <= 40000 )
        {
            scoreText.text = "D";
              scoreText.color = new Color32(255, 0, 0, 255); // 赤
        }
    }
}
