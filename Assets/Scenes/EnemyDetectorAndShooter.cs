using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class EnemyDetectorAndShooter : MonoBehaviour
{

    public GameObject missilePrefab; // 発射する弾のプレハブ
    public Transform launchPoint; // 弾を発射する位置
    public float detectionRadius = 20f; // 検出範囲
    

   public Vector3 detectionSize = new Vector3(20f, 10f, 100f);

    public LayerMask enemyLayer; // 敵のレイヤーマスク
    public int maxTargets = 5; // 最大検出する敵の数
    public GameObject markerPrefab; // 敵の位置を示す3Dモデル


    private List<Transform> detectedEnemies = new List<Transform>(); // 検出した敵リスト
    private List<GameObject> activeMarkers = new List<GameObject>(); // 配置されたマーカーのリスト
    private bool isDetecting = false; // 検出中フラグ
    private Coroutine detectionCoroutine; // 索敵用コルーチン
   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //OnDrawGizmosSelected();

        // スペースキーが押されている間、敵を検出
        if (Input.GetKeyDown(KeyCode.Space) && detectionCoroutine == null)
        {
            if (!isDetecting)
            {
                isDetecting = true;
                detectionCoroutine = StartCoroutine(DetectEnemiesPeriodically());
                Debug.Log("検出を開始...");
            }


        }

        // スペースキーを離した瞬間に弾を発射
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isDetecting)
            {
                if (detectionCoroutine != null)
                {
                    StopCoroutine(detectionCoroutine);
                    detectionCoroutine = null; // コルーチンを停止後、nullに設定
                    Debug.Log("検出を停止しました...");
                }
                FireMissiles();
                Debug.Log("弾を発射しました！");
            }

            // 状態をリセット
            isDetecting = false;

            // マーカーを削除
            ClearMarkers();
        }
        if (detectionCoroutine == null)
        {
            isDetecting = false;
        }
        FollowMarkers();

       
    }

    private void OnDrawGizmos() // OnDrawGizmosSelected() → OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // **検出範囲のボックスサイズ**
        Vector3 boxSize = new Vector3(detectionSize.x, detectionSize.y, detectionSize.z); // X, Y, Z の大きさ

        // **ボックスのワイヤーフレームを描画**
        Gizmos.DrawWireCube(transform.position, boxSize);
    }

    //マーカの処理
    private void FollowMarkers()
    {
        

        for (int i = activeMarkers.Count - 1; i >= 0; i--) // インデックスがズレないように逆順ループ
        {
            if (activeMarkers[i] != null && detectedEnemies[i] != null)
            {
                activeMarkers[i].transform.position = detectedEnemies[i].position;
            }
            else
            {
                if (activeMarkers[i] != null)
                {
                    Destroy(activeMarkers[i]);
                }
                activeMarkers.RemoveAt(i);
                detectedEnemies.RemoveAt(i);
            }
        }
    }



    private void ClearMarkers()
    {
        // 配置された全てのマーカーを削除
        foreach (GameObject marker in activeMarkers)
        {
            Destroy(marker);
        }
        activeMarkers.Clear();
    }



    IEnumerator DetectEnemiesPeriodically()
    {
        yield return new WaitForSeconds(2f);
        while (isDetecting)
        {
            DetectAndAddEnemy();
            yield return new WaitForSeconds(1f); // 1秒間隔で実行
        }
    }





    void DetectAndAddEnemy()
    {
       
        // すでに maxTargets の敵を検出している場合、新しい敵を追加しない
        if (detectedEnemies.Count >= maxTargets)
        {
            return;
        }

        // **長方形のサイズを設定（X, Y, Z 方向の大きさ）**
        Vector3 boxSize = new Vector3(detectionSize.x, detectionSize.y, detectionSize.z); // X, Y, Z のサイズ

        // **ボックス内の敵を取得**
        Collider[] hits = Physics.OverlapBox(transform.position, boxSize / 2, Quaternion.identity, enemyLayer);

        if (hits.Length == 0)
        {
            Debug.Log("敵が見つかりませんでした。");
            return;
        }

        // 敵を距離順にソート
        List<Transform> sortedEnemies = hits
            .OrderBy(hit => Vector3.Distance(transform.position, hit.transform.position))
            .Select(hit => hit.transform)
            .ToList();

        foreach (Transform enemy in sortedEnemies)
        {
            if (!detectedEnemies.Contains(enemy))
            {
                detectedEnemies.Add(enemy);
                Debug.Log($"敵 {enemy.name} を検出しました！");

                if (activeMarkers.Count < maxTargets)
                {
                    GameObject marker = Instantiate(markerPrefab, enemy.position, Quaternion.identity);
                    activeMarkers.Add(marker);
                }

                if (detectedEnemies.Count >= maxTargets)
                {
                    break;
                }
            }
        }

    }

  


    void FireMissiles()
    {

        // 検出した敵それぞれに弾を発射
        foreach (Transform enemy in detectedEnemies)
        {
            if (enemy != null)
            {
                // 弾を生成
                GameObject missile = Instantiate(missilePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

                // ターゲットを設定（弾側に設定メソッドを用意する必要あり）
                Missile missileScript = missile.GetComponent<Missile>();
                if (missileScript != null)
                {
                    missileScript.SetTarget(enemy);
                }
            }
        }
        
        ClearMarkers();
        
        // 検出リストをクリア
        detectedEnemies.Clear();
        
    }
}