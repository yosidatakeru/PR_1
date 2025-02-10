using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowingBulletScript : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    private Vector3 targetPosition; // 発射時のプレイヤー位置

    void Start()
    {
        // 発射された瞬間のプレイヤー位置を取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            targetPosition = player.transform.position;
        }
        else
        {
            // プレイヤーがいない場合は前方に飛ぶ
            targetPosition = transform.position + transform.forward * 10f;
        }

       
    }

    void Update()
    {
        // ターゲット位置に向かって移動
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 目標地点に到達したら削除
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<SphereCollider>().enabled = false;
            //敵を消す/
            Destroy(gameObject);



        }
    }
}
