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
    float enemySpeed = 100;
    //敵のスピード
    float Speed = 4.0f;
    //敵の攻撃制御
    int timeUntilNextShot = 0;
    //弾の制御乱数
    int bulletTimerReset = 0;

    //敵の動き制御
    int behaviorattern = 0;

    int comboScore = 0;

    int score = 0;

    int destroyScore = 100;

    public GameObject particle;

    EnemySpawnScript enemySpawnScript;

    ScoreScript scoreScript;

    ComboSceorwScript comboSceorwScript;

    private ComboGaugeScript comboGaugeScript;

    private GameObject EnemySpawnObject; 



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
    }



    // Update is called once per frame
    void Update()
    {

        //transform.position += enemySpeed * transform.forward * Time.deltaTime;

       
        switch (behaviorattern)
        {
           //左
           case 0:
             transform.position -= Speed * transform.right * Time.deltaTime;
             break;
            //右
            case 1:

                transform.position += Speed * transform.right * Time.deltaTime;
                break;
             //上
             case 2:
                
                 transform.position += Speed * transform.up * Time.deltaTime;
                 break;
             //下
             case 3:

                 transform.position += Speed * transform.up * Time.deltaTime;
                 break;
              //何もしない
              case 4:
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
            GetComponent<BoxCollider>().enabled = false;
            Instantiate(particle, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            //当たったら消滅
            GetComponent<MeshRenderer>().enabled = false;
            enemySpawnScript.defeats += 1;

            comboGaugeScript.Gauge = 600;

            //スコア刑の処理
            //ここ調整する
            score =  comboSceorwScript.conboScore * destroyScore / 2;

            //スコアの受け渡い
            scoreScript.score += destroyScore + score;

            comboSceorwScript.conboScore += 1;


            //敵を消す/
            Destroy(gameObject);



        }
       
    }
     
    

    
}
