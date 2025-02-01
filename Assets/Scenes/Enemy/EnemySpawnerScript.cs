using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab; // 敵のプレハブ
    public Transform player;       // プレイヤー
    private int enemyCount = 8;     // スポーンする敵の数
    private float spawnRadius = 8f; // スポーンする円の半径
    private float orbitSpeed = 0.5f;  // 敵の回転速度
    private float speed = 10.0f;//回転軸の移動
    Vector3 spawnPosition = new Vector3(0,0,0);
    public int arrangement = 2;
    public int erase = 1;
    public int timeToErase = 100;
    //回転させ方の設定
    public int move = 4;
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
            float x = player.position.x + spawnRadius * Mathf.Cos(angle);
            float y = player.position.x + spawnRadius * Mathf.Cos(angle);
            float z = player.position.z + spawnRadius * Mathf.Sin(angle);
            //どうスポーンさせるか
            switch (arrangement) 
            {
                case 0:
                   
                    //何もしない
                    break;

                case 1:
                    //横にスポーン
                    spawnPosition = new Vector3(transform.position.x, y, z);
                   
                    break;
                case 2:
                    //縦にスポーン
                    spawnPosition = new Vector3(x, player.position.y, z);
                    break;
            }

            // 敵を生成
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // 敵の周回スクリプトにパラメータを渡す
            //ここでスクリプトを渡している
            ScoreMangerScript orbitScript = enemy.AddComponent<ScoreMangerScript>();
            orbitScript.Setup(player, spawnRadius, orbitSpeed, angle, move);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position -= speed * transform.right * Time.deltaTime;


       
        if (transform.position.x >= 250 * (spawnRadius * 2)|| transform.position.x <= -250 * (spawnRadius * 2))
        {
            Destroy(gameObject);
        }

        if (transform.position.y >= 250 * (spawnRadius * 2) )
        {
            Destroy(gameObject);
        }

        if (transform.position.y >= 100 * (spawnRadius * 2))
        {
            Destroy(gameObject);
        }
    }
}
