using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FixedCanonScript : MonoBehaviour
{
    public GameObject particle;
    int scoreResult = 0;
    int destroyScore = 500;
    private ComboGaugeScript comboGaugeScript;
    ComboSceorwScript comboSceorwScript;
    Vector3 particleposition = Vector3.zero;
  
    public ParticleSystem destructionParticles; // 破壊のパーティクルシステム
    private int comboup = 1;
    //いくつ加算されたか
    int addAmount;
    // Start is called before the first frame update
    void Start()
    {
        comboGaugeScript = GameObject.Find("ComboGauge").GetComponent<ComboGaugeScript>();
        comboSceorwScript = GameObject.Find("ComboScore (TMP)").GetComponent<ComboSceorwScript>();
        particleposition = new Vector3(0, 3, 0);
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
            Instantiate(particle, new Vector3(transform.position.x, transform.position.y + particleposition.y, transform.position.z), Quaternion.identity);


            comboGaugeScript.Gauge = 600;

            //スコアの処理
            scoreResult = comboSceorwScript.conboScore * destroyScore / 9;

            //スコアの受け渡い
            ScoreScript.score += destroyScore + scoreResult;

            addAmount = destroyScore + scoreResult;


            ScoreScript.AddScore(addAmount);

            comboSceorwScript.conboScore += comboup;


            //敵を消す/
            Destroy(gameObject);



        }

    }
}
