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

    public GameObject muzzleEffect; // エフェクトのプレハブ
    private bool hasPlayedEffect = false; // 一度だけ再生するためのフラグ

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

            StartCoroutine(ShootAfterEffect());
            timeUntilNextShot = bulletTimerReset;
            hasPlayedEffect = false;
        }
    }

    private System.Collections.IEnumerator ShootAfterEffect()
    {
        // エフェクト再生
        if (hasPlayedEffect == false && muzzleEffect != null)
        {
            Instantiate(muzzleEffect, transform.position, transform.rotation);
            hasPlayedEffect = true; // フラグを立てて二度目以降は再生しない
        }

        // 少し待ってから弾を発射（0.2秒待つ）
        yield return new WaitForSeconds(0.5f);

        Instantiate(EnemyBullet, transform.position, Quaternion.identity);
        
    }

    // プレイヤーが範囲から出た時の処理
    public void OnPlayerExit()
    {
        Debug.Log("プレイヤーが範囲外に出た！");
        
    }
}
