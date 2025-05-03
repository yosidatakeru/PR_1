using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanonBolletScript : MonoBehaviour
{
  

    //敵の攻撃制御
    int timeUntilNextShot = 0;
    //弾の制御乱数
    int bulletTimerReset = 200;
    //弾のオブジェクトの呼び出し
    public GameObject EnemyBullet;
    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextShot =200;
    }

    // Update is called once per frame
    void Update()
    {
        //  this.transform.LookAt(Player.transform);
        //攻撃
        timeUntilNextShot--;
       

    }

    // プレイヤーが当たっている間の処理
    public void OnPlayerStay()
    {
       
        if (timeUntilNextShot <= 0)
        {

            //敵の生成
            Instantiate(EnemyBullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

            // EnemySpawn;
            timeUntilNextShot = bulletTimerReset;

        }
    }

    // プレイヤーが範囲から出た時の処理
    public void OnPlayerExit()
    {
        Debug.Log("プレイヤーが範囲外に出た！");
        
    }
}
