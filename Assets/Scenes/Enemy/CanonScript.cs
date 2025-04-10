using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.SocialPlatforms.Impl;

public class CanonScript : MonoBehaviour
{
    //エフェクト
    public GameObject particle;
    public GameObject Score;
    private ComboGaugeScript comboGaugeScript;
    ComboSceorwScript comboSceorwScript;
    ScoreScript scoreScript;
    int score = 0;
    int destroyScore = 100;
    Vector3 particleposition = Vector3.zero;
    public GameObject[] brokenParts; // 敵が崩れるパーツ
    public ParticleSystem destructionParticles; // 破壊のパーティクルシステム
    private int comboup = 1;
    // Start is called before the first frame update
    void Start()
    {
        comboGaugeScript = GameObject.Find("ComboGauge").GetComponent<ComboGaugeScript>();
        comboSceorwScript = GameObject.Find("ComboScore (TMP)").GetComponent<ComboSceorwScript>();
        scoreScript = GameObject.Find("ScoreText (TMP)").GetComponent<ScoreScript>();
        particleposition = new Vector3 (0, 3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
          //  Instantiate(Score, new Vector3(transform.position.x, transform.position.y + particleposition.y, transform.position.z), Quaternion.identity);
            Instantiate(particle, new Vector3(transform.position.x, transform.position.y + particleposition.y , transform.position.z), Quaternion.identity);
           

            comboGaugeScript.Gauge = 600;
           
            //スコアの処理
            score = comboSceorwScript.conboScore * destroyScore / 9;

            //スコアの受け渡い
            scoreScript.score += destroyScore + score;

            comboSceorwScript.conboScore += comboup;


            //敵を消す/
            Destroy(gameObject);



        }

    }
}
