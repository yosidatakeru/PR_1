using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMangerScript : MonoBehaviour
{
    public Transform player;  // 追従するターゲット（プレイヤー）
    public float radius = 5f; // 軌道半径
    public float speed = 2f;  // 回転速度

    private float angle = 0f; // 現在の角度
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // プレイヤーの現在位置を基準に円軌道を計算
            angle += speed * Time.deltaTime * 2 * Mathf.PI;

            float x = player.position.x + radius * Mathf.Cos(angle);
            float z = player.position.z + radius * Mathf.Sin(angle);

            // 敵を新しい位置に移動
            transform.position = new Vector3(x, transform.position.y, z);
        }
    }
}
