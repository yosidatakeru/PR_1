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
        if(ScoreScript.score>= 150000)
        {
            scoreText.text = "SS";
        }
        else if(ScoreScript.score <= 150000 && ScoreScript.score >= 140000)
        {
            scoreText.text = "S";
        }
        else if (ScoreScript.score <= 140000 && ScoreScript.score >= 120000)
        {
            scoreText.text = "A";
        }
        else if (ScoreScript.score <= 120000 && ScoreScript.score >= 80000)
        {
            scoreText.text = "B";
        }
        else if (ScoreScript.score <= 80000 && ScoreScript.score >= 40000)
        {
            scoreText.text = "C";
        }
        else if (ScoreScript.score <= 40000 )
        {
            scoreText.text = "D";
        }
    }
}
