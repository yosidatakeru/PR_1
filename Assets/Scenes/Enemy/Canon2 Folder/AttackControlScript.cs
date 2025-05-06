using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackControlScript : MonoBehaviour
{
    public ShootScript ShootR; // LookAt処理を持つオブジェクトのスクリプトを指定
    public ShootScript ShootL;// LookAt処理を持つオブジェクトのスクリプトを指定

    void Start()
    {
        StartCoroutine(RepeatShoot());
    }

    private IEnumerator RepeatShoot()
    {
        while (true)
        {
            if (ShootR != null) ShootR.Shoot();
            if (ShootL != null) ShootL.Shoot();

            yield return new WaitForSeconds(5f); // 5秒待つ
        }
    }

}
