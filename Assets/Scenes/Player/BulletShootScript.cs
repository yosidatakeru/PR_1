using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletShootScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    private Vector3 moveDirection; // 発射方向

    // Start is called before the first frame update
    Vector3 BulletPos = Vector3.zero;

    void Start()
    {
        // プレイヤーの位置を取得
        GameObject player = GameObject.FindGameObjectWithTag("Frame");
        if (player != null)
        {
            // 発射方向を計算（正規化して速度に影響を与えないようにする）
            moveDirection = (player.transform.position - transform.position).normalized;
        }
        else
        {
            // 念のためデフォルトで前方向に進む
            moveDirection = transform.forward;
        }

        // 一定時間後に自動で削除
        Destroy(gameObject, lifetime);

    }

    // Update is called once per frame
    void Update()
    {

        // ターゲット位置に向かって移動

        //// 目標地点に到達したら削除
        //if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        //{
        //    Destroy(gameObject);
        //}
        transform.position += moveDirection * speed * Time.deltaTime;


    }
}
