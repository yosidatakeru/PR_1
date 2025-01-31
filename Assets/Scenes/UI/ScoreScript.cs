using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
   
    // Start is called before the first frame update
    public int score = 0;
    private TMP_Text scoreText;
    void Start()
    {
        score = 0;
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "score:0";
    }

    // Update is called once per frame
    void Update()
    {
       
        scoreText.text = "SCORE:" + score.ToString();
    }
}
