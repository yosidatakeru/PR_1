using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;


public class EnemySpawnScript : MonoBehaviour
{
 




    //敵
    public GameObject enemy;
    //地上の敵
    public GameObject ChaseAndOrbit;

    public GameObject woll;
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
    public int wave = 0;
    public EnemySpawnerScript enemyScript;
    int Spawnstime = 1200;
    Vector3 enemyPosition = new Vector3(0, 0, 0);
    int time = 0;

    // Start is called before the first frame update
    void Start()
    {
        //  enemeSoawn = 5;
        // enemySpawnerScript = GameObject.Find("ChaseAndOrbitObject").GetComponent<EnemySpawnerScript>();
        InvokeRepeating("RepeatMethod", 1f, 1f);
    }

    //設定項目
    void RepeatMethod()
    {
        // ここに毎秒実行したい処理を書く
        time++;
        Debug.Log(time);
    }
    // Update is called once per frame
    void Update()
    {

        
        switch (wave)
        {
            case 0:
                //ゲーム開始の処理
                enemySpawns = 6;
                wave = 1;
                break;
            case 1:
                //enemyCount ＝ 0；　　　　　// スポーンする敵の数
                //arrangement = 0;//どう配置するか（横１縦２）
                //spawnRadius = 0f; // スポーンする円の半径
                //centerMovement = 0;//移動の設定(左1右2上3下4前5後6)
                //speed = 0.0f;//回転軸の移動
                //move = 2;//回転させ方の設定(左1右2上3下4左斜め上5左斜め下6右斜め上7右斜め下8)
                //orbitSpeed = 0f;  // 敵の回転速度
                // spawnRadiusmove = 1;//拡大と収縮
                //Vector3 enemyPosition = new Vector3(40, 0, 30);
                //SpawnEnemy(enemyPosition, 8, 2, 3f, 1, 6f, 2, 5f, 0);

               
               

                enemyPosition = new Vector3(40, 0, 30);
               
                    for (int i = 0; i <= enemySpawns - 1; i++)
                    {
                        //敵のスポーン位置

                       // woll = Instantiate(woll, new Vector3(EnemePos.x, EnemePos.y, EnemePos.z), Quaternion.identity);
                        //EnemePos.x += 30;


                        enemyPosition.x += 5;
                        SpawnEnemy(enemyPosition, 5, 2, 3f, 1, 20f, 2, 5f, 0);


                       
                    }
                
                wave = 2;
                break;

            case 2:
                //準備   
                enemySpawns = 3;
                if (time == 6)
                {
                    wave = 3;
                }
                break;

            case 3:
                //準備   
                enemyPosition = new Vector3(-40, 0, -10);
                for (int i = 0; i <= enemySpawns - 1; i++) 
                {
                    enemyPosition.x += 20;
                    SpawnEnemy(enemyPosition, 10, 1, 3f, 5, 20f, 5, 5f, 0);
                }

                wave = 4;
                break;

            case 4:
                //準備   

                if (time == 20)
                {
                    enemySpawns = 8;
                    wave = 5;
                }
                break;

            case 5:
                //準備   

                enemyPosition = new Vector3(-40, 0,30 );
                for (int i = 0; i <= enemySpawns - 1; i++)
                {
                    enemyPosition.x -= 5;
                    SpawnEnemy(enemyPosition, 10, 2, 3f, 2, 15f, 5, 5f, 0);
                }
                wave = 6;
                break;


            case 6:
                //準備   

                if (time == 30)
                {
                    wave = 7;
                }
                break;

            case 7:
                //準備   

                enemyPosition = new Vector3(40, 0, 30);
                for (int i = 0; i <= enemySpawns - 1; i++)
                {
                    enemyPosition.x += 5;
                    SpawnEnemy(enemyPosition, 10, 1, 5f, 1, 15f, 7, 5f, 0);
                }
                wave = 8;
                
                break;

            case 8:
                //準備   
                //45
                if (time == 45)
                {
                    wave = 9;
                }
                break;

            case 9:
                //準備   

                enemyPosition = new Vector3(40, 0, 30);
                for (int i = 0; i <= enemySpawns - 1; i++)
                {
                    enemyPosition.x += 5;
                    SpawnEnemy(enemyPosition, 10, 1, 8f, 1, 15f, 7, 5f, 0);
                }
               

                enemyPosition = new Vector3(-40, 0, 30);
                for (int i = 0; i <= enemySpawns - 1; i++)
                {
                    enemyPosition.x -= 5;
                    SpawnEnemy(enemyPosition, 10, 2, 3f, 2, 15f, 5, 5f, 0);
                }
                wave = 10;
                break;

            case 10:
                //準備   
                //55
                if (time == 55)
                {
                    enemySpawns = 5;
                    wave = 11;
                }
                break;

            case 11:
                //準備   

               
                    EnemePos.x = -35;
                    EnemePos.y = -9;
                    EnemePos.z = 325;

                    for (int i = 0; i <= enemySpawns - 1; i++)
                    {
                        //敵のスポーン位置

                         woll = Instantiate(woll, new Vector3(EnemePos.x, EnemePos.y, EnemePos.z), Quaternion.identity);
                        EnemePos.x += 10;
                        EnemePos.z +=5 ;

                                            
                    }
                wave = 12;
                break;

            case 12:
                //準備   
                //70
                if (time == 70)
                {
                    enemySpawns = 5;
                    wave = 13;
                }

                break;

            case 13:
                //準備   
                enemyPosition = new Vector3(40, 0, 30);
                for (int i = 0; i <= enemySpawns - 1; i++)
                {
                    enemyPosition.x += 5;
                    SpawnEnemy(enemyPosition, 10, 1, 8f, 1, 15f, 7, 5f, 0);
                }


                enemyPosition = new Vector3(-40, 0, 30);
                for (int i = 0; i <= enemySpawns - 1; i++)
                {
                    enemyPosition.x -= 5;
                    SpawnEnemy(enemyPosition, 10, 2, 3f, 2, 15f, 5, 5f, 0);
                }

                //EnemePos.x = +35;
                //EnemePos.y = -9;
                //EnemePos.z = 325;

                //for (int i = 0; i <= enemySpawns - 1; i++)
                //{
                //    //敵のスポーン位置

                //     woll = Instantiate(woll, new Vector3(EnemePos.x, EnemePos.y, EnemePos.z), Quaternion.identity);
                //    EnemePos.x -= 10;
                //    EnemePos.z += 5;


                //}
                wave = 14;
                break;

             case 14:
                  if (time == 80)
                 {
                  enemySpawns = 10;
                  wave = 15;
                 }
                break;

            case 15:
                enemyPosition = new Vector3(40, 0, 30);
                for (int i = 0; i <= enemySpawns - 1; i++)
                {
                    enemyPosition.x += 5;
                    SpawnEnemy(enemyPosition, 12, 1, 8f, 1, 15f, 7, 3f, 0);
                }


                enemyPosition = new Vector3(-40, 0, 30);
                for (int i = 0; i <= enemySpawns - 1; i++)
                {
                    enemyPosition.x -= 5;
                    SpawnEnemy(enemyPosition, 12, 2, 3f, 2, 15f, 5, 1f, 0);
                }
                wave = 16;
                
                break;

            case 16:

                if (time == 95)
                {
                    wave = 17;
                }
               
                break;

            case 17:


                //クリアや条件
                wave = 18;

                break;
        }

        void SpawnEnemy(Vector3 spawnPosition, int enemyCount, int arrangement, float spawnRadius, int centerMovement, float speed, int move, float orbitSpeed, int spawnRadiusMove)
        {
            

            // オブジェクトのスポーン
            GameObject ChaseAndOrbitInstance = Instantiate(ChaseAndOrbit, spawnPosition, Quaternion.identity);

            // スポーンした敵のスクリプトを取得
            EnemySpawnerScript enemyScript = ChaseAndOrbitInstance.GetComponent<EnemySpawnerScript>();

           

            // 敵の設定を行う
            enemyScript.enemyCount = enemyCount;
            enemyScript.arrangement = arrangement;
            enemyScript.spawnRadius = spawnRadius;
            enemyScript.centerMovement = centerMovement;
            enemyScript.speed = speed;
            enemyScript.move = move;
            enemyScript.orbitSpeed = orbitSpeed;
            enemyScript.spawnRadiusmove = spawnRadiusMove;
        }
        
    }
}






