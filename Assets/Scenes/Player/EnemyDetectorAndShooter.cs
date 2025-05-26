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
    
    //範囲設定
    public Vector3 detectionSize = new Vector3(20f, 15f, 150f);

    public LayerMask enemyLayer; // 敵のレイヤーマスク
    int maxTargets = 10; // 最大検出する敵の数
    public GameObject markerPrefab; // 敵の位置を示す3Dモデル


    private List<Transform> detectedEnemies = new List<Transform>(); // 検出した敵リスト
    private List<GameObject> activeMarkers = new List<GameObject>(); // 配置されたマーカーのリスト
    private bool isDetecting = false; // 検出中フラグ
    private Coroutine detectionCoroutine; // 索敵用コルーチン
    bool isBlocked = false;
    public bool isBlockedForward = false; // 前進禁止フラグ
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
            //敵の検出
            if (!isDetecting)
            {
                isDetecting = true;
                detectionCoroutine = StartCoroutine(DetectEnemiesPeriodically());
              //  Debug.Log("検出を開始...");
            }


      //  }

        // スペースキーを離した瞬間に弾を発射
        if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonDown("RB"))
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

        //OnDrawGizmosSelected();

        if (detectedEnemies.Count > 0)
        {
            CheckForObstacles();
        }

        if (detectionCoroutine == null)
        {
            isDetecting = false;
        }
        FollowMarkers();

       
    }


    /// <summary>
    /// ロックオンしている敵が障害物の後ろに入ったらロック解除
    /// </summary>
    void CheckForObstacles()
    {
        for (int i = detectedEnemies.Count - 1; i >= 0; i--)
        {
            Transform enemy = detectedEnemies[i];

            if (enemy == null)
            {
                // 敵が削除された場合もロック解除
                RemoveTarget(i);
                continue;
            }

            Vector3 rayStart = transform.position + Vector3.up * 1.5f; // 少し上からRayを撃つ
            Vector3 enemyCenter = enemy.GetComponent<Collider>().bounds.center; // 敵の中心

            Vector3 direction = (enemyCenter - rayStart).normalized;
            float distance = Vector3.Distance(rayStart, enemyCenter);

            RaycastHit[] hits = Physics.RaycastAll(rayStart, direction, distance);

            bool isBlocked = false;
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("EnemyWoll"))
                {
                    isBlocked = true;
                    //Debug.Log($"敵 {enemy.name} は障害物 {hit.collider.name} によって見えなくなりました。ロック解除。");
                    break;
                }
            }

            // **障害物があればロック解除**
            if (isBlocked)
            {
                RemoveTarget(i);
            }
        }
    }

    /// <summary>
    /// 指定したインデックスの敵をリストから削除し、マーカーも消去
    /// </summary>
    void RemoveTarget(int index)
    {
        if (index < detectedEnemies.Count)
        {
            detectedEnemies.RemoveAt(index);
        }

        if (index < activeMarkers.Count)
        {
            Destroy(activeMarkers[index]);
            activeMarkers.RemoveAt(index);
        }
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
            
        while (isDetecting)
        {
            DetectAndAddEnemy();
            yield return new WaitForSeconds(0.1f); // 1秒間隔で実行
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
           // Debug.Log("敵が見つかりませんでした。");
            return;
        }



        // 敵を距離順にソート
        List<Transform> sortedEnemies = hits
            .OrderBy(hit => Vector3.Distance(transform.position, hit.transform.position))
            .Select(hit => hit.transform)
            .ToList();
        Vector3 playerForward = transform.forward; // プレイヤーの前方ベクトル


        foreach (Collider hit in hits.OrderBy(hit => Vector3.Distance(transform.position, hit.transform.position)))
        {
            Transform enemy = hit.transform;

            // 敵の方向ベクトルを計算
            Vector3 toEnemy = (enemy.position - transform.position).normalized;

            // ドット積を使って前方のみを判定（0 以上なら前方）
            if (Vector3.Dot(playerForward, toEnemy) < 0)
            {
               // Debug.Log($"敵 {enemy.name} は後ろにいるため無視します。");
                continue;
            }

            // **Raycastを使って遮蔽物チェック**
            Vector3 rayStart = transform.position;
            Vector3 rayEnd = enemy.position;
            Vector3 direction = (rayEnd - rayStart).normalized;
            float distance = Vector3.Distance(rayStart, rayEnd);

            RaycastHit hitInfo;
            isBlocked = false;

            if (Physics.Raycast(rayStart, direction, out hitInfo, distance))
            {
                if (hitInfo.collider != null && hitInfo.collider.CompareTag("EnemyWoll"))
                {
                    
                    isBlocked = true;
                    //Debug.Log($"敵 {enemy.name} は '{hitInfo.collider.name}' (Obstacle) によってブロックされています。");
                }
            }

            if (isBlocked)
            {
                continue;
            }

            


            if (!detectedEnemies.Contains(enemy))
            {
                detectedEnemies.Add(enemy);
                //Debug.Log($"敵 {enemy.name} を検出しました！");

                if (activeMarkers.Count < maxTargets)
                {
                    GameObject marker = Instantiate(markerPrefab, enemy.position, Quaternion.identity);
                    activeMarkers.Add(marker);
                }

                if (detectedEnemies.Count >= maxTargets) break;
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