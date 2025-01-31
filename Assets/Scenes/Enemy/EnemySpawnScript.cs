using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class EnemySpawnScript : MonoBehaviour
{
    
    //敵
    public GameObject enemy;
    //地上の敵
    public GameObject groundEnemy;
    //敵のスポンジ時間の制御
    int enemeSoawn = 5;
    //スポーン数
    public int enemySpawns = 5;
    //エネミーの座標
    Vector3 EnemePos = Vector3.zero;
    //敵の速さ
    //float spawnSpeed = 100;
    //撃破数
   public int defeats = 0;
   

  
    // Start is called before the first frame update
    void Start()
    {
      //  enemeSoawn = 5;

    }



    // Update is called once per frame
    void Update()
    {


      　//敵が出てくる時間
        enemeSoawn--;
        //ス本させるOBJを後ろに進める
       // transform.position += spawnSpeed * transform.forward * Time.deltaTime;



       // Debug.Log(defeats);
       

        if (enemeSoawn < 0&& enemySpawns > 5  )
        {

            //敵のスポーン位置
            EnemePos.x = Random.Range(0, 10) + transform.position.x;
            EnemePos.y = Random.Range(0, 10) + transform.position.y;
            EnemePos.z = Random.Range(0, 10) + transform.position.z;

           
            //オブジェクトのスポーン
            Instantiate(enemy, new Vector3(EnemePos.x, EnemePos.y, EnemePos.z), Quaternion.identity);
          //  Instantiate(groundEnemy, new Vector3(EnemePos.x, EnemePos.y, EnemePos.z), Quaternion.identity);
          //スポーンするたびに減らす
            enemySpawns--;
            //次の敵のスポーンまでの時間
            enemeSoawn = 10;


        }
        
        //一定数倒すとリスポーン
        if (defeats == 5)
        {
            enemySpawns = 10;
            defeats = 0;
        }

    }

 
}
