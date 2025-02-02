using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class EnemySpawnScript : MonoBehaviour
{

    //敵
    public GameObject enemy;
    //地上の敵
    public GameObject ChaseAndOrbit;


    Transform player;
    //敵のスポンジ時間の制御
    // int enemeSoawn = 5;
    //スポーン数
    public int enemySpawns = 0;
    //エネミーの座標
    Vector3 EnemePos = Vector3.zero;
    //敵の速さ
    //float spawnSpeed = 100;
    //撃破数
    public int defeats = 0;
    int wave = 0;
   public EnemySpawnerScript enemyScript;
    int Spawnstime = 1200;
    

    // Start is called before the first frame update
    void Start()
    {
        //  enemeSoawn = 5;
        // enemySpawnerScript = GameObject.Find("ChaseAndOrbitObject").GetComponent<EnemySpawnerScript>();

    }

    //設定項目

    // Update is called once per frame
    void Update()
    {



        switch (wave)
        {
            case 0:
                //ゲーム開始の処理
                enemySpawns = 8;
                wave = 1;
                break;
            case 1:


                EnemePos.x = 40;
                EnemePos.y = 0;
                EnemePos.z = 30;
                for (int i = 0; i <= enemySpawns - 1; i++)
                {
                    //敵のスポーン位置


                    //enemyCount = 2;     // スポーンする敵の数
                    //spawnRadius = 3f; // スポーンする円の半径
                    //orbitSpeed = 2f;  // 敵の回転速度
                    //speed = 0.0f;//回転軸の移動
                    //arrangement = 1;//どう配置するか（横１縦２）
                    //move = 2;//回転させ方の設定
                    //centerMovement = 0;//移動の設定
                    // spawnRadiusmove = 1;//拡大と収縮
                    EnemePos.x += 5;
                    //オブジェクトのスポーン
                    ChaseAndOrbit = Instantiate(ChaseAndOrbit, new Vector3(EnemePos.x, EnemePos.y, EnemePos.z), Quaternion.identity);
                    enemyScript = ChaseAndOrbit.GetComponent<EnemySpawnerScript>();
                    enemyScript.enemyCount = 10;
                    enemyScript.spawnRadius = 3f;
                    enemyScript.orbitSpeed = 2f;
                    enemyScript.arrangement = 1;
                    enemyScript.speed = 4.0f;//回転軸の移動
                    enemyScript.move = 5;
                    enemyScript.centerMovement = 1;
                    enemyScript.spawnRadiusmove = 0;
                    //スポーンするたびに減らす
                    //enemySpawns--;
                }

                EnemePos.x = -40;
                EnemePos.y = 0;
                EnemePos.z = 30;

                for (int i = 0; i <= enemySpawns - 1; i++)
                {
                    //敵のスポーン位置


                  
                    EnemePos.x -= 5;
                    //オブジェクトのスポーン
                    ChaseAndOrbit = Instantiate(ChaseAndOrbit, new Vector3(EnemePos.x, EnemePos.y, EnemePos.z), Quaternion.identity);
                    enemyScript = ChaseAndOrbit.GetComponent<EnemySpawnerScript>();
                    enemyScript.enemyCount = 10;
                    enemyScript.spawnRadius = 3f;
                    enemyScript.orbitSpeed = 2f;
                    enemyScript.arrangement = 1;
                    enemyScript.speed = 4.0f;//回転軸の移動
                    enemyScript.move = 7;
                    enemyScript.centerMovement = 2;
                    enemyScript.spawnRadiusmove = 0;
                    //スポーンするたびに減らす
                    //enemySpawns--;
                }
                wave = 2;
                break;

            case 2:
                 //準備   

                 break;

        }


        
    }
}


    
