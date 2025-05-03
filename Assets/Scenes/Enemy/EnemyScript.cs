using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    //enemyスクリプトの呼び出し
    EnemySpawnScript enemySpawn;
    //弾のオブジェクトの呼び出し
    public GameObject EnemyBullet;


    //後ろに下がるスピード
    float enemySpeed = 0;
    //敵のスピード
    float Speed = 0;
    //敵の攻撃制御
    int timeUntilNextShot = 0;
    //弾の制御乱数
    int bulletTimerReset = 0;

    //敵の動き制御
    int behaviorattern = 0;
   
    int comboScore = 0;

    int scoreReset = 0;

    int destroyScore = 10;

    public GameObject particle;

    EnemySpawnScript enemySpawnScript;

    ScoreScript scoreScript;

    ComboSceorwScript comboSceorwScript;

    private ComboGaugeScript comboGaugeScript;

    private GameObject EnemySpawnObject;

    public AudioClip deathSound;  // 敵が死んだときの効果音

    public  GameObject EnemyDestroyObject;
    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextShot = Random.Range(300, 600);
        bulletTimerReset = timeUntilNextShot;
        enemySpawnScript = GameObject.Find("EnemySpawnObject").GetComponent<EnemySpawnScript>();
        scoreScript = GameObject.Find("ScoreText (TMP)").GetComponent<ScoreScript>();
        comboSceorwScript = GameObject.Find("ComboScore (TMP)").GetComponent<ComboSceorwScript>();
        comboGaugeScript = GameObject.Find("ComboGauge").GetComponent<ComboGaugeScript>();
        behaviorattern = 0;

       // Invoke(nameof(DelayedDestroy), 10.0f); // 2秒後に実行


    }



    // Update is called once per frame
    void Update()
    {

        //transform.position += enemySpeed * transform.forward * Time.deltaTime;

       
        switch (behaviorattern)
        {

            case 0:
                //何もしない
             break;
           
            case 1:
                //左
                transform.position -= Speed * transform.right * Time.deltaTime;
                break;
           
             case 2:
                //右
                transform.position += Speed * transform.right * Time.deltaTime;
                 break;
           
             case 3:
                //上
                transform.position += Speed * transform.up * Time.deltaTime;
                 break;
            
             case 4:
                //下
                transform.position += Speed * transform.up * Time.deltaTime;
                 break;
        }









        //攻撃
        timeUntilNextShot--;
        if (timeUntilNextShot <= 0)
        {
           
            //敵の生成
            Instantiate(EnemyBullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

           // EnemySpawn;
            timeUntilNextShot = bulletTimerReset;

        }

        


    }

    //敵の当たり判定
    void OnCollisionEnter(Collision collision)
    {
       
      
        if (collision.gameObject.tag == "Bullet")
        {
           
            Instantiate(particle, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            //当たったら消滅
           // GetComponent<MeshRenderer>().enabled = false;
            //enemySpawnScript.defeats += 1;

            comboGaugeScript.Gauge = 600;
            // 敵が死んだときの効果音を再生
          
            //スコア刑の処理
            //ここ調整する
            scoreReset =  comboSceorwScript.conboScore * destroyScore /9;

            //スコアの受け渡い
            ScoreScript.score += destroyScore + scoreReset;

            comboSceorwScript.conboScore += 1;


            //敵を消す/
            Destroy(gameObject);



        }
       
    }


    void DelayedDestroy()
    {
        enemySpawnScript.defeats += 1;
        Instantiate(EnemyDestroyObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }

}
