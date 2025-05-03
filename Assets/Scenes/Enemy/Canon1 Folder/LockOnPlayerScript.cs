using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnPlayerScript : MonoBehaviour
{

    // Start is called before the first frame update
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // "Player" タグを使ってプレイヤーを探す
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerStay()
    {

        this.transform.LookAt(player.transform);
    }

    // プレイヤーが範囲から出た時の処理
    public void OnPlayerExit()
    {
        Debug.Log("プレイヤーが範囲外に出た！");

    }
}
