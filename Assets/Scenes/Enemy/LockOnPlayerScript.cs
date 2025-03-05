using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnPlayerScript : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerStay()
    {

        this.transform.LookAt(Player.transform);
    }

    // プレイヤーが範囲から出た時の処理
    public void OnPlayerExit()
    {
        Debug.Log("プレイヤーが範囲外に出た！");

    }
}
