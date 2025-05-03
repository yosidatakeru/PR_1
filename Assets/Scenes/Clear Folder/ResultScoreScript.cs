using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class ResultScoreScript : MonoBehaviour
{
    private TMP_Text scoreText;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "score:0";
       
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.color = Color.red;
        

        
            score = ScoreScript.score;
        

        scoreText.text = "SCORE:" + score.ToString();
    }
}
