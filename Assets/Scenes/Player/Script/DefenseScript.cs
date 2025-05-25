using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseScript : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("タグ 'Player' を持つオブジェクトが見つかりませんでした。");
        }
        //ここで消す
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.position; // プレイヤーに追従
        }
    }
}
