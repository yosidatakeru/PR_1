using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float scrollSpeed = 100f;  // 背景のスクロール速度
    public float resetPosition = -244f; // 手前（カメラ側）に到達する位置
    public float startPosition = 1464f;  // 背景が最初に配置される奥の位置



    Vector3 groundPos = Vector3.zero;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        // Z軸方向に手前へ移動
        transform.position -= Vector3.forward * scrollSpeed * Time.deltaTime;

        // 背景が手前に来たら奥へ戻す
        if (transform.position.z <= resetPosition)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPosition);
        }

    }
}
