using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    int speed = 200;
    public float resetPositionZ = 1450f; // z 座標リセット値を設定





    Vector3 groundPos = Vector3.zero;
    void Start()
    {
        groundPos.z = resetPositionZ; // 初期位置を設定
    }

    // Update is called once per frame
    void Update()
    {

        transform.position -= speed * transform.forward * Time.deltaTime;

        // z 座標が -100 に近い場合にリセット
        if (transform.position.z <=-550) // 誤差を許容した比較
        {
            Vector3 newPosition = transform.position;
            newPosition.z = groundPos.z;
            transform.position = newPosition;
        }
    }
}
