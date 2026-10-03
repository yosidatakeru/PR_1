using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class ResultScoreScript : MonoBehaviour
{
    private TMP_Text scoreText;
    public static int result = 0;
    public static int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "score:0";
       
    }

    // Update is called once per frame
    void Update()
    {

        score = ScoreScript.score;

        if(score > result)
        {
            result += Random.Range(200, 1000);
        }
        
        if (score < result)
        {
            result = ScoreScript.score;
        }




        scoreText.text = "SCORE:" + result.ToString();
    }
}
