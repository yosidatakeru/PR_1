using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetScript : MonoBehaviour
{
    GameObject player;
    private MeshRenderer meshRenderer;
    float zposition;

    void Start()
    {
        // タグでプレイヤーを取得
        player = GameObject.FindGameObjectWithTag("Player");

        // このオブジェクトの MeshRenderer を取得
        meshRenderer = GetComponent<MeshRenderer>();

        if (player != null)
        {
            Debug.Log("プレイヤーの初期座標: " + player.transform.position);
        }
        else
        {
            Debug.LogWarning("Playerタグのオブジェクトが見つかりませんでした。");
        }

        if (meshRenderer == null)
        {
            Debug.LogWarning("MeshRenderer がアタッチされていません！");
        }
    }

    void Update()
    {
        // プレイヤーとメッシュが null でないか確認
        if (player != null && meshRenderer != null)
        {
            Vector3 pos = player.transform.position;
            zposition = pos.z;

          

            // Z座標の範囲で表示切り替え
            if (zposition >= 30 && zposition <= 3300f)
            {
                meshRenderer.enabled = true;
            }
            else
            {
                meshRenderer.enabled = false;
            }
        }
    }
}
