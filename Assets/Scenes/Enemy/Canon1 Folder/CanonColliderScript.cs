using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonColliderScript : MonoBehaviour
{
    public LockOnPlayerScript lookAtScript; // LookAt処理を持つオブジェクトのスクリプトを指定

    public CanonBolletScript lookBoullet; // LookAt処理を持つオブジェクトのスクリプトを指定

    private void OnTriggerStay(Collider other)
    {
        if (lookAtScript != null && other.CompareTag("Player")) // プレイヤーと接触中
        {
            lookAtScript.OnPlayerStay(); // 当たっている間に処理
        }

        if (lookBoullet != null && other.CompareTag("Player")) // プレイヤーと接触中
        {
            lookBoullet.OnPlayerStay(); // 当たっている間に処理
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (lookBoullet != null && other.CompareTag("Player")) // プレイヤーが範囲から出たら
        {
            lookBoullet.OnPlayerExit(); // 当たり判定が終了
        }
    }
}
