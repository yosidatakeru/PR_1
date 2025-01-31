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
    private float speed = 0.0f;//回転軸の移動
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
            float z = player.position.z + spawnRadius * Mathf.Sin(angle);
            Vector3 spawnPosition = new Vector3(x, player.position.y, z);

            // 敵を生成
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // 敵の周回スクリプトにパラメータを渡す
            ScoreMangerScript orbitScript = enemy.AddComponent<ScoreMangerScript>();
            orbitScript.Setup(player, spawnRadius, orbitSpeed, angle);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position -= speed * transform.right * Time.deltaTime;
    }
}
