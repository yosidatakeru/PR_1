using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.ComponentModel;

public class EnemySpawnerScript : MonoBehaviour
{
   
    // Start is called before the first frame update
    public GameObject enemyPrefab; // 敵のプレハブ
  

    public Transform player;       // プレイヤー
    public int enemyCount = 0;     // スポーンする敵の数
    public float spawnRadius = 0; // スポーンする円の半径
    public float orbitSpeed = 0;  // 敵の回転速度
    public float speed = 0;//回転軸の移動
    Vector3 spawnPosition = new Vector3(0,0,0);
    public int arrangement = 0;//どう配置するか（横１縦２）
    public int move = 0;//回転させ方の設定
    public int centerMovement = 0;//移動の設定
    public int spawnRadiusmove = 0;//拡大と収縮
    float x = 0;
    float y = 0;
    float z = 0;
    void Start()
    {
        SpawnEnemies();
       

    }

  
    public void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // 敵を均等な角度に配置
            float angle = (i * 2 * Mathf.PI) / enemyCount;

            // スポーン位置の計算
           
            //どうスポーンさせるか
            switch (arrangement) 
            {
                case 0:
                   
                    //何もしない
                    break;

                case 1:
                     x = player.position.x + spawnRadius * Mathf.Cos(angle);
                     z = player.position.z + spawnRadius * Mathf.Sin(angle);
                    //横にスポーン
                    spawnPosition = new Vector3(transform.position.x, y, z);
                   
                    break;
                case 2:
                    
                    y = player.position.y + spawnRadius * Mathf.Cos(angle);
                    z = player.position.z + spawnRadius * Mathf.Sin(angle);
                    //縦にスポーン
                    spawnPosition = new Vector3(x, player.position.y, z);
                    break;
            }

            // 敵を生成
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // 敵の周回スクリプトにパラメータを渡す
            //ここでスクリプトを渡している
            ScoreMangerScript orbitScript = enemy.AddComponent<ScoreMangerScript>();
            orbitScript.Setup(player, spawnRadius, orbitSpeed, angle, move, spawnRadiusmove);
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (centerMovement) 
        {
            case 0:
                //何もしない
                break;
            case 1:
                //左
                transform.position -= speed * transform.right * Time.deltaTime;
                break;
            case 2:
                //右
                transform.position += speed * transform.right * Time.deltaTime;
                break;
            case 3:
                //上
                transform.position += speed * transform.up * Time.deltaTime;
                break;
            case 4:
                //下
                transform.position -= speed * transform.right * Time.deltaTime;
                break;

            case 5:
                //前
                transform.position += speed * transform.forward * Time.deltaTime;
                break;

            case 6:
                //後
                transform.position -= speed * transform.forward * Time.deltaTime;
                break;



        }


        Destroy(gameObject, 30);


       
    }

   
}
